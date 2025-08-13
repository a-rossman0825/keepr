import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Vault } from "@/models/Vault.js";


class VaultsService {
  
  
  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    logger.log('Got Profile Vaults', res.data);
    AppState.vaults = res.data.map((pojo) => new Vault(pojo));
  }


}

export const vaultsService = new VaultsService();