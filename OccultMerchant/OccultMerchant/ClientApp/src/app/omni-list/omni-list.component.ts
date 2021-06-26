import {AfterViewChecked, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FetchDataService} from '../fetch-data.service'
import {Weapons, WeaponsType} from "../Items/Weapons";
import {MatTable, MatTableModule} from "@angular/material/table";

import {animate, state, style, transition, trigger} from '@angular/animations';
import {Dice} from '../Items/Dice'
import {CoinType, Price} from "../Items/Price";
import {any} from "codelyzer/util/function";
import {FormGroup, FormControl} from "@angular/forms";
@Component({
  selector: 'app-omni-list',
  templateUrl: './omni-list.component.html',
  styleUrls: ['./omni-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class OmniListComponent implements OnInit {

  @ViewChild("tableId",{static:false}) public tab: MatTable<any>;
  public coinEnum = CoinType;
  public weapons : Weapons[] = new Array<Weapons>();
  public columnList : Array<string> = ['name','dmgM', 'price', 'critical', 'range', 'proficiency'];
  public selectedRow: Weapons;
  private backupRow: Weapons;
  // boolean per attivare la modifica della riga selezionata
  public edit: boolean = false;
  // booleant per attivare l'aggiunta di una riga;
  public add: boolean = false;
  @ViewChild('botton',{static:false}) private editrow: ElementRef;

  public states = ["d2","d4", "d6", "d8", "d10", "d12", "d20", "d100"];
  public formController: FormGroup = new FormGroup(
    {
      "name": new FormControl(''),
      description: new FormControl(''),
      critical: new FormControl(''),
      range: new FormControl(''),
      source: new FormControl(''),
      proficiency: new FormControl(''),
      type: new FormControl(''),
      category: new FormControl(''),
      price: new FormGroup({
        value: new FormControl(),
        coin: new FormControl()
      })
    }
  );
  constructor(public data: FetchDataService, ) {
  }

  ngOnInit(): void {

    this.data.giveMeWeapons().subscribe(result => {
      //this.weapons = result;
      result.forEach((value, index,array) => {
        let tmp : Weapons = new Weapons(value['id'],value['name'],value['description'],value['source'],new Price(value['price'].value,value['price'].coin),
          new Dice(value['dmgM'].number,value['dmgM'].value), value['critical'],value['typeWeapons'],value['range'],value['proficiency']);
        this.weapons.push(tmp);
      });
      console.log(this.weapons);
    }, error => console.error(error));
    console.log("Sono Vivo");

  }


  selectRowFunction(element:Weapons){
    if(!this.edit){
      this.selectedRow = this.selectedRow === element ? null : element
    }
  }

  editWeapons(){
    if(!this.edit){
      this.edit = true;
      console.log(this.selectedRow);
      this.backupRow = this.selectedRow.clone();
      console.log(this.backupRow);

    } else {
      this.edit = false;
      console.log(this.selectedRow);
      console.log(this.backupRow);
     this.weapons[this.weapons.indexOf(this.selectedRow)]= this.backupRow.clone();
     this.tab.renderRows();
    }
  }

  addWeapons(){
    let weapon : Weapons = new Weapons(this.weapons.length);
    console.log(weapon);
    this.add = true;
  }

  deleteFunction(){
    if(this.edit)
    {
      let i: number = this.weapons.indexOf(this.selectedRow);
      console.log(this.weapons[i]);
      this.weapons.splice(i,1);
      this.tab.renderRows();
      this.edit = false;
      this.add = false;
    }
    if(this.add){
      this.add = false;
    }
  }

  saveFunction(){
    if(this.edit){
      console.log("saving");
      this.data.updateWeapond(this.selectedRow);
      this.selectedRow = null
      this.edit = false;
    }
  }



  cancellFunciotn(){
    if(this.edit){
      this.selectedRow = null;
      this.edit = false;
    }
  }

}
