import { Component, OnInit } from '@angular/core';
import { IMeal } from 'src/app/shared/models/meal';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';
import { BasketService } from 'src/app/basket/basket.service';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation, NgxGalleryImageSize } from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-meal-details',
  templateUrl: './meal-details.component.html',
  styleUrls: ['./meal-details.component.scss'],
})
export class MealDetailsComponent implements OnInit {
  meal: IMeal;
  quantity = 1;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

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

  initializeGallery() {
    this.galleryOptions = [
      {
        width: '500px',
        height: '600px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Fade,
        imageSize: NgxGalleryImageSize.Contain,
        thumbnailSize: NgxGalleryImageSize.Contain,
        preview: false
      }
    ];
    this.galleryImages = this.getImages();
  }

  getImages() {
    const imageUrls = [];
    for (const photo of this.meal.photos) {
      imageUrls.push({
        small: photo.pictureUrl,
        medium: photo.pictureUrl,
        big: photo.pictureUrl,
      });
    }
    return imageUrls;
  }

  addItemToBasket() {
    this.basketService.addItemToBasket(this.meal, this.quantity);
  }

  incrementQuantity() {
    this.quantity++;
  }

  decrementQuantity() {
    if (this.quantity > 1) {
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
          this.initializeGallery();
        },
        (error) => {
          console.log(error);
        }
      );
  }
}
