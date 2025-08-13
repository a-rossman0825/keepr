import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Vault } from "@/models/Vault.js";


class VaultsService {


  async updateVault(vaultId, editData) {
    const res = await api.put(`api/vaults/${vaultId}`, editData);
    logger.log('Edited vault!', res.data);
    const updatedVault = new Vault(res.data);
    
    const i = AppState.vaults.findIndex((vault) => vault.id == vaultId);
    if (i !== -1) AppState.vaults.splice(i, 1, updatedVault);
    else AppState.vaults.unshift(updatedVault);
    
    if (AppState.activeVault?.id == vaultId) {
      AppState.activeVault = updatedVault;
    }
    return updatedVault;
  }
  
  
  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    logger.log('Got Profile Vaults', res.data);
    AppState.vaults = res.data.map((pojo) => new Vault(pojo));
  }


}

export const vaultsService = new VaultsService();