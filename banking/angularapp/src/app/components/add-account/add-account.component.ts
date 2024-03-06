import { Component } from '@angular/core';
import { Account } from 'src/app/models/account.model';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent {
accounts: Account[] = [];

  constructor(private accountService: AccountService) {}

  addAccount() {
    this.accountService.addAccount(this.accounts).subscribe(() => {
      // Handle success
      console.log('Account added successfully');
      alert("Account added successfully");
    }, (error) => {
      // Handle error
      console.error('Failed to add account:', error);
      alert("Failed to add account:', error");
    });
  }
}
