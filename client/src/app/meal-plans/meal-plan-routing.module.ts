import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MealPlansComponent } from './meal-plans.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {path: '', component: MealPlansComponent},
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class MealPlanRoutingModule { }
