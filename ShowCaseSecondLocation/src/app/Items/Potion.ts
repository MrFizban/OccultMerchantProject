import {Base, Price} from "./Base";
import {PotionStock} from "./Shop";

export class SpellName{
  constructor(public id:number = 0,
              public name: string = ""
  ) {}
}

export class Potion extends Base{
  constructor( public id: number = 0,
               public name: string ="",
               public description: string ="",
               public source: string ="",
               public price: Price = new Price(),
               public spellName: SpellName = new SpellName(),
               public levell: number = 0
               ) {
    super(id,name,description,source,price);
  }


}


