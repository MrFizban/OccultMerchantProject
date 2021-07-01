import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { WeaponsListComponent } from './weapons-list/weapons-list.component';
import { ShopListComponent } from './shop-list/shop-list.component';
import { SpellListComponent } from './spell-list/spell-list.component';
import { PotionListComponent } from './potion-list/potion-list.component';
import { HttpClientModule } from '@angular/common/http';
import { FlexLayoutModule } from '@angular/flex-layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatCardModule} from "@angular/material/card";
import { ShopEditorComponent } from './shop-list/shop-editor/shop-editor.component';
import { ShopViewComponent } from './shop-list/shop-view/shop-view.component';
import { PotionEditorComponent } from './potion-list/potion-editor/potion-editor.component';
import {MatButtonModule} from "@angular/material/button";
import {ReactiveFormsModule} from "@angular/forms";
import {MatTableModule} from "@angular/material/table";
import {MatIconModule} from "@angular/material/icon";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatSelectModule} from "@angular/material/select";
import { MatInputModule } from '@angular/material/input';
import {MatToolbarModule} from "@angular/material/toolbar";
import { AppRoutingModule } from './app-routing.module';
import { SpellEditorComponent } from './spell-list/spell-editor/spell-editor.component';
import { IAmErrorComponent } from './iam-error/iam-error.component';

@NgModule({
  declarations: [
    AppComponent,
    WeaponsListComponent,
    ShopListComponent,
    SpellListComponent,
    PotionListComponent,
    ShopEditorComponent,
    ShopViewComponent,
    PotionEditorComponent,
    SpellEditorComponent,
    IAmErrorComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FlexLayoutModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatButtonModule,
    ReactiveFormsModule,
    MatTableModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatToolbarModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
