import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { EditMealComponent } from './edit-meal/edit-meal.component';
import { AdminComponent } from './admin.component';
import { EditMenuComponent } from './edit-menu/edit-menu.component';

const routes: Routes = [
  {path: '', component: AdminComponent},
  {path: 'create', component: EditMealComponent, data: {breadcrumb: 'Create Meal'}},
  {path: 'edit/:id', component: EditMealComponent, data: {breadcrumb: 'Edit Meal'}},
  {path: 'createMenu', component: EditMenuComponent, data: {breadcrumb: 'Create Menu'}},
  {path: 'editMenu/:id', component: EditMenuComponent, data: {breadcrumb: 'Edit Menu'}}
];
@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
