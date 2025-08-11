import { DbItem } from "./DbItem.js";
import { Profile } from "./Profile.js";

export class Vault extends DbItem{
  constructor(data){
    super(data);
    this.name = data.name;
    this.description = data.description;
    this.img = data.img;
    this.isPrivate = data.isPrivate;
    this.creatorId = data.creatorId;
    this.creator = new Profile(data.creator);
  }
}