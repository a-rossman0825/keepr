export const dev = window.location.origin.includes('localhost')
export const baseURL = dev ? 'http://localhost:7045' : ''
export const useSockets = false
// TODO use your own auth stuff
export const domain = 'dev-gp7tg4z8hq16f7fn.us.auth0.com'
export const clientId = 'TNNcXURUZf90eK2BbzuKAr9h3qplq9GG'
export const audience = 'https://keepr-api.com/'