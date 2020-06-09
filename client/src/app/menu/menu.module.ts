import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { MenuRoutingModule } from './menu-routing.module';
import { MenuComponent } from './menu.component';



@NgModule({
  declarations: [MenuComponent],
  imports: [
    CommonModule,
    SharedModule,
    MenuRoutingModule
  ]
})
export class MenuModule { }
