import { Component, OnInit } from '@angular/core';
import { IMeal } from 'src/app/shared/models/meal';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.scss']
})
export class MealDetailsComponent implements OnInit {
  meal: IMeal;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMeal();
  }

  loadMeal(){
    this.shopService.getMeal(+this.activateRoute.snapshot.paramMap.get('id')).subscribe(meal => {
      this.meal = meal;
    }, error => {
      console.log(error);
    });
  }

}
