import { v4 as uuidv4 } from 'uuid';

export interface IBasket {
  id: string;
  items: IBasketItem[];
  clientSecret?: string;
  paymentIntentId?: string;
  deliveryMethodId?: number;
}

export interface IBasketItem {
  id: number;
  mealName: string;
  price: number;
  quantity: number;
  pictureUrl: string;
  restaurant: string;
  menu: string;
  type: string;
}

export class Basket implements IBasket {
  id = uuidv4();
  items: IBasketItem[] = [];
}

export interface IBasketTotals {
  shipping: number;
  subtotal: number;
  total: number;
}
