import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-checkout-datetime',
  templateUrl: './checkout-datetime.component.html',
  styleUrls: ['./checkout-datetime.component.scss']
})
export class CheckoutDatetimeComponent implements OnInit {
  @Input() checkoutForm: FormGroup;
  public startAt = new Date();
  public min = new Date();
  public max = new Date(2021, 3, 21, 20, 30);
  @Output() shippingDate: EventEmitter<Date> = new EventEmitter<Date>();
  selectedDate: Date;

  constructor() { }

  ngOnInit(): void {
  }

  test() {
    console.log(this.selectedDate);
    this.shippingDate.emit(this.selectedDate);
  }

}
