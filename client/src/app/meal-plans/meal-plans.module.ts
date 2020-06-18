import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealPlanRoutingModule } from './meal-plan-routing.module';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    MealPlanRoutingModule,
    SharedModule
  ]
})
export class MealPlansModule { }
