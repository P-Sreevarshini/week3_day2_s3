import { Component, OnInit } from '@angular/core';
import { FDAccount } from 'src/app/models/fixedDepositAccount';
import { FdAccountService } from 'src/app/services/fdaccount.service';

@Component({
  selector: 'app-view-fdaccount',
  templateUrl: './view-fdaccount.component.html',
  styleUrls: ['./view-fdaccount.component.css']
})
export class ViewFdAccountsComponent implements OnInit {
  accounts: FDAccount[];
  userId: number;
  userRole: string;
  loading: boolean = false;
  error: string = null;


  constructor(private fdAccountService: FdAccountService) { }

  ngOnInit(): void {
    this.userId = parseInt(localStorage.getItem('user'), 10); // Retrieve user ID from local storage and convert to number
    this.userRole = localStorage.getItem('userRole');
    if (this.userRole === 'Admin') {
      this.getAllFdAccounts();
    } else if (this.userRole === 'Customer') {
      this.getFdAccountsByUser();
    }
    }

  getAllFdAccounts(): void {
    if (this.userRole === 'Admin') {

    this.fdAccountService.getAllFdAccounts().subscribe(accounts => {
      this.accounts = accounts;
    });
  }
}

//   getFdAccountsByUser(): void {
//     if (this.userRole === 'Customer') {
//     this.fdAccountService.getFdAccountsByUser(this.userId).subscribe(accounts => {
//       this.accounts = accounts;
//     });
//   }
// }

getFdAccountsByUser(): void {
  this.loading = true;
  this.fdAccountService.getFdAccountsByUser(this.userId).subscribe(
    accounts => {
      this.accounts = accounts;
      this.loading = false;
    },
    error => {
      this.error = "Error occurred while fetching FD accounts.";
      this.loading = false;
      console.error(error);
    }
  );
}

updateAccountStatus(fdAccountId: number, newStatus: string): void {
  this.fdAccountService.updateFdAccountStatus(fdAccountId, newStatus).subscribe(() => {
    console.log(`Status updated to: ${newStatus} for account ID: ${fdAccountId}`);
  });
}
}
