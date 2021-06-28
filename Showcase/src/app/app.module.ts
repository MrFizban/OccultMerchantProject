
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { OmniListComponent } from './omni-list/omni-list.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from '@angular/material/table';
import {MatCardModule} from '@angular/material/card';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import { AddWeaponFormComponent } from './add-weapon-form/add-weapon-form.component';
import {AddShopComponent} from './add-shop/add-shop.component';
import { ShopManagerComponent } from './shop-manager/shop-manager.component';
import {MatIconModule} from '@angular/material/icon';
import { ShowcaseComponent } from './showcase/showcase.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    OmniListComponent,
    AddWeaponFormComponent,
    AddShopComponent,
    ShopManagerComponent,
    ShowcaseComponent
  ],
    imports: [
        BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            {path: '', component: OmniListComponent, pathMatch: 'full'},
            {path: 'omni-list', component: OmniListComponent},
            {path: 'shop-manager', component: ShopManagerComponent},
            {path: 'app-showcase', component: ShowcaseComponent},
            {path: 'omni-list/:shop/:edit', component: OmniListComponent},
        ]),
        BrowserAnimationsModule,
        MatTableModule,
        MatCardModule,
        MatButtonModule,
        MatFormFieldModule,
        MatToolbarModule,
        MatInputModule,
        ReactiveFormsModule,
        MatSelectModule,
        MatIconModule
    ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
