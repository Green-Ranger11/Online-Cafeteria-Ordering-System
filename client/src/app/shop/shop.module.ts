import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { MealItemComponent } from './meal-item/meal-item.component';
import { SharedModule } from '../shared/shared.module';
import { MealDetailsComponent } from './meal-details/meal-details.component';
import { ShopRoutingModule } from './shop-routing.module';



@NgModule({
  declarations: [ShopComponent, MealItemComponent, MealDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    ShopRoutingModule
  ],
  exports: [MealItemComponent]
})
export class ShopModule { }
