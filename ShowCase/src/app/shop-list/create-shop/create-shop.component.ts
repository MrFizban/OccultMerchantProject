import {Component, Input, OnInit} from '@angular/core';
import {FormControl, FormGroup} from "@angular/forms";
import {Shop} from "../../Items/Shop";
import {FetchDataService} from "../../fetch-data.service";
import {ShopListComponent} from "../shop-list.component";


@Component({
  selector: 'app-create-shop',
  templateUrl: './create-shop.component.html',
  styleUrls: ['./create-shop.component.css']
})
export class CreateShopComponent implements OnInit {
  public editFormVisible: boolean = false;

  @Input("paretn") parent!: ShopListComponent;
  public name: FormControl = new FormControl("");
  public description: FormControl = new FormControl("");
  public space: FormControl = new FormControl("");
  public isActive: FormControl = new FormControl(false);

  constructor(private fethcData: FetchDataService) {
  }

  ngOnInit(): void {
  }

  createShop() {
    let tmpShop = new Shop()
    tmpShop.name = this.name.value
    tmpShop.description = this.description.value
    tmpShop.space = parseInt(this.space.value);
    if (!tmpShop.space) {
      tmpShop.space = 0;
    }
    tmpShop.isActive = this.isActive.value
    console.log(tmpShop)
    this.fethcData.postShop(tmpShop).subscribe(result => {
      tmpShop.id = result['id'];
      console.log("adding new");
      console.log(result);
      this.parent.shops.push(tmpShop);
      this.parent.change.detectChanges();
      this.editFormVisible = false;
    });
    this.resttForm();
  }

  cancell() {
    this.editFormVisible = false;
    this.resttForm();
  }

  private resttForm() {
    this.name.setValue("");
    this.description.setValue("");
    this.space.setValue(0);
    this.isActive.setValue(false);
  }
}
