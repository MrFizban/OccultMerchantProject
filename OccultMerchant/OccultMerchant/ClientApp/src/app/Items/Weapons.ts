import {Dice} from "../Items/Dice"
import {Price} from "./Price";
import {Base} from "./Base"
export enum WeaponsType {
  bludgeoning,
  piercing,
  slashing
}

export class Weapons extends Base{
  public dmgM: Dice;
  public critical: string;
  public weaponType: WeaponsType;
  public range: number;
  public proficiency: string;

  constructor( _name:string="Gianni", _descprition: string = "Una descrizone", _source :string ="", _price :Price = new Price(), _dmgM: Dice = new Dice(), _critical: string = "", _weaponsType: WeaponsType = 0, _range: number = 0, _proficiency: string = "") {
    super(_name,_descprition,_source,_price);
    this.dmgM = _dmgM;
    this.critical = _critical;
    this.weaponType = _weaponsType;
    this.range = _range;
    this.proficiency = _proficiency;
  }


  ToString():string{
    console.log(this.name);
    return this.critical;
  }
}
