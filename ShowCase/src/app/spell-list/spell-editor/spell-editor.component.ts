import {Component, Input, OnInit} from '@angular/core';
import {Spell} from "../../Items/Spell";
import {CoinType, Price} from "../../Items/Base";
import {FormControl, FormGroup} from "@angular/forms";
import {FetchDataService} from "../../fetch-data.service";

@Component({
  selector: 'app-spell-editor',
  templateUrl: './spell-editor.component.html',
  styleUrls: ['./spell-editor.component.css']
})
export class SpellEditorComponent implements OnInit {


  @Input("Spell") public spell!:Spell;
  @Input("list") public list!: Array<Spell>;
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
    levell: new FormControl()
  });

  constructor(private fetchData:FetchDataService) { }

  ngOnInit(): void {
    this.formController.setValue({
      name:this.spell.name,
      description: this.spell.description,
      source: this.spell.source,
      price: {
        value:this.spell.price.value,
        coin: this.spell.price.coin
      },
      levell: this.spell.levell
    })
    this.showTime = true;

  }

  editorSwitch(){
    this.isEditing = !this.isEditing;
  }

  addToDatabase(){
    let value = this.formController.value;
    let tmp: Spell = new Spell(0,value['name'],value['description'],value['source'],
      new Price(value['price']['value'], value['name']['coin'],),value['spellName'],value['levell']);
    console.log(tmp);
    this.spell = tmp;
    return  this.fetchData.postSpell(tmp)
      ;
  }
}
