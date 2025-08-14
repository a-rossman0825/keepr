import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Vault } from "@/models/Vault.js";
import { Keep } from "@/models/Keep.js";


class VaultsService {

  async createVault(vaultData, route) {
    const res = await api.post("api/vaults", vaultData);
    const vault = new Vault(res.data);
    if (route == vault.creatorId)
      // FIXME unshift
    AppState.vaults.push(vault);
  }


  async DeleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`);
    logger.log("Deleted Vault", res.data);

    const i = AppState.vaults.findIndex((vault) => vault.id == vaultId);
    AppState.vaults.splice(i, 1);
    AppState.activeVault = null;
  }


  async getKeepsForVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    logger.log('Got VaultKeeps!', res.data);
    const keeps = res.data.map((pojo) => new Keep(pojo))
    AppState.keeps = keeps;
  }


  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`);
    logger.log('Got Vault!', res.data);
    const vault = new Vault(res.data);
    AppState.activeVault = vault;
  }


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
  
  
  async getVaultsByProfileId(profileId, bool) {
    const res = await api.get(`api/profiles/${profileId}/vaults`);
    logger.log('Got Profile Vaults', res.data);
    if (!bool){
      AppState.vaults = res.data.map((pojo) => new Vault(pojo));
    } else {
      AppState.userVaults = res.data.map((pojo)=> new Vault(pojo));
    }
  }

}

export const vaultsService = new VaultsService();