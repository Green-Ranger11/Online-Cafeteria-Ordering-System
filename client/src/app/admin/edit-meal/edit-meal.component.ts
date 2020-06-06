import { Component, OnInit } from '@angular/core';
import { MealFormValues } from 'src/app/shared/models/meal';
import { IRestaurant } from 'src/app/shared/models/restaurants';
import { IMenu } from 'src/app/shared/models/menu';
import { IType } from 'src/app/shared/models/mealType';
import { AdminService } from '../admin.service';
import { ShopService } from 'src/app/shop/shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit-meal',
  templateUrl: './edit-meal.component.html',
  styleUrls: ['./edit-meal.component.scss'],
})
export class EditMealComponent implements OnInit {
  meal: MealFormValues;
  mealFormValues: MealFormValues;
  restaurants: IRestaurant[];
  menus: IMenu[];
  types: IType[];

  constructor(
    private adminService: AdminService,
    private shopService: ShopService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.meal = new MealFormValues();
  }

  ngOnInit(): void {
    const restaurants = this.getRestaurants();
    const types = this.getTypes();
    const menus = this.getMenus();

    forkJoin([types, menus, restaurants]).subscribe(
      (results) => {
        this.types = results[0];
        this.menus = results[1];
        this.restaurants = results[2];
      },
      (error) => {
        console.log(error);
      },
      () => {
        if (this.route.snapshot.url[0].path === 'edit') {
          this.loadMeal();
        }
      }
    );
  }

  loadMeal() {
    this.shopService
      .getMeal(+this.route.snapshot.paramMap.get('id'))
      .subscribe((response: any) => {
        const RestaurantId =
          this.restaurants &&
          this.restaurants.find((x) => x.name === response.restaurant).id;
        const mealTypeId =
          this.types && this.types.find((x) => x.name === response.mealType).id;
        const MenuId =
          this.menus && this.menus.find((x) => x.name === response.menu).id;
        this.meal = response;
        this.mealFormValues =  { ...response, RestaurantId, mealTypeId, MenuId };
      });
  }

  getRestaurants() {
    return this.shopService.getRestaurants();
  }

  getTypes() {
    return this.shopService.getTypes();
  }

  getMenus() {
    return this.shopService.getMenus();
  }
}
