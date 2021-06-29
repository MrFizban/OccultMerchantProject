import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { WeaponsListComponent } from './weapons-list/weapons-list.component';
import { ShopListComponent } from './shop-list/shop-list.component';
import { SpellListComponent } from './spell-list/spell-list.component';
import { PotionListComponent } from './potion-list/potion-list.component';

@NgModule({
  declarations: [
    AppComponent,
    WeaponsListComponent,
    ShopListComponent,
    SpellListComponent,
    PotionListComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
