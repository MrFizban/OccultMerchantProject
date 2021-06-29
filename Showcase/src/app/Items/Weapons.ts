import {Dice} from "../Items/Dice"
import {Price} from "./Price";
import {Base} from "./Base"
export enum WeaponsType {
  bludgeoning,
  piercing ,
  slashing
}

export class Weapons extends Object{

  constructor(
    public id: number ,
    public name: string = "",
    public description : string = "",
    public source: string = "",
    public price: Price = new Price(),
    public dmgM: Dice = new Dice(),
    public critical: string  = "",
    public typeWeapons: WeaponsType = WeaponsType.bludgeoning,
    public range: number = 0,
    public proficiency: string = ""
  ) {
    super();
  }

  public clone(): any {
    var cloneObj = new Weapons(0) ;
    for (var attribut in this) {
      if (typeof this[attribut] === "object") {
        // @ts-ignore
        cloneObj[attribut] = this[attribut].clone();
      } else {
        // @ts-ignore
        cloneObj[attribut] = this[attribut];
      }
    }
    return cloneObj;
  }


}
