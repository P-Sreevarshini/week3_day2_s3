import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { NgForm } from '@angular/forms';
import { JwtService } from '../../services/jwt.service';
import { Router } from '@angular/router';
import { Account } from '../../models/account.model'; // Import the Account model

@Component({
  selector: 'app-add-account',
  templateUrl: './add-account.component.html',
  styleUrls: ['./add-account.component.css']
})
export class AddAccountComponent {
  userRole: string;

  account: Account = {
    AccountId: 0,
    UserId: 0,
    Balance: 0,
    AccountType: ''
  };

  constructor(private accountService: AccountService, private jwtService: JwtService, private router: Router) {
    this.userRole = this.jwtService.getUserRole();
  }

  addAccount(form: NgForm): void {
    if (form.valid) {
      this.accountService.addAccount(this.account).subscribe(
        () => {
          alert('Account added successfully!');
          this.router.navigate(['/view/accounts']);
        },
        (error) => {
          console.log(this.account);
          console.error('Error adding account:', error);
        }
      );
    }
  }
}
