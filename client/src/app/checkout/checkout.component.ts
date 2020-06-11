import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AccountService } from '../account/account.service';
import { BasketService } from '../basket/basket.service';
import { Observable } from 'rxjs';
import { IBasketTotals } from '../shared/models/basket';
import { CheckoutService } from './checkout.service';
import { IOrder } from '../shared/models/order';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  checkoutForm: FormGroup;
  basketTotals$: Observable<IBasketTotals>;
  choice = true;
  @Input() total = 0;
  selectedDate: Date;

  constructor(
    private fb: FormBuilder,
    private accountService: AccountService,
    private basketService: BasketService,
    private checkoutService: CheckoutService
  ) {}

  ngOnInit(): void {
    this.getOrder();
    this.createCheckOutForm();
    this.getAddressFormValues();
    this.getDeliveryMethodValue();
    this.basketTotals$ = this.basketService.basketTotals$;
  }

  async getOrder() {
    this.checkoutService.getOrdersForUser().then((orders: IOrder[]) => {
      this.total = orders.filter(o => o.paymentMethod === false).reduce((prev, current) => prev + current.total, 0);
      console.log(this.total);
    }, error => {
      console.error(error);
    });
  }

  paymentChoice(choice: boolean) {
    return this.choice = choice;
  }

  getSelectedDate(shippingDate: Date) {
    return this.selectedDate = shippingDate;
  }

  createCheckOutForm() {
    this.checkoutForm = this.fb.group({
      addressForm: this.fb.group({
        firstName: [null, Validators.required],
        lastName: [null, Validators.required],
        building: [null, Validators.required],
        room: [null, Validators.required],
      }),
      deliveryForm: this.fb.group({
        deliveryMethod: [null, Validators.required],
      }),
      paymentForm: this.fb.group({
        nameOnCard: [null, Validators.required],
      }),
    });
  }

  getAddressFormValues() {
    this.accountService.getUserAddress().subscribe(
      (address) => {
        if (address) {
          this.checkoutForm.get('addressForm').patchValue(address);
        }
      },
      (error) => {
        console.error();
      }
    );
  }

  getDeliveryMethodValue() {
    const basket = this.basketService.getCurrentBasketValue();
    if (basket.deliveryMethodId != null) {
      this.checkoutForm.get('deliveryForm').get('deliveryMethod').patchValue(basket.deliveryMethodId.toString());
    }
  }
}
