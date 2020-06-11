import { Component, OnInit, Input } from '@angular/core';
import { IBasket } from 'src/app/shared/models/basket';
import { BasketService } from 'src/app/basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { Router, NavigationExtras } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { OrdersService } from 'src/app/orders/orders.service';
import { IOrder } from 'src/app/shared/models/order';

@Component({
  selector: 'app-checkout-payment-payroll',
  templateUrl: './checkout-payment-payroll.component.html',
  styleUrls: ['./checkout-payment-payroll.component.scss'],
})
export class CheckoutPaymentPayrollComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  loading = false;
  @Input() total;
  @Input() selectedDate: Date;

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    console.log(this.selectedDate);
  }

  private createOrder(basket: IBasket) {
    const orderToCreate = this.getOrderToCreate(basket, this.selectedDate);
    return this.checkoutService.createOrder(orderToCreate).toPromise();
  }

  private getOrderToCreate(basket: IBasket, shippingDate: Date) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm
        .get('deliveryForm')
        .get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value,
      paymentMethod: false,
      shippingDate
    };
  }

  async submitOrder() {
    this.loading = true;
    const basket = this.basketService.getCurrentBasketValue();
    try {
      const createdOrder = await this.createOrder(basket);
      console.log(createdOrder);
      this.basketService.deleteBasket(basket);
      const navigationExtras: NavigationExtras = { state: createdOrder };
      this.router.navigate(['checkout/success'], navigationExtras);
      this.loading = false;
    } catch (error) {
      console.error(error);
      this.loading = false;
    }
  }

}
