import { Profile } from '@/models/Profile.js';
import { AppState } from '../AppState.js'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger.js'
import { api } from './AxiosService.js'


class AccountService {



  async getProfileById(profileId) {
    const res = await api.get(`api/profiles/${profileId}`);
    logger.log('got profile', res.data);
    AppState.activeProfile = new Profile(res.data);
  }

  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
}

export const accountService = new AccountService()
