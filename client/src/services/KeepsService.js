import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";
import { VaultKeep } from "@/models/VaultKeep.js";

class KeepsService {
  
  async createVaultKeep(vaultKeepData) {
    logger.log("䷳", vaultKeepData);
    const res = await api.post("api/vaultKeeps", vaultKeepData);
    const vaultKeep = new VaultKeep(res.data);
    AppState.vaultKeeps.push(vaultKeep);
  }
  
  
  
  async deleteVaultKeep(vaultKeepId) {
    const res = await api.delete(`api/vaultkeeps/${vaultKeepId}`);
    logger.log('Deleted VaultKeep', res.data);
    AppState.activeKeep = null;
    const i = AppState.keeps.findIndex((keep)=> keep.vaultKeepId == vaultKeepId);
    AppState.keeps.splice(i, 1);
  }


  async updateKeep(keepId, updateData) {
    const res = await api.put(`api/keeps/${keepId}`, updateData);
    logger.log("updated Keep", res.data);

    AppState.activeKeep = new Keep(res.data);
  }

  async getKeepById(keepId) {
    const res = await api.get(`api/keeps/${keepId}`);
    const newKeep = new Keep(res.data);

    AppState.activeKeep = newKeep;

    const i = AppState.keeps.findIndex((keep) => keep.id == newKeep.id);
    AppState.keeps.splice(i, 1, newKeep);

    return newKeep;
  }

  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`);
    logger.log("Deleted keep", res.data);

    const i = AppState.keeps.findIndex((keep) => keep.id == keepId);
    logger.log("got index of keep", i);

    AppState.keeps.splice(i, 1);
  }

  async createKeep(keepData, route) {
    logger.log("🖼️", keepData);
    const res = await api.post("api/keeps", keepData);
    const keep = new Keep(res.data);
    if (route == keep.creatorId)
    AppState.keeps.push(keep);
  }

  async getKeeps() {
    const res = await api.get("api/keeps");
    logger.log("Got Keeps!", res.data);

    AppState.keeps = res.data.map((pojo) => new Keep(pojo));
  }

    async getKeepsByProfileId(profileId) {
      const res = await api.get(`api/profiles/${profileId}/keeps`);
      logger.log('Got Users Keeps', res.data);
      AppState.keeps = res.data.map((pojo) => new Keep(pojo));
  
    }
}

export const keepsService = new KeepsService();
