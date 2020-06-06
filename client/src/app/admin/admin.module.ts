import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { EditMealComponent } from './edit-meal/edit-meal.component';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
import { EditMealFormComponent } from './edit-meal-form/edit-meal-form.component';
import { EditMealPhotosComponent } from './edit-meal-photos/edit-meal-photos.component';



@NgModule({
  declarations: [AdminComponent, EditMealComponent, EditMealComponent, EditMealFormComponent, EditMealPhotosComponent],
  imports: [
    CommonModule,
    SharedModule,
    AdminRoutingModule
  ]
})
export class AdminModule { }
