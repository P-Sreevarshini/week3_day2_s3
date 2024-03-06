import { Component } from '@angular/core';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent {
  account = { userId: 0, balance: 0, accountType: '' };

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    // Retrieve user details from local storage
    const userId = Number(localStorage.getItem('userId'));
    const userRole = localStorage.getItem('userRole');

    if (userId && userRole === 'Customer') {
      // Set the user ID for the new account
      this.account.userId = userId;
    } else {
      // Redirect or handle unauthorized access
      console.error('Unauthorized access');
    }
  }

  addAccount() {
    if (localStorage.getItem('userRole') !== 'Customer') {
      // Redirect or handle unauthorized access
      console.error('Only customers can add accounts');
      return;
    }

    this.accountService.addAccount(this.account).subscribe((addedAccount) => {
      // Handle success
      console.log('Account added successfully:', addedAccount);
    }, (error) => {
      // Handle error
      console.error('Failed to add account:', error);
    });
  }
}
