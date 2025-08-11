import { logger } from "@/utils/Logger.js";
import { api } from "./AxiosService.js";
import { AppState } from "@/AppState.js";
import { Keep } from "@/models/Keep.js";


class KeepsService{
  
  async getKeeps(){
    const res = await api.get('api/keeps');
    logger.log('Got Keeps!', res.data);
    AppState.keeps = res.data.map(pojo => new Keep(pojo));
  }
}

export const keepsService = new KeepsService();