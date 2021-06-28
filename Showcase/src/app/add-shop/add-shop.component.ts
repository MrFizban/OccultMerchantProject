import {Component, Input, OnInit} from '@angular/core';
import {OmniListComponent} from "../omni-list/omni-list.component";

@Component({
  selector: 'app-add-shop',
  templateUrl: './add-shop.component.html',
  styleUrls: ['./add-shop.component.css']
})
export class AddShopComponent implements OnInit {
  @Input() omniList!: OmniListComponent;
  constructor() { }

  ngOnInit() {
  }

}
