import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PotionListComponent } from './potion-list/potion-list.component';
import { ShopListComponent } from './shop-list/shop-list.component'
import { SpellListComponent } from "./spell-list/spell-list.component";
import {IAmErrorComponent} from "./iam-error/iam-error.component";
const routes: Routes = [
  {path: '', redirectTo:"/shop",pathMatch: 'full'},
  { path: 'shop', component: ShopListComponent },
  { path: 'potion', component: PotionListComponent },
  { path: 'potion/:id', component: PotionListComponent },
  { path: 'spell', component: SpellListComponent },
  { path: 'spell/:id', component: SpellListComponent },
  { path: '**', component: IAmErrorComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
