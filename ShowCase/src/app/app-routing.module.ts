import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PotionListComponent } from './potion-list/potion-list.component';
import { ShopListComponent } from './shop-list/shop-list.component'
const routes: Routes = [
  { path: '', component: PotionListComponent },
  { path: 'shop', component: ShopListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
