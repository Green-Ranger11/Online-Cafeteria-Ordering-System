import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { MealItemComponent } from './meal-item/meal-item.component';



@NgModule({
  declarations: [ShopComponent, MealItemComponent],
  imports: [
    CommonModule
  ],
  exports: [
    ShopComponent
  ]
})
export class ShopModule { }
