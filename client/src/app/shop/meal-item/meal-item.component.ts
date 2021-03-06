import { Component, OnInit, Input } from '@angular/core';
import { IMeal } from 'src/app/shared/models/meal';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-meal-item',
  templateUrl: './meal-item.component.html',
  styleUrls: ['./meal-item.component.scss']
})
export class MealItemComponent implements OnInit {
  @Input() meal: IMeal;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.meal);
  }
}
