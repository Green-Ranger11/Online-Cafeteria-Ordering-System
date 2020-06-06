import { Component, OnInit, Input } from '@angular/core';
import { IRestaurant } from 'src/app/shared/models/restaurants';
import { IMenu } from 'src/app/shared/models/menu';
import { IType } from 'src/app/shared/models/mealType';
import { MealFormValues } from 'src/app/shared/models/meal';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../admin.service';

@Component({
  selector: 'app-edit-meal-form',
  templateUrl: './edit-meal-form.component.html',
  styleUrls: ['./edit-meal-form.component.scss']
})
export class EditMealFormComponent implements OnInit {
  @Input() meal: MealFormValues;
  @Input() restaurants: IRestaurant[];
  @Input() menus: IMenu[];
  @Input() types: IType[];

  constructor(private route: ActivatedRoute, private adminService: AdminService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(meal: MealFormValues) {
    if (this.route.snapshot.url[0].path === 'edit') {
      const updatedMeal: MealFormValues = { ...this.meal, ...meal, price: +meal.price };
      this.adminService
        .updateMeal(updatedMeal, +this.route.snapshot.paramMap.get('id'))
        .subscribe((response: any) => {
          this.router.navigate(['/admin']);
        });
    } else {
      const newMeal = { ...meal, price: +meal.price };
      this.adminService.createMeal(newMeal).subscribe((response: any) => {
        this.router.navigate(['/admin']);
      });
    }
  }

  updatePrice(event: any) {
    this.meal.price = event;
  }
}
