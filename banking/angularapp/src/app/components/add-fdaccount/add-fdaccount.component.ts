import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { FdaccountService } from '../../services/fdaccount.service'; // Import the service here

@Component({
  selector: 'app-add-fdaccount',
  templateUrl: './add-fdaccount.component.html',
  styleUrls: ['./add-fdaccount.component.css']
})
export class AddFdaccountComponent implements OnInit {
  fdAccountForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router, private fdAccountService: FdaccountService) { } // Inject the service here

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
      this.router.navigate(['/view/FDaccount']);
    });
  }
}
