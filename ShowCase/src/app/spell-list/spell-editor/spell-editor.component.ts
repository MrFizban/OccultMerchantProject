import {Component, Input, OnInit} from '@angular/core';
import {CasterPossibility, Spell} from "../../Items/Spell";
import {CoinType, Price} from "../../Items/Base";
import {FormControl, FormGroup} from "@angular/forms";
import {FetchDataService} from "../../fetch-data.service";
import {SpellListComponent} from "../spell-list.component";

@Component({
  selector: 'app-spell-editor',
  templateUrl: './spell-editor.component.html',
  styleUrls: ['./spell-editor.component.css']
})
export class SpellEditorComponent implements OnInit {
  @Input("spell") public spell!:Spell;
  @Input("parent") public parent!: SpellListComponent;
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
  public editFormVisible: boolean = false;

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



  addToDatabase(){
    let value = this.formController.value;
    this.editFormVisible = false;
    if(this.spell.id == 0) {
      let tmp: Spell = new Spell(0,value['name'],value['description'],value['source'],
        new Price(value['price']['value'], value['price']['coin']),new Array<CasterPossibility>(),value['levell']);
      this.spell = tmp;
      console.log(this.spell);
      return this.fetchData.postSpell(tmp).subscribe(() => {
        this.parent.spells.push(this.spell);
        this.parent.table.renderRows();
      });
    } else {
      let index = this.parent.spells.indexOf(this.spell);
      this.spell = new Spell(this.spell.id,value['name'],value['description'],value['source'],
        new Price(value['price']['value'], value['price']['coin']),this.spell.casterPossibility,value['levell'])
      console.log(this.spell);
      return this.fetchData.updateSpell(this.spell).subscribe(() =>{
        this.parent.spells[index] = this.spell;
        this.parent.table.renderRows();
      })
    }

  }

  addNew(){
    if(!this.editFormVisible) {
      this.spell = new Spell();

      this.showTime = true;
      this.parent.expandedElement = null;
      this.editFormVisible = true;
    }else{
      this.editFormVisible  = false;
      this.parent.change.detectChanges();
      this.addNew();
    }
  }

  editForm(element:Spell){
    if(!this.editFormVisible) {
      this.spell = element;
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
      this.parent.expandedElement = null;
      this.editFormVisible = true;
    }else {
      this.editFormVisible  = false;
      this.parent.change.detectChanges();
      this.editForm(element);
    }
  }

}
