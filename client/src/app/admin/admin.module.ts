import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin.component';
import { EditMealComponent } from './edit-meal/edit-meal.component';
import { SharedModule } from '../shared/shared.module';
import { AdminRoutingModule } from './admin-routing.module';
import { EditMealFormComponent } from './edit-meal-form/edit-meal-form.component';
import { EditMealPhotosComponent } from './edit-meal-photos/edit-meal-photos.component';
import { EditMenuComponent } from './edit-menu/edit-menu.component';
import { EditMenuFormComponent } from './edit-menu-form/edit-menu-form.component';
import { EditIngrediantsComponent } from './edit-ingrediants/edit-ingrediants.component';

@NgModule({
  declarations: [
    AdminComponent,
    EditMealComponent,
    EditMealComponent,
    EditMealFormComponent,
    EditMealPhotosComponent,
    EditMenuComponent,
    EditMenuFormComponent,
    EditIngrediantsComponent,
  ],
  imports: [CommonModule, SharedModule, AdminRoutingModule],
})
export class AdminModule {}
