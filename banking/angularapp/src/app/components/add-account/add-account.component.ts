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

  addAccount() {
    this.accountService.addAccount(this.account).subscribe(() => {
      // Handle success
      console.log('Account added successfully');
    }, (error) => {
      // Handle error
      console.error('Failed to add account:', error);
    });
  }
}
