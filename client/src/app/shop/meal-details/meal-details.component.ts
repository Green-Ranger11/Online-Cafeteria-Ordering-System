import { Component, OnInit } from '@angular/core';
import { IMeal } from 'src/app/shared/models/meal';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.scss'],
})
export class MealDetailsComponent implements OnInit {
  meal: IMeal;
  quantity = 1;

  constructor(
    private shopService: ShopService,
    private activateRoute: ActivatedRoute,
    private bcService: BreadcrumbService,
    private basketService: BasketService
  ) {
    this.bcService.set('@mealDetails', '');
  }

  ngOnInit(): void {
    this.loadMeal();
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.meal, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if(this.quantity > 1) {
      this.quantity--;
    }
  }

  loadMeal() {
    this.shopService
      .getMeal(+this.activateRoute.snapshot.paramMap.get('id'))
      .subscribe(
        (meal) => {
          this.meal = meal;
          this.bcService.set('@mealDetails', meal.name);
          console.log(this.meal);
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
