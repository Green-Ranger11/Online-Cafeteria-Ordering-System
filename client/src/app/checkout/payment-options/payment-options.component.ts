import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CdkStepper } from '@angular/cdk/stepper';
import { BasketService } from 'src/app/basket/basket.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-payment-options',
  templateUrl: './payment-options.component.html',
  styleUrls: ['./payment-options.component.scss']
})
export class PaymentOptionsComponent implements OnInit {
  // True if credit card False if Payroll
  @Output() choice: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Input() appSteppper: CdkStepper;
  creditCard = true;

  constructor(private basketService: BasketService, private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onClick(choice: boolean) {
    if (choice) {
      this.createPaymentIntent();
      this.choice.emit(choice);
    } else {
      this.choice.emit(choice);
    }
  }

  createPaymentIntent() {
    return this.basketService.createPaymentIntent().subscribe((response: any) => {
      this.appSteppper.next();
    }, error => {
      console.error(error);
    });
  }

}
