import {AfterContentInit, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FetchDataService} from "../../fetch-data.service";
import {ActivatedRoute} from "@angular/router";
import {PotionStock, Shop} from "../../Items/Shop";
import {Filter, Price} from "../../Items/Base";
import {Potion, SpellName} from "../../Items/Potion";
import {Position} from "../../Items/Position";
import {MatTable} from "@angular/material/table";
import {FormControl, FormGroup} from "@angular/forms";
import {PotionListComponent} from "../../potion-list/potion-list.component";

@Component({
  selector: 'app-shop-editor',
  templateUrl: './shop-editor.component.html',
  styleUrls: ['./shop-editor.component.css']
})
export class ShopEditorComponent implements OnInit, AfterContentInit {

  public shop: Shop = new Shop();
  columnList: string[] = ['name', 'levell', 'price', 'quantity'];
  public expandedElement: PotionStock | null = null;
  public pos: Position = {left: 1000, top: 1000};

  @ViewChild("card", {read: ElementRef}) card!: ElementRef;
  @ViewChild("card2", {read: ElementRef}) card2!: ElementRef;
  @ViewChild("idTablePotion") public table!: MatTable<any>;
  @ViewChild("potionListComponet") public potioList!: PotionListComponent;
  public editing: boolean = false;
  public open!: boolean;


  public name: FormControl = new FormControl("");
  public description: FormControl = new FormControl("");
  public space: FormControl = new FormControl("");
  public isActive: FormControl = new FormControl("");

  constructor(private fetchData: FetchDataService, private activatedRoute: ActivatedRoute, public change: ChangeDetectorRef) {

  }

  ngOnInit(): void {

    this.activatedRoute.paramMap.subscribe((param) => {
      if (param.get('idShop')) {
        this.fetchData.getShop(parseInt(param.get('idShop')!)).subscribe((result: Shop) => {
          this.shop = new Shop(
            result['id'],
            result['name'],
            result['description'],
            result['source'],
            new Price(
              result['price']['value'],
              result['price']['coin']),
            new Filter(),
            result['space'],
            result['isActive']
          );

          result['potionReserv'].forEach(valuePotion => {
            this.shop.potionReserv.push(new PotionStock(
              new Potion(
                valuePotion['potion']['id'],
                valuePotion['potion']['name'],
                valuePotion['potion']['description'],
                valuePotion['potion']['source'],
                new Price(
                  valuePotion['potion']['price']['value'],
                  valuePotion['potion']['price']['coin']),
                new SpellName(
                  valuePotion['potion']['spellName']['id'],
                  valuePotion['potion']['spellName']['name']),
                valuePotion['potion']['levell']), valuePotion['quantity'])
            )
          });
          console.log(this.shop)

            this.name.setValue(  this.shop.name);
          this.description.setValue( this.shop.description);
            this.space.setValue( this.shop.space);
              this.isActive.setValue( this.shop.isActive);

          console.log("on init" +this.shop.isActive);
        });
      }


    });
  }

  ngAfterContentInit() {

  }

  swicthshow() {
    if (this.expandedElement != null) {
      this.change.detectChanges();
      this.pos.left -= this.card2.nativeElement.offsetLeft;
      this.pos.top -= this.card2.nativeElement.offsetTop;
      this.change.detectChanges();
      this.pos.left += this.card.nativeElement.offsetLeft - 100;
      this.pos.top += this.card.nativeElement.offsetTop;
      this.change.detectChanges();


    }
  }

  extendelement(element: PotionStock) {

    if (this.expandedElement != element) {
      this.expandedElement = element;
      this.swicthshow()

    } else if (this.expandedElement === element) {
      this.expandedElement = null;
    }
  }

  deletePotion(potion: PotionStock) {
    this.fetchData.deletePotionFromStock(this.shop.id, potion.potion.id).subscribe(() => {
      this.shop.potionReserv.splice(this.shop.potionReserv.indexOf(this.expandedElement!), 1);
      this.expandedElement = null;
      this.potioList.potions.push(potion.potion);
      this.potioList.table.renderRows();
      this.table.renderRows();
    });
  }

  updateShopPotion() {
    this.fetchData.updateShopPotion(this.shop).subscribe();

  }

  updateShop() {

    this.shop.name = this.name.value;
    this.shop.description = this.description.value;
    this.shop.space = this.space.value;
    this.shop.isActive = this.isActive.value;
    this.shop.space =  this.space.value;
    console.log("shopTmp")
    console.log(this.shop)
    this.fetchData.updateShop(this.shop).subscribe();
    this.change.detectChanges();
    this.editing = false
  }
}

