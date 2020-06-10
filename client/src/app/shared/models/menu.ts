export interface IMenu {
  id: number;
  name: string;
}

export interface IMenuToCreate {
  name: string;
  RestaurantId: number;
}

export class MenuFormValues implements IMenuToCreate {
  name = '';
  RestaurantId: number;

  constructor(init?: MenuFormValues) {
    Object.assign(this, init);
  }
}
