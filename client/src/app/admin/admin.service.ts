import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { MealFormValues, Ingrediant, IngrediantsFormValues } from '../shared/models/meal';
import { MenuFormValues } from '../shared/models/menu';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createMeal(meal: MealFormValues) {
    return this.http.post(this.baseUrl + 'meals', meal);
  }

  updateMeal(meal: MealFormValues, id: number) {
    return this.http.put(this.baseUrl + 'meals/' + id, meal);
  }

  deleteMeal(id: number) {
    return this.http.delete(this.baseUrl + 'meals/' + id);
  }

  createMenu(menu: MenuFormValues) {
    return this.http.post(this.baseUrl + 'menus', menu);
  }

  updateMenu(menu: MenuFormValues, id: number) {
    return this.http.put(this.baseUrl + 'menus/' + id, menu);
  }

  deleteMenu(id: number) {
    return this.http.delete(this.baseUrl + 'menus/' + id);
  }

  uploadImage(file: File, id: number) {
    const formData = new FormData();
    formData.append('photo', file, 'image.png');
    return this.http.put(this.baseUrl + 'meals/' + id + '/photo', formData, {
      reportProgress: true,
      observe: 'events',
    });
  }

  deleteMealPhoto(photoId: number, mealId: number) {
    return this.http.delete(
      this.baseUrl + 'meals/' + mealId + '/photo/' + photoId
    );
  }

  setMainPhoto(photoId: number, mealId: number) {
    return this.http.post(
      this.baseUrl + 'meals/' + mealId + '/photo/' + photoId,
      {}
    );
  }

  addMealIngrediant(ingrediant: IngrediantsFormValues, mealId: number) {
    return this.http.post(
      this.baseUrl + 'meals/' + mealId + '/ingrediant/',
      ingrediant
    );
  }

  updateMealIngrediant(
    ingrediant: Ingrediant,
    mealId: number,
    ingrediantId: number
  ) {
    return this.http.put(
      this.baseUrl + 'meals/' + mealId + '/ingrediant/' + ingrediantId,
      ingrediant
    );
  }

  deleteMealIngrediant(ingrediantId: number, mealId: number) {
    return this.http.delete(
      this.baseUrl + 'meals/' + mealId + '/ingrediant/' + ingrediantId
    );
  }
}
