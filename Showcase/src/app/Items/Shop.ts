export class Item{
  constructor(
    public id:number = 0,
    public name:string = "",
    public quantity:number = 0
  ) {
  }
}

export class Shop{
  constructor(
    public id:number,
    public name:string = "",
    public space:number = 0,
    public isActive:boolean=false,
    public weaponsItems: Array<Item> = new Array<Item>()
  ) {
  }
}
