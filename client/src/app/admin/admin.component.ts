import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { IMeal } from '../shared/models/meal';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from '../shop/shop.service';
import { AdminService } from './admin.service';
import { IMenu } from '../shared/models/menu';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  @ViewChild('search', { static: false }) searchTerm: ElementRef;
  meals: IMeal[];
  menus: IMenu[];
  totalCount: number;
  menuCount: number;
  mealShopParams: ShopParams;
  menuShopParams: ShopParams;

  constructor(private shopService: ShopService, private adminService: AdminService) {
    this.mealShopParams = this.shopService.getShopParams();
    this.menuShopParams = this.shopService.getShopParams();
   }

  ngOnInit(): void {
    this.getMeals();
    this.getMenus();
  }

  getMeals() {
    this.shopService.getMealsForManager(this.mealShopParams).subscribe(response => {
      this.meals = response.data;
      this.totalCount = response.count;
    }, error => {
      console.error(error);
    });
  }

  getMenus() {
    this.shopService.getMenusForManager().subscribe(
      (response) => {
        this.menus = response;
        this.menuCount = this.menus.length;
      },
      (error) => console.log(error)
    );
  }

  onPageChanged(event: any) {
    const params = this.shopService.getShopParams();
    if (params.pageNumber !== event) {
      params.pageNumber = event;
      this.shopService.setShopParams(params);
      this.getMeals();
    }
  }

  deleteMeal(id: number) {
    this.adminService.deleteMeal(id).subscribe((response: any) => {
      this.meals.splice(this.meals.findIndex(p => p.id === id), 1);
      this.totalCount--;
    });
  }

  deleteMenu(id: number) {
    this.adminService.deleteMenu(id).subscribe((response: any) => {
      this.menus.splice(this.menus.findIndex(p => p.id === id), 1);
      this.totalCount--;
    });
  }

  onSearch() {
    this.mealShopParams.search = this.searchTerm.nativeElement.value;
    this.mealShopParams.pageNumber = 1;
    this.getMeals();
  }

  onReset() {
    this.searchTerm.nativeElement.value = '';
    this.mealShopParams = new ShopParams();
    this.getMeals();
  }

}

