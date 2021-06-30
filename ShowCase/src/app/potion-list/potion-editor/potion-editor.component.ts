import { Component, OnInit, Input } from '@angular/core';
import {FormControl, FormGroup, FormGroupName} from "@angular/forms";
import {CoinType, Price} from "../../Items/Base";
import {Potion} from "../../Items/Potion";
import {FetchDataService} from "../../fetch-data.service";

@Component({
  selector: 'app-potion-editor',
  templateUrl: './potion-editor.component.html',
  styleUrls: ['./potion-editor.component.css']
})
export class PotionEditorComponent implements OnInit {

  @Input("potion") public potion!:Potion;
  @Input("list") public list!: Array<Potion>;
  public showTime: boolean = false;
  public enumCoynType =  CoinType;

  // controllo per attivare l'editing
  isEditing: boolean = true;

  //fomr controller
  public formController: FormGroup  =  new FormGroup({
    name: new FormControl(),
    description: new FormControl(),
    source: new FormControl(),
    price: new FormGroup({
      value: new FormControl(),
      coin: new FormControl()
    }),
    spellName: new FormControl(),
    levell: new FormControl()
  });

  constructor(private fetchData:FetchDataService) { }

  ngOnInit(): void {
    this.formController.setValue({
      name:this.potion.name,
      description: this.potion.description,
      source: this.potion.source,
      price: {
        value:this.potion.price.value,
        coin: this.potion.price.coin
      },
      spellName: this.potion.spellName.name,
      levell: this.potion.levell
    })
    this.showTime = true;
    this.potion.spellName.id=3;
  }

  editorSwitch(){
    this.isEditing = !this.isEditing;
  }

  addToDatabase(){
    let value = this.formController.value;
    let tmp: Potion = new Potion(0,value['name'],value['description'],value['source'],
      new Price(value['price']['value'], value['name']['coin'],),value['spellName'],value['levell']);
    console.log(tmp);
    this.potion = tmp;
    return  this.fetchData.postPotion(tmp)
   ;
  }
}
