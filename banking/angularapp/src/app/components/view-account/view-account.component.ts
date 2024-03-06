import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Account } from 'src/app/models/account.model';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-account',
  templateUrl: './view-account.component.html',
  styleUrls: ['./view-account.component.css']
})
export class ViewAccountComponent implements OnInit {
  accounts: Account[];
  userRole: string;

  constructor(private accountService: AccountService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.getAllAccounts();
    this.userRole = localStorage.getItem('userRole');
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.authService.decodeToken(token);
      if (decodedToken) {
        this.userRole = decodedToken.role;
      }
    }
  }

  getAllAccounts(): void {
    this.accountService.getAllAccounts().subscribe(accounts => {
      this.accounts = accounts;
    });
  }

  deleteAccount(account: Account): void {
    if (this.accountService.deleteAccount) {
      this.accountService.deleteAccount(account.AccountId).subscribe(() => {
        this.getAllAccounts();
      });
    } else {
      console.error('deleteAccount method not found in AccountService');
    }
  }
}
