import {Base, Price} from "./Base";
import {SpellName} from "./Potion";


export class CasterPossibility {
  constructor(name: string, level:number) {
  }
}

export class Spell extends Base {


  constructor(public id: number = 0,
              public name: string = "",
              public description: string = "",
              public source: string = "",
              public price: Price = new Price(),
              public casterPossibility: Array<CasterPossibility> = new Array<CasterPossibility>(),
              public levell: number = 0
  ) {
    super(id, name, description, source, price);
  }
}
