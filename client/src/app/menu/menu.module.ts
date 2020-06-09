import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MenuRoutingModule } from './menu-routing.module';
import { MenuComponent } from './menu.component';
import { MenuDetailsComponent } from './menu-details/menu-details.component';
import { MealItemComponent } from '../shop/meal-item/meal-item.component';
import { ShopModule } from '../shop/shop.module';



@NgModule({
  declarations: [MenuComponent, MenuDetailsComponent],
  imports: [
    CommonModule,
    SharedModule,
    MenuRoutingModule,
    ShopModule
  ]
})
export class MenuModule { }
