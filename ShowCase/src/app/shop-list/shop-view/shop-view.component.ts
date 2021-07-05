import {AfterViewChecked, ChangeDetectorRef, Component, Input, OnInit} from '@angular/core';
import {Shop} from "../../Items/Shop";
import {ShopListComponent} from "../shop-list.component";
import {Router} from "@angular/router";
import {FetchDataService} from "../../fetch-data.service";

@Component({
  selector: 'app-shop-view',
  templateUrl: './shop-view.component.html',
  styleUrls: ['./shop-view.component.css']
})
export class ShopViewComponent implements OnInit, AfterViewChecked {
  @Input("shop") public shop!: Shop;
  public localShop!: Shop;
  @Input("selected") public selected!: Shop|null;
  @Input("parent") public parent!: ShopListComponent;
  @Input("isNew") public isNew!: boolean;
  public showTabel: boolean = false;
  public addNew: boolean = false;
  @Input("delteting") public deleting!: boolean;
  constructor(public change: ChangeDetectorRef, public router:Router, private fetchData: FetchDataService) { }

  ngOnInit(): void {
    this.localShop =  new Shop(
      this.shop.id,
      this.shop.name,
      this.shop.description,
      this.shop.source,
      this.shop.price,
      this.shop.filter,
      this.shop.space,
      this.shop.isActive,
      this.shop.potionReserv
    )
    //this.change.detectChanges();
  }

  ngAfterViewChecked() {

  }

  selectShop(){
    this.selected = this.selected === this.localShop ? null : this.localShop;
    console.log(this.selected);
  }


  deleteShop() {

    this.fetchData.deleteShop(this.shop).subscribe(()=>{
      this.parent.shops.splice(this.parent.shops.indexOf(this.shop),1);
      this.parent.change.detectChanges();
    });
  }
}
