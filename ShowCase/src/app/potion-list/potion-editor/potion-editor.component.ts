import { Component, OnInit, Input } from '@angular/core';
import {FormControl, FormGroup, FormGroupName} from "@angular/forms";
import {CoinType, Price} from "../../Items/Base";
import {Potion, SpellName} from "../../Items/Potion";
import {FetchDataService} from "../../fetch-data.service";
import {PotionListComponent} from "../potion-list.component";

@Component({
  selector: 'app-potion-editor',
  templateUrl: './potion-editor.component.html',
  styleUrls: ['./potion-editor.component.css']
})
export class PotionEditorComponent implements OnInit {

  @Input("potion") public potion!:Potion;
  @Input("parent") public parent!: PotionListComponent;
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
  public editFormVisible: boolean = false;

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



  addToDatabase(){
    let value = this.formController.value;
    this.editFormVisible = false;
    if(this.potion.id == 0) {
      let tmp: Potion = new Potion(0, value['name'], value['description'], value['source'],
        new Price(value['price']['value'], value['name']['coin'],), new SpellName(3, value['spellName']), value['levell']);
      this.potion = tmp;
      return this.fetchData.postPotion(tmp).subscribe(() => {
        this.parent.potions.push(this.potion);
        this.parent.table.renderRows();
      });
    } else {
      let index = this.parent.potions.indexOf(this.potion);
      this.potion = new Potion(this.potion.id, value['name'], value['description'], value['source'],
        new Price(value['price']['value'], value['name']['coin'],), new SpellName(3, value['spellName']), value['levell']);

      return this.fetchData.updatePotion(this.potion).subscribe(() =>{
        this.parent.potions[index] = this.potion;
        this.parent.table.renderRows();
      })
    }

  }

  addNew(){
    if(!this.editFormVisible) {
      this.potion = new Potion();
      this.parent.expandedElement = null;
      this.editFormVisible = true;
    }else{
      this.editFormVisible  = false;
      this.parent.change.detectChanges();
      this.addNew();
    }
  }

  editForm(element:Potion){
    if(!this.editFormVisible) {
      this.potion = element;
      this.parent.expandedElement = null;
      this.editFormVisible = true;
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
    }else {
      this.editFormVisible  = false;
      this.parent.change.detectChanges();
      this.editForm(element);
    }
  }

}
