import { Component, OnInit } from '@angular/core';
import { MenuFormValues } from 'src/app/shared/models/menu';
import { IRestaurant } from 'src/app/shared/models/restaurants';
import { AdminService } from '../admin.service';
import { ShopService } from 'src/app/shop/shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-edit-menu',
  templateUrl: './edit-menu.component.html',
  styleUrls: ['./edit-menu.component.scss'],
})
export class EditMenuComponent implements OnInit {
  menu: MenuFormValues;
  menuFormValues = new MenuFormValues();
  restaurants: IRestaurant[];

  constructor(
    private adminService: AdminService,
    private shopService: ShopService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.menu = new MenuFormValues();
  }

  ngOnInit(): void {
    const restaurants = this.getRestaurants();

    forkJoin([restaurants]).subscribe(
      (results) => {
        this.restaurants = results[0];
      },
      (error) => {
        console.log(error);
      },
      () => {
        if (this.route.snapshot.url[0].path === 'edit') {
          this.loadMenu();
        }
      }
    );
  }

  loadMenu() {
    this.shopService
      .getMenu(+this.route.snapshot.paramMap.get('id'))
      .subscribe((response: any) => {
        const RestaurantId =
          this.restaurants &&
          this.restaurants.find((x) => x.name === response.restaurant).id;
        this.menu = response;
        this.menuFormValues =  { ...response, RestaurantId};
      });
  }

  getRestaurants() {
    return this.shopService.getRestaurants();
  }
}
