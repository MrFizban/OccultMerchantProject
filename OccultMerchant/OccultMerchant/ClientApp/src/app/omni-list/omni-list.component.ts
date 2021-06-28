import {AfterViewChecked, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FetchDataService} from '../fetch-data.service'
import {Weapons, WeaponsType} from "../Items/Weapons";
import {MatTable, MatTableModule} from "@angular/material/table";

import {animate, state, style, transition, trigger} from '@angular/animations';
import {Dice} from '../Items/Dice'
import {CoinType, Price} from "../Items/Price";
import {any} from "codelyzer/util/function";
import {FormGroup, FormControl} from "@angular/forms";
import {Item, Shop} from "../Items/Shop";
import {ActivatedRoute} from "@angular/router";
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
  public weapontypeEnum = WeaponsType;
  public weapons : Weapons[] = new Array<Weapons>();
  public columnList : Array<string> = ['name','dmgM', 'price', 'critical', 'range', 'proficiency'];
  public columnDescriptionList : Array<string> = ['description','source', 'typeWeapons'];
  public selectedRow: Weapons;
  private backupRow: Weapons;
  // boolean per attivare la modifica della riga selezionata
  public edit: boolean = false;
  // booleant per attivare l'aggiunta di una riga;
  public add: boolean = false;
  public shop: boolean = false;
  @ViewChild('botton',{static:false}) private editrow: ElementRef;

  public idShop: number = -1;
  public editShop: boolean = false;
  public states = ["2","4", "6", "8", "10", "12", "20", "100"];
  public weaponsFormGroup: FormGroup = new FormGroup(
    {
      "name": new FormControl(''),
      description: new FormControl(''),
      dmgM: new FormGroup({
        number: new FormControl('0'),
        value: new FormControl(state[0])
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

  public shopFormGroup : FormGroup = new FormGroup({
    name: new FormControl(""),
    space: new FormControl(0),
    isActive: new FormControl(false)
  });



  public selectedRowForShop: Set<Item> = new Set<Item>();

  constructor(public data: FetchDataService,private route: ActivatedRoute ) {
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
    this.idShop = Number(this.route.snapshot.paramMap.get('shop'));
    this.editShop = Boolean(this.route.snapshot.paramMap.get('shop'));

    if(this.idShop){
      this.editShopItems();
    }
    console.log("route:\t" + this.idShop + ":" + this.editShop);
  }


  selectRowFunction(element:Weapons){
    if(this.shop){
      this.selectedRowForShop.add(new Item(element.id,element.name, 0));
      console.log(this.selectedRowForShop);
    } else if(!this.edit){
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

  insertWeapons(){
    let weapon : Weapons = new Weapons(this.weapons.length);
    console.log("price:\t" + this.weaponsFormGroup.get(["price","value"]).value)
    weapon.name = this.weaponsFormGroup.get("name").value;
    weapon.description = this.weaponsFormGroup.get("description").value;
    weapon.source = this.weaponsFormGroup.get("source").value;
    weapon.price =  new Price(this.weaponsFormGroup.get(["price","value"]).value,this.weaponsFormGroup.get(["price","coin"]).value )
    weapon.dmgM = new Dice(this.weaponsFormGroup.get(["dmgM","number"]).value,this.weaponsFormGroup.get(["price","value"]).value);
    weapon.critical  = this.weaponsFormGroup.get("critical").value;
    weapon.range = parseInt(this.weaponsFormGroup.get("range").value);
    weapon.typeWeapons = (this.weaponsFormGroup.get("type").value);
    weapon.proficiency = this.weaponsFormGroup.get("proficiency").value;
    console.log(weapon)
    this.data.addWeapons(weapon).subscribe(result =>{
      this.weapons.push(weapon);
      this.tab.renderRows();
      this.add = false;
    })

  }

  deleteFunction() {
    this.data.deleteWeapons(this.selectedRow).subscribe(resut =>{
      let i: number = this.weapons.indexOf(this.selectedRow);
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
    this.weaponsFormGroup = new FormGroup(
      {
        "name": new FormControl(''),
        description: new FormControl(''),
        dmgM: new FormGroup({
          number: new FormControl('0'),
          value: new FormControl(state[0])
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
      this.data.updateWeapond(this.selectedRow);
      this.selectedRow = null
      this.edit = false;
    }
  }


  addShop(){
    let shopTmp : Shop = new Shop(0);
    shopTmp.name = this.shopFormGroup.get('name').value
    shopTmp.space = this.shopFormGroup.get('space').value
    shopTmp.isActive = this.shopFormGroup.get('isActive').value
    this.selectedRowForShop.forEach(value => shopTmp.weaponsItems.push(new Item(value.id,value.name,0)));
    this.data.insertShop(shopTmp).subscribe();
  }

  editShopItems(){
    this.shop = this.editShop;
  this.shopFormGroup  = new FormGroup({
      name: new FormControl(this.data.shopList[this.idShop].name),
      space: new FormControl((this.data.shopList[this.idShop].space)),
      isActive: new FormControl(this.data.shopList[this.idShop].isActive)
    });
    this.data.shopList[this.idShop].weaponsItems.forEach(value => this.selectedRowForShop.add(value))
  }

  updateShop(){
    console.log("update shop")
    let shop: Shop = new Shop(this.data.shopList[this.idShop].id);
    this.selectedRowForShop.forEach(value => shop.weaponsItems.push(value));
    this.data.addItemToShop(shop);

  }

  cancellShop(){
    this.selectedRowForShop.clear();
    this.shop = false;
  }


  deleteitem(item:Item,shop:Shop){

  }

}
