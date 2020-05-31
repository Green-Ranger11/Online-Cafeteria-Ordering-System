import { Component, OnInit } from '@angular/core';
import { IMeal } from 'src/app/shared/models/meal';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.scss']
})
export class MealDetailsComponent implements OnInit {
  meal: IMeal;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute, private bcService: BreadcrumbService) { 
    this.bcService.set('@mealDetails', '');
  }

  ngOnInit(): void {
    this.loadMeal();
  }

  loadMeal(){
    this.shopService.getMeal(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(meal => {
      this.meal = meal;
      this.bcService.set('@mealDetails', meal.name);
    }, error => {
      console.log(error);
    });
  }

}
