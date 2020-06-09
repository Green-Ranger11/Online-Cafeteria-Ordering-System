import { Component, OnInit } from '@angular/core';
import { IMenu } from '../shared/models/menu';
import { IMeal } from '../shared/models/meal';
import { MenuService } from './menu.service';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
