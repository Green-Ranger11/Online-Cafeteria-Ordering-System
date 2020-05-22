import { Component, OnInit } from '@angular/core';
import { IMeal } from '../shared/models/meal';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  meals: IMeal[];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.shopService.getMeals().subscribe(response => {
      this.meals = response.data;
    }, error => console.log(error));
  }

}
