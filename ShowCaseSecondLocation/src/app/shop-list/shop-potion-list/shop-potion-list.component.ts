import {
  Component,
  ElementRef,
  OnInit,
  ViewChild,
  OnChanges,
  AfterContentChecked,
  AfterViewChecked,
  AfterContentInit,
  ChangeDetectorRef, AfterViewInit, Input
} from '@angular/core';
import {FetchDataService} from "../../fetch-data.service";
import {Potion, SpellName} from "../../Items/Potion";
import {CoinType, Price} from "../../Items/Base";
import {MatTable} from "@angular/material/table";
import {MatCard} from "@angular/material/card";
import {animate, state, style, transition, trigger} from "@angular/animations";
import {isExtended} from "@angular/compiler-cli/src/ngtsc/shims/src/expando";
import {Position} from "../../Items/Position";
import {ActivatedRoute} from "@angular/router";
import {FormControl} from "@angular/forms";
import {PotionStock, Shop} from "../../Items/Shop";
import {ShopEditorComponent} from "../shop-editor/shop-editor.component";

@Component({
  selector: 'app-shop-potion-list',
  templateUrl: './shop-potion-list.component.html',
  styleUrls: ['./shop-potion-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class ShopPotionListComponent implements OnInit, OnChanges {

  public potions: Array<Potion> = new Array<Potion>();
  public columnList: Array<string> = ['stock', 'name', 'spellName', 'levell', 'price',];

  @ViewChild("idTable") table!: MatTable<any>;
  @ViewChild("card", {read: ElementRef}) card!: ElementRef;
  @ViewChild("card2", {read: ElementRef}) card2!: ElementRef;
  public pos: Position = {left: 1000, top: 1000};
  public show: boolean = false;
  expandedElement: Potion | null = null;
  public newPotion: Potion = new Potion(0);
  //public quantity: FormControl = new FormControl("???");
  public quantity: any;
  @Input("stock") stock!: Shop;
  @Input("parent") parent!: ShopEditorComponent;
  public IdsStock: Array<number> = new Array<number>();


  constructor(public fetcData: FetchDataService, public change: ChangeDetectorRef, private activatedRoute: ActivatedRoute) {

  }

  ngOnInit(): void {

    this.fetcData.getAllPotion().subscribe((result: Array<any>) => {
      console.log(this.stock)
      console.log(this.stock.potionReserv)
      console.log("result:\t" + result.length + "\t" + (this.potions.length + this.IdsStock.length))
      if (result.length != (this.potions.length + this.IdsStock.length)) {

        result.forEach(value => {
          if (!this.IdsStock.includes(value['id'])) {
            this.potions.push(new Potion(
              value['id'],
              value['name'],
              value['description'],
              value['source'],
              new Price(
                value['price']['value'],
                value['price']['coin']),
              new SpellName(
                value['spellName']['id'],
                value['spellName']['name']),
              value['levell']));

            this.stock.potionReserv.forEach(value => this.IdsStock.push(value['potion']['id']));
          }
        })

        this.table.renderRows();
      }

    });

  }

  ngOnChanges() {
    console.log("ciao")
  }


  extendelement(element: Potion) {

    if (this.expandedElement != element) {
      this.expandedElement = element;

    } else if (this.expandedElement === element) {
      this.expandedElement = null;
    }
  }


  addPotion(element: Potion) {
    let tmpShop = new Shop();
    tmpShop.potionReserv.push(new PotionStock(element, this.quantity));
    tmpShop.id = this.stock.id;
    console.log(tmpShop)
    console.log( this.stock.potionReserv)
    this.stock.potionReserv.push(new PotionStock(element, this.quantity));
    console.log( this.stock.potionReserv)
    this.fetcData.updateShopPotion(tmpShop).subscribe(value => {
        this.potions.splice(this.potions.indexOf(element), 1);
        this.table.renderRows();
        this.parent.table.renderRows();
        this.quantity = 0;
      }
    );
  }
}
