import {Component, OnInit, ViewChild} from '@angular/core';
import {Item, Shop} from "../Items/Shop";
import {FetchDataService} from "../fetch-data.service";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {ActivatedRoute} from "@angular/router";
import {MatTable} from "@angular/material/table";

@Component({
  selector: 'app-shop-manager',
  templateUrl: './shop-manager.component.html',
  styleUrls: ['./shop-manager.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ShopManagerComponent implements OnInit {

  @ViewChild("idtable",{static:false}) public tab!: MatTable<any>;
  selectedRow: Shop|null = null;
  edit: boolean = false;
  columnList: string[] = ['name', 'space', 'isActive'];

  constructor(public data: FetchDataService) {
    data.getAllShop().subscribe((result: Shop[])=> {
      this.data.shopList =result;
      console.log(this.data.shopList)
    });
  }

  ngOnInit() {

  }


  deleteShop(){
    this.data.deleteShop(this.selectedRow!).subscribe(() =>{
      console.log(this.data.shopList);
      this.data.shopList.splice(this.data.shopList.indexOf(this.selectedRow!),1);
      console.log(this.tab);
      this.tab.renderRows();
      console.log(this.data.shopList);
    });
    this.selectedRow = null;
  }

  updateShop(){
    this.data.updateShop(this.selectedRow!);
    this.edit = false;
  }

  deleteitem(item:Item,shop:Shop){
    this.data.deleteItems(item,shop).subscribe(() =>{
      shop.weaponsItems.splice(shop.weaponsItems.indexOf(item),1);
      this.tab.renderRows();
    });
  }


}
