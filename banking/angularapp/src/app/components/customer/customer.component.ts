import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  isCustomer: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.authService.isCustomer$.subscribe(isCustomer => {
      this.isCustomer = isCustomer;
    });
  }
}
