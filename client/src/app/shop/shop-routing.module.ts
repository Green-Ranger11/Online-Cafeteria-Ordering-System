import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { ShopComponent } from './shop.component';
import { MealDetailsComponent } from './meal-details/meal-details.component';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: ':id', component: MealDetailsComponent, data: {breadcrumb: {alias: 'mealDetails'}}}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
