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
    userId: number;
    accountId: number;

    constructor(private accountService: AccountService, private authService: AuthService, private router: Router) { }

    ngOnInit(): void {
      this.userRole = localStorage.getItem('userRole');
      if (this.userRole === 'Admin') {
        this.getAllAccounts();
      } else if (this.userRole === 'Customer') {
        this.getCustomerAccounts(this.userId);
        this.deleteAccount(this.userId, this.accountId);
      }
    }

    getAllAccounts(): void {
      this.accountService.getAllAccounts().subscribe(accounts => {
        this.accounts = accounts;
      });
    }
    getCustomerAccounts(userId: number): void {
      this.accountService.getCustomerAccounts(userId).subscribe(accounts => {
        this.accounts = accounts;
      });
    }
    deleteAccount(userId: number, accountId: number): void {
      if (this.accountService.deleteAccount) {
        this.accountService.deleteAccount(userId, accountId).subscribe(() => {
          this.getAllAccounts();
        });
      } else {
        console.error('deleteAccount method not found in AccountService');
      }
    }
  }
