import {Dice} from "../Items/Dice"
import {Price} from "./Price";
import {Base} from "./Base"
export enum WeaponsType {
  bludgeoning="bludgeoning",
  piercing ="piercing",
  slashing = "slashing"
}

export class Weapons{

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

  }


  ToString():string{
    console.log(this.name);
    return this.critical;
  }

  cose(): string{
    console.log("Cose :)");
    return "Cose :)";
  }
}
