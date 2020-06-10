import { Component, OnInit } from '@angular/core';
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
  meals: IMeal[];
  menus: IMenu[];
  totalCount: number;
  shopParams: ShopParams;

  constructor(private shopService: ShopService, private adminService: AdminService) {
    this.shopParams = this.shopService.getShopParams();
   }

  ngOnInit(): void {
    this.getMeals();
    this.getMenus();
  }

  getMeals() {
    this.shopService.getMeals(this.shopParams).subscribe(response => {
      this.meals = response.data;
      this.totalCount = response.count;
    }, error => {
      console.error(error);
    });
  }

  getMenus() {
    this.shopService.getMenus().subscribe(
      (response) => {
        this.menus = response;
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

}

