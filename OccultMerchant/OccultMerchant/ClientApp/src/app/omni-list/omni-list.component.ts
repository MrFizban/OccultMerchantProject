import { Component, OnInit , ViewChild} from '@angular/core';
import {FetchDataService} from '../fetch-data.service'
import {Weapons, WeaponsType} from "../Items/Weapons";
import {MatTable, MatTableModule} from "@angular/material/table";

import {animate, state, style, transition, trigger} from '@angular/animations';
import {Dice} from '../Items/Dice'
import {Price} from "../Items/Price";
import {any} from "codelyzer/util/function";
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

  public weapons : Weapons[] = new Array<Weapons>();
  public columnList : Array<string> = ['name','dmgM', 'price', 'critical', 'description',  'proficiency', 'range', 'source', 'typeWeapons'];
  public selectedRow: any;
  // boolean per attivare la modifica della riga selezionata
  public edit: boolean = false;
  // booleant per attivare l'aggiunta di una riga;
  public add: boolean = false;


  constructor(public data: FetchDataService) {}

  ngOnInit(): void {

    this.data.giveMeWeapons().subscribe(result => {
      //this.weapons = result;
      result.forEach((value, index) => {
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
    this.edit = !this.edit;
  }

  addWeapons(){
    let weapon : Weapons = new Weapons(this.weapons.length);
    this.weapons.push(weapon)
    this.tab.renderRows();
    this.selectedRow = weapon;
    this.add = true;
    this.edit = true;
  }

  deleteFunction(){
    if(this.add){
      this.weapons.pop();
      this.tab.renderRows();
      this.selectedRow = null;
      this.edit = false;
      this.add = false;
    }
    else if(this.edit)
    {
      let i: number = this.weapons.indexOf(this.selectedRow);
      console.log(this.weapons[i]);
      this.weapons.splice(i,1);
      this.tab.renderRows();
      this.edit = false;
      this.add = false;
    }
  }

  saveFunction(){
    if(this.add){
      this.data.updateWeapons(this.selectedRow).subscribe();
      this.selectedRow = null
      this.add = false;
    }
  }

  cancellFunciotn(){
    if(this.edit){
      this.selectedRow = null;
      this.edit = false;
    }
  }

}
