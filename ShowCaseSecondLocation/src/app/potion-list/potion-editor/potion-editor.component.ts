import { Component, OnInit, Input } from '@angular/core';
import {FormControl, FormGroup, FormGroupName} from "@angular/forms";
import {CoinType, Price} from "../../Items/Base";
import {Potion, SpellName} from "../../Items/Potion";
import {FetchDataService} from "../../fetch-data.service";
import {PotionListComponent} from "../potion-list.component";
import {ActivatedRoute, Router} from "@angular/router";

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
  private makeRoute = false;
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

  constructor(public fetchData:FetchDataService, private router:Router) { }

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

  addToDatabaseWrapper(){
     this.makeRoute = true;
      this.addToDatabase();
   }

  // aggiunge la nuova poziona nell database
  addToDatabase(){
    let value = this.formController.value;
    this.editFormVisible = false;
    //se la pozione è nuova ha id == 0
    if(this.potion.id == 0) {
      let tmp: Potion = new Potion(0, value['name'], value['description'], value['source'],
        new Price(value['price']['value'], value['name']['coin'],), new SpellName(0, ""), value['levell']);
      this.potion = tmp;
      this.parent.potions[this.parent.potions.length -1] = this.potion;
      return this.fetchData.postPotion(tmp).subscribe((res) => {
        this.parent.potions[this.parent.potions.length -1].id = res;
        console.log(res)
        this.potion.id = res.id;
        this.parent.table.renderRows();
        if(this.makeRoute) {
          this.router.navigate(['spell', {id: this.potion.id}])
        }
      });
    } else {
      let index = this.parent.potions.indexOf(this.potion);
      this.potion = new Potion(this.potion.id, value['name'], value['description'], value['source'],
        new Price(value['price']['value'], value['name']['coin'],), new SpellName(3, value['spellName']), value['levell']);
      this.parent.potions[index] = this.potion

      return console.log( this.fetchData.updatePotion(this.potion).subscribe((res) =>{
        this.parent.table.renderRows();
        if(this.makeRoute) {
          this.router.navigate(['spell', {id: this.potion.id}])
        }
      }));

    }

  }

  //apre la form per inserire pozion nuova
  addNew(){
    if(!this.editFormVisible) {
      this.potion = new Potion();
      this.parent.expandedElement = null;
      this.editFormVisible = true;
      this.parent.potions.push(this.potion)
    }else{
      //resetta la fomr
      this.parent.potions.splice(this.parent.potions.length-1,1);
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

  cancell() {
    this.parent.potions.splice(this.parent.potions.length-1,1);
    this.editFormVisible = false;
  }



}
