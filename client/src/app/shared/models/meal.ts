export interface IMeal {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  mealType: string;
  menu: string;
  restaurant: string;
  photos: Photo[];
  ingrediants: Ingrediant[];
}

export interface Photo {
  id: number;
  pictureUrl: string;
  fileName: string;
  isMain: boolean;
}

export interface Ingrediant {
  name: string;
  price: number;
  quantity: number;
}


export interface IMealToCreate {
  name: string;
  description: string;
  price: number;
  pictureUrl: string;
  mealTypeId: number;
  MenuId: number;
  RestaurantId: number;
}

export class MealFormValues implements IMealToCreate {
  name = '';
  description = '';
  price = 0;
  pictureUrl = '';
  RestaurantId: number;
  MenuId: number;
  mealTypeId: number;

  constructor(init?: MealFormValues) {
    Object.assign(this, init);
  }
}
