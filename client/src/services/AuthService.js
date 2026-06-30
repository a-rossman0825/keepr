import { AUTH_EVENTS, initialize } from "@bcwdev/auth0provider-client";
import { AppState } from "../AppState.js";
import { audience, clientId, domain } from "../env.js";
import { accountService } from "./AccountService.js";
import { api } from "./AxiosService.js";
import { socketService } from "./SocketService.js";

export const AuthService = initialize({
  domain,
  clientId,
  authorizationParams: {
    audience,
    redirect_uri: window.location.origin,
  },
});

let hasRefreshInterceptor = false;
let accountBootstrapPromise = null;

AuthService.on(AUTH_EVENTS.AUTHENTICATED, async function () {
  api.defaults.headers.authorization = AuthService.bearer;
  if (!hasRefreshInterceptor) {
    api.interceptors.request.use(refreshAuthToken);
    hasRefreshInterceptor = true;
  }
  AppState.identity = AuthService.identity;
  if (!accountBootstrapPromise) {
    accountBootstrapPromise = accountService
      .getAccount()
      .finally(() => (accountBootstrapPromise = null));
  }
  await accountBootstrapPromise;
  socketService.authenticate(AuthService.bearer);
  // NOTE if there is something you want to do once the user is authenticated, place that here
});

async function refreshAuthToken(config) {
  if (AuthService.state == AUTH_EVENTS.AUTHENTICATED) {
    return config;
  }
  const expires = AuthService.identity.exp * 1000;
  const expired = expires < Date.now();
  const needsRefresh = expires < Date.now() + 1000 * 60 * 60 * 12;
  if (expired) {
    await AuthService.loginWithPopup();
  } else if (needsRefresh) {
    await AuthService.getTokenSilently();
    api.defaults.headers.authorization = AuthService.bearer;
    socketService.authenticate(AuthService.bearer);
  }
  return config;
}
