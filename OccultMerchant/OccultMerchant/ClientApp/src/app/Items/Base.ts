import {Price} from "./Price";
import {__core_private_testing_placeholder__} from "@angular/core/testing";


export class Base {
  public name: string;
  public description : string;
  public source: string;
  public price: Price;

  constructor(_name:string="Gianni", _descprition: string = "Una descrizone", _source :string ="", _price :Price = new Price()) {
    this.name = _name;
    this.description = _descprition;
    this.source = _source;
    this.price = _price;
  }
}
