import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { IMenu } from '../shared/models/menu';
import { ShopParams } from '../shared/models/shopParams';
import { IPagination } from '../shared/models/pagination';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  baseUrl = environment.apiUrl;
  shopParams = new ShopParams();

  constructor(private http: HttpClient) { }

  getMeals(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.restaurantId !== 0) {
      params = params.append(
        'restaurantId',
        shopParams.restaurantId.toString()
      );
    }

    if (shopParams.menuId !== 0) {
      params = params.append('menuId', shopParams.menuId.toString());
    }

    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageIndex', shopParams.pageSize.toString());

    return this.http
      .get<IPagination>(this.baseUrl + 'meals', { observe: 'response', params })
      .pipe(
        map((response) => {
          return response.body;
        })
      );
  }

  getMenus(restaurant: number) {
    return this.http.get<IMenu[]>(this.baseUrl + 'meals/menus/' + restaurant);
  }

  getShopParams() {
    return this.shopParams;
  }

  setShopParams(params: ShopParams) {
    this.shopParams = params;
  }
}
