import { DbItem } from "./DbItem.js";


export class Profile extends DbItem {
  constructor(data){
    super(data);
    this.name = data.name;
    this.picture = data.picture;
    this.coverImg = data.coverImg;
  }
}