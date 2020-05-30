import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagination } from '../shared/models/pagination';
import { IRestaurant } from '../shared/models/restaurants';
import { IType } from '../shared/models/mealType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IMeal } from '../shared/models/meal';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getMeals(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.restaurantId !== 0) {
      params = params.append('restaurantId', shopParams.restaurantId.toString());
    }

    if (shopParams.typeId !== 0) {
      params = params.append('typeId', shopParams.typeId.toString());
    }

    if (shopParams.search){
      params = params.append('search', shopParams.search)
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageIndex', shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'meals', {observe: 'response', params})
      .pipe(
        map(response => {
          return response.body;
        })
      );
  }

  getMeal(id: number){
    return this.http.get<IMeal>(this.baseUrl + 'meals/' + id);
  }

  getRestaurants(){
    return this.http.get<IRestaurant[]>(this.baseUrl + 'meals/restaurants');
  }

  getTypes(){
    return this.http.get<IType[]>(this.baseUrl + 'meals/types');
  }
}
