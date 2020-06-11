import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { Routes } from '@angular/router';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { SharedModule } from '../shared/shared.module';
import { CheckoutAddressComponent } from './checkout-address/checkout-address.component';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CheckoutPaymentComponent } from './checkout-payment/checkout-payment.component';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import { PaymentOptionsComponent } from './payment-options/payment-options.component';
import { CheckoutPaymentPayrollComponent } from './checkout-payment-payroll/checkout-payment-payroll.component';
import { OrdersService } from '../orders/orders.service';
import { OrdersModule } from '../orders/orders.module';
import { CheckoutDatetimeComponent } from './checkout-datetime/checkout-datetime.component';

@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutAddressComponent,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent,
    CheckoutPaymentComponent,
    CheckoutSuccessComponent,
    PaymentOptionsComponent,
    CheckoutPaymentPayrollComponent,
    CheckoutDatetimeComponent,
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    SharedModule]
})
export class CheckoutModule {}
