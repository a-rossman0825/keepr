import { DbItem } from "./DbItem.js";

export class VaultKeep extends DbItem{
  constructor(data){
    super(data);
    this.keepId = data.keepId;
    this.vaultId = data.vaultId;
    this.creatorId = data.creatorId;
  }
}