import {Component, Input, OnInit} from '@angular/core';
import {CoinType} from "../Items/Price";
import {WeaponsType} from "../Items/Weapons";
import {FormControl, FormGroup} from "@angular/forms";
import {state} from "@angular/animations";
import {OmniListComponent} from "../omni-list/omni-list.component";

@Component({
  selector: 'app-add-weapon-form',
  templateUrl: './add-weapon-form.component.html',
  styleUrls: ['./add-weapon-form.component.css']
})
export class AddWeaponFormComponent implements OnInit {

  @Input()  omniList: OmniListComponent
  constructor() {

  }

  ngOnInit() {

  }

}
