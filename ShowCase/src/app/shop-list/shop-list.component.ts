import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FetchDataService} from "../fetch-data.service";
import {PotionStock, Shop} from "../Items/Shop";
import {Filter, Price} from "../Items/Base";
import {Potion, SpellName} from "../Items/Potion";

@Component({
  selector: 'app-shop-list',
  templateUrl: './shop-list.component.html',
  styleUrls: ['./shop-list.component.css']
})
export class ShopListComponent implements OnInit {

  public shops: Array<Shop> = new Array<Shop>();
  public selectedShop: Shop|null = null;
  public deleting: boolean = false;

  constructor(private fetcData: FetchDataService, public change: ChangeDetectorRef) {
  }

  ngOnInit(): void {
    this.fetcData.getAllShop().subscribe((result: Array<Shop>) => {
      console.log(result)
      result.forEach((value, index) => {
        this.shops.push(new Shop(
          value['id'],
          value['name'],
          value['description'],
          value['source'],
          new Price(
            value['price']['value'],
            value['price']['coin']),
          new Filter(),
          value['space'],
          value['isActive']
        ));

        value['potionReserv'].forEach(valuePotion => {
          this.shops[index].potionReserv.push(new PotionStock(
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
      })


     // console.log(this.shops)

    });
  }



}
