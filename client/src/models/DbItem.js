
export class DbItem {
  constructor(data) {
    this.id = data.id;
    this.createdAt = new Date(data.createdAt);
    this.updatedAt = new Date(data.updatedAt);
  }
}