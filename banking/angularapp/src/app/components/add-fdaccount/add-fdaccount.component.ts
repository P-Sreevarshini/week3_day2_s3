import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-fdaccount',
  templateUrl: './add-fdaccount.component.html',
  styleUrls: ['./add-fdaccount.component.css']
})
export class AddFDAccountComponent implements OnInit {
  fdAccountForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private FixedDepositAccountService: FixedDepositAccountService, private router: Router) { }

  ngOnInit(): void {
    this.fdAccountForm = this.formBuilder.group({
      // Define your form controls with validators here
      userName: ['', Validators.required],
      balance: ['', [Validators.required, Validators.min(0)]],
      accountType: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.fdAccountForm.invalid) {
      return;
    }
    const formData = this.fdAccountForm.value;
    this.fdAccountService.addFDAccount(formData).subscribe(() => {
      // Redirect or perform any action after successful submission
      this.router.navigate(['/fd-accounts']);
    });
  }
}
