import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";


class KeepsService{

  async createKeep(keepData) {
    logger.log('🖼️', keepData);
    const res = await api.post('api/keeps', keepData);
    const keep = new Keep(res.data);
    AppState.keeps.push(keep);
  }
  
  async getKeeps(){
    const res = await api.get('api/keeps');
    logger.log('Got Keeps!', res.data);
    AppState.keeps = res.data.map(pojo => new Keep(pojo));
  }
}

export const keepsService = new KeepsService();