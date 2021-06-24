import { Component, OnInit } from '@angular/core';
import {FetchDataService} from '../fetch-data.service'
@Component({
  selector: 'app-omni-list',
  templateUrl: './omni-list.component.html',
  styleUrls: ['./omni-list.component.css']
})
export class OmniListComponent implements OnInit {

  constructor(public data: FetchDataService) {
    this.data.giveMeWeapons();
    console.log("Sono Vivo");
    console.log(this.data.weapons);
  }

  ngOnInit(): void {
  }



}
