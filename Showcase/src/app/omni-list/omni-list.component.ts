import {AfterViewChecked, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FetchDataService} from '../fetch-data.service'
import {Weapons, WeaponsType} from "../Items/Weapons";
import {MatTable, MatTableModule} from "@angular/material/table";

import {animate, state, style, transition, trigger} from '@angular/animations';
import {Dice} from '../Items/Dice'
import {CoinType, Price} from "../Items/Price";

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

  @ViewChild("tableId",{static:false}) public tab!: MatTable<any>;
  public coinEnum = CoinType;
  public weapontypeEnum = WeaponsType;
  public weapons : Weapons[] = new Array<Weapons>();
  public columnList : Array<string> = ['name','dmgM', 'price', 'critical', 'range', 'proficiency'];
  public columnDescriptionList : Array<string> = ['description','source', 'typeWeapons'];
  public selectedRow!: Weapons | null;
  private backupRow!: Weapons;
  // boolean per attivare la modifica della riga selezionata
  public edit: boolean = false;
  // booleant per attivare l'aggiunta di una riga;
  public add: boolean = false;
  @ViewChild('botton',{static:false}) private editrow!: ElementRef;

  public diceSet = ["2","4", "6", "8", "10", "12", "20", "100"];
  public formController: FormGroup = new FormGroup(
    {
      "name": new FormControl(''),
      description: new FormControl(''),
      dmgM: new FormGroup({
        number: new FormControl('0'),
        value: new FormControl(this.diceSet[0])
      }),
      critical: new FormControl(''),
      range: new FormControl('0'),
      source: new FormControl(''),
      proficiency: new FormControl(''),
      type: new FormControl(WeaponsType.piercing),
      category: new FormControl(''),
      price: new FormGroup({
        value: new FormControl('0'),
        coin: new FormControl(CoinType.CopperCoin)
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
      this.backupRow = this.selectedRow!.clone();
      console.log(this.backupRow);

    } else {
      this.edit = false;
      console.log(this.selectedRow);
      console.log(this.backupRow);
     this.weapons[this.weapons.indexOf(this.selectedRow!)]= this.backupRow.clone();
     this.tab.renderRows();
    }
  }

  addWeapons(){
    this.add = true;

  }

  insertWeapons(){
    let weapon : Weapons = new Weapons(this.weapons.length);
    console.log("price:\t" + this.formController.get(["price","value"])!.value)
    weapon.name = this.formController.get("name")!.value;
    weapon.description = this.formController.get("description")!.value;
    weapon.source = this.formController.get("source")!.value;
    weapon.price =  new Price(this.formController.get(["price","value"])!.value,this.formController.get(["price","coin"])!.value )
    weapon.dmgM = new Dice(this.formController.get(["dmgM","number"])!.value,this.formController.get(["price","value"])!.value);
    weapon.critical  = this.formController.get("critical")!.value;
    weapon.range = parseInt(this.formController.get("range")!.value);
    weapon.typeWeapons = (this.formController.get("type")!.value);
    weapon.proficiency = this.formController.get("proficiency")!.value;
    console.log(weapon)
    this.data.addWeapons(weapon).subscribe(result =>{
      this.weapons.push(weapon);
      this.tab.renderRows();
      this.add = false;
    })

  }

  deleteFunction() {
    this.data.deleteWeapons(this.selectedRow!).subscribe(resut =>{
      let i: number = this.weapons.indexOf(this.selectedRow!);
      console.log(this.weapons[i]);
      this.weapons.splice(i, 1);
      this.tab.renderRows();
      this.selectedRow = null;
      if (this.edit)
      {
        this.edit = false;
      }
      console.log("deleting");
    })


  }

  resetAddFunction(){
    this.formController = new FormGroup(
      {
        "name": new FormControl(''),
        description: new FormControl(''),
        dmgM: new FormGroup({
          number: new FormControl('0'),
          value: new FormControl(this.diceSet[0])
        }),
        critical: new FormControl(''),
        range: new FormControl('0'),
        source: new FormControl(''),
        proficiency: new FormControl(''),
        type: new FormControl(''),
        category: new FormControl(''),
        price: new FormGroup({
          value: new FormControl('0'),
          coin: new FormControl(CoinType.CopperCoin)
        })
      }
    );
    this.add = false;
  }

  saveFunction(){
    if(this.edit){
      console.log("saving");
      this.data.updateWeapond(this.selectedRow!);
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
