import { Component, OnInit, Input } from '@angular/core';
import { IMenu } from 'src/app/shared/models/menu';
import { IMeal } from 'src/app/shared/models/meal';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { MenuService } from '../menu.service';

@Component({
  selector: 'app-menu-details',
  templateUrl: './menu-details.component.html',
  styleUrls: ['./menu-details.component.scss']
})
export class MenuDetailsComponent implements OnInit {
  menus: IMenu[];
  meals: IMeal[];
  shopParams = new ShopParams();
  totalCount: number;
  @Input() restaurantSelected: number;

  constructor(private menuService: MenuService) { }

  ngOnInit(): void {
    this.getMeals();
    this.getMenus(this.restaurantSelected);
    this.onRestaurantSelected(this.restaurantSelected);
  }

  getMenus(restaurantSelected: number) {
    this.menuService.getMenus(restaurantSelected).subscribe(
      (response) => {
        this.menus = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => console.log(error)
    );
  }

  onRestaurantSelected(restaurantId: number) {
    this.shopParams.restaurantId = restaurantId;
    this.shopParams.pageNumber = 1;
    this.getMeals();
  }

  onMenuSelected(menuId: number) {
    if (menuId === 0) {
      this.shopParams.menuId = menuId;
      this.shopParams.restaurantId = this.restaurantSelected;
      this.getMeals();
    } else {
    this.shopParams.restaurantId = 0;
    this.shopParams.menuId = menuId;
    this.shopParams.pageNumber = 1;
    this.getMeals();
    }
  }

  getMeals() {
    this.menuService.getMeals(this.shopParams).subscribe(
      (response) => {
        this.meals = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      (error) => console.log(error)
    );
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getMeals();
    }
  }
}
