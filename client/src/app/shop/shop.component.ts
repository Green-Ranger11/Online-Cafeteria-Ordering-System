import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IMeal } from '../shared/models/meal';
import { ShopService } from './shop.service';
import { IRestaurant } from '../shared/models/restaurants';
import { IType } from '../shared/models/mealType';
import { ShopParams } from '../shared/models/shopParams';
import { IMenu } from '../shared/models/menu';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  meals: IMeal[];
  restaurants: IRestaurant[];
  menus: IMenu[];
  types: IType[];
  shopParams = new ShopParams();
  totalCount: number;
  sortOption = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];

  constructor(private shopService: ShopService) {}

  ngOnInit() {
    this.getMeals();
    this.getRestaurants();
    this.getTypes();
    this.getMenus();
  }

  getMeals() {
    this.shopService.getMeals(this.shopParams).subscribe(
      (response) => {
        this.meals = response.data;
        this.shopParams.pageNumber = response.pageIndex;
        this.shopParams.pageSize = response.pageSize;
        this.totalCount = response.count;
      },
      (error) => console.log(error)
    );
  }

  getRestaurants() {
    this.shopService.getRestaurants().subscribe(
      (response) => {
        this.restaurants = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => console.log(error)
    );
  }

  getMenus() {
    this.shopService.getMenus().subscribe(
      (response) => {
        this.menus = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => console.log(error)
    );
  }

  getTypes() {
    this.shopService.getTypes().subscribe(
      (response) => {
        this.types = [{ id: 0, name: 'All' }, ...response];
      },
      (error) => console.log(error)
    );
  }

  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;
    this.getMeals();
  }

  onRestaurantSelected(restaurantId: number) {
    this.shopParams.restaurantId = restaurantId;
    this.shopParams.pageNumber = 1;
    this.getMeals();
  }

  onMenuSelected(menuId: number) {
    this.shopParams.menuId = menuId;
    this.shopParams.pageNumber = 1;
    this.getMeals();
  }

  onSortSelected(sort: string) {
    this.shopParams.sort = sort;
    this.getMeals();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event) {
      this.shopParams.pageNumber = event;
      this.getMeals();
    }
  }

  onSearch() {
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getMeals();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getMeals();
  }
}
