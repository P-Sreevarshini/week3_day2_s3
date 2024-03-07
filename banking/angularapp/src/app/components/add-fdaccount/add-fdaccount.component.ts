import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FdaccountService } from '../../services/fdaccount.service';

@Component({
  selector: 'app-add-fdaccount',
  templateUrl: './add-fdaccount.component.html',
  styleUrls: ['./add-fdaccount.component.css']
})
export class AddFdaccountComponent implements OnInit {
  fdAccountForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private fdAccountService: FdaccountService
  ) {}

  ngOnInit() {
    this.fdAccountForm = this.formBuilder.group({
      amount: ['', [Validators.required, Validators.min(0)]],
      tenureMonths: ['', [Validators.required, Validators.min(1)]],
      interestRate: ['', [Validators.required, Validators.min(0)]]
    });
  }

  // Convenience getter for easy access to form fields
  get form() {
    return this.fdAccountForm.controls;
  }

  onSubmit() {
    // Stop here if form is invalid
    if (this.fdAccountForm.invalid) {
      return;
    }

    // Prepare data for submission
    const fdData = {
      amount: this.form.amount.value,
      tenureMonths: this.form.tenureMonths.value,
      interestRate: this.form.interestRate.value
    };

    // Call the service to add FD account
    this.fdAccountService.addFdAccount(fdData).subscribe(
      () => {
        // Redirect to another page after successful submission
        this.router.navigate(['/success']); // Change '/success' to the desired route
      },
      error => {
        // Handle error
        console.error('Error adding FD account:', error);
      }
    );
  }
}
