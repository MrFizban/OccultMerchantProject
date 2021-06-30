import { Component, OnInit } from '@angular/core';
import {FetchDataService} from "../fetch-data.service";

@Component({
  selector: 'app-shop-list',
  templateUrl: './shop-list.component.html',
  styleUrls: ['./shop-list.component.css']
})
export class ShopListComponent implements OnInit {

  constructor(private fetcData:FetchDataService) { }

  ngOnInit(): void {

  }



}
