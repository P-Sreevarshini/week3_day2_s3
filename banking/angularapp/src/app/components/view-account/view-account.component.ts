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
    this.userRole = localStorage.getItem('userRole');
    console.log('User Role:', this.userRole);
  
    if (this.userRole === 'Admin') {
      this.getAllAccounts();
    } else if (this.userRole === 'Customer') {
      const userId = this.getUserIdFromStorage();
      if (userId) {
        this.getCustomerAccounts(userId);
      }
    }
  // }
    this.userRole = localStorage.getItem('userRole');
    console.log('User Role:', this.userRole);

    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.authService.decodeToken(token);
      if (decodedToken) {
        this.userRole = decodedToken.role;
      }
    }
  }

  getAllAccounts(): void {
    this.userRole = localStorage.getItem('userRole');
    if (this.userRole === 'Admin') {
    this.accountService.getAllAccounts().subscribe(accounts => {
      this.accounts = accounts;
    });
  }
}
  getCustomerAccounts(userId: number): void {
    console.log("user"+userId);
    this.userRole = localStorage.getItem('userRole');
    console.log("user"+this.userRole);
    if (this.userRole === 'Customer') {
      console.log("user"+this.userRole);
    this.accountService.getCustomerAccounts(userId).subscribe(accounts => {
      this.accounts = accounts;
      console.log(accounts);

    });
  }
}
  deleteAccount(userId: number, accountId: number): void {
    this.userRole = localStorage.getItem('userRole');
     if (this.userRole === 'Admin') {
    if (this.accountService.deleteAccount) {
      this.accountService.deleteAccount(userId, accountId).subscribe(() => {
        this.getAllAccounts();
      });
    } else {
      console.error('deleteAccount method not found in AccountService');
    }
  }
}
getUserIdFromStorage(): number {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.authService.decodeToken(token);
      if (decodedToken) {
        return decodedToken.userId;
      }
    }
    return null;
  }
}
