import {Base, Filter, Price} from "./Base";
import {Potion} from "./Potion";

export class PotionStock {
  constructor(public potion: Potion, public quantity: number) {
  }
}

export class Shop extends Base
{
  constructor(
    public id: number = 0,
    public name: string ="",
    public description: string ="",
    public source: string ="",
    public price: Price = new Price(),
    public filter:Filter = new Filter(),
    public space: number = 0,
    public isActive: boolean = false,
    public potionReserv : Array<PotionStock> = new Array<PotionStock>()
  ) {
    super(id,name,description,source,price,filter);
  }
}
