import { IAddress } from './address';

export interface IOrderToCreate {
  basketId: string;
  deliveryMethodId: number;
  shipToAddress: IAddress;
  paymentMethod: boolean;
}

export interface IOrder {
  id: number;
  buyerEmail: string;
  orderDate: string;
  shipToAddress: IAddress;
  deliveryMethod: string;
  shippingPrice: number;
  orderItems: IOrderItem[];
  subtotal: number;
  status: string;
  total: number;
  paymentMethod: boolean;
}

export interface IOrderItem {
  mealId: number;
  mealName: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}
