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
  editModeMap: { [key: number]: boolean } = {}; // Map to track the edit mode of each FD
  selectedaccount: Account;


  constructor(private accountService: AccountService, private authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.userRole = localStorage.getItem('userRole');
    console.log('User Role:', this.userRole);
  
    const userId = this.authService.getUserId();
    console.log('User ID:', userId);
  
    if (this.userRole === 'Admin') {
      this.getAllAccounts();
    } else if (this.userRole === 'Customer') {
      if (userId) {
        this.getCustomerAccounts(userId);
      }
    }
  }

  toggleEditMode(accounts: Account): void {
    this.selectedaccount = this.selectedaccount === accounts ? null : accounts;
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
    this.userRole = localStorage.getItem('userRole');
    // console.log("user"+this.userRole);
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
     if (this.userRole === 'Customer') {
    if (this.accountService.deleteAccount) {
      this.accountService.deleteAccount(userId, accountId).subscribe(() => {
        this.getCustomerAccounts(userId);
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
  cancelEdit(): void {
    this.editModeMap[this.selectedaccount.AccountId] = false;
    this.selectedaccount = null; 
  }
  updateAccount(account: Account): void {
    if (!account.AccountId) {
        console.error('Account ID is undefined.');
        console.log('Account Object:', account);
        return;
    }
    if (this.userRole !== 'Customer') {
        console.error('Access denied. Only Customer can update accounts.');
        return;
    }

    const updatedData: Account = { ...account };  
    updatedData.Balance = account.Balance;
    updatedData.AccountType = account.AccountType;

    this.accountService.updateAccount(account.AccountId, updatedData).subscribe(
        () => {
            console.log('Account updated successfully.');
            this.getAllAccounts();
            // this.getCustomerAccounts(this.userId); // Assuming this method exists to refresh the account list
        },
        (error) => {
            console.error('Error updating account:', error);
        }
    );
}

  // editAccount(accounts: Account): void {
  //   // console.log('User Role:', this.userRole);

  //   if (this.userRole !== 'Customer') {
  //     console.error('Access denied. Only Customer can edit Account.');
  //     return;
  //   }
  //   this.editModeMap[accounts.AccountId] = !this.editModeMap[accounts.AccountId];
  //   this.selectedaccount = accounts;
  // }
  
//   editAccount(account: Account): void {
//     if (!account.AccountId) {
//         console.error('Account ID is undefined.');
//         console.log('Account Object:', account);
//         return;
//     }
//     if (this.userRole !== 'Customer') {
//         console.error('Access denied. Only Customer can update accounts.');
//         return;
//     }

//     const updatedData: Account = { ...account };
//     updatedData.Balance = account.Balance;
//     updatedData.AccountType = account.AccountType;

//     this.accountService.editAccount(account.AccountId, updatedData).subscribe(
//         () => {
//             console.log('Account updated successfully.');
//             this.getAllAccounts();
//         },
//         (error) => {
//             console.error('Error updating account:', error);
//         }
//     );
// }
// editAccount(account: Account): void {
//   console.log('Edit button clicked for account:', account);

//   if (!account.AccountId) {
//     console.error('Account ID is undefined.');
//     console.log('Account Object:', account);
//     return;
//   }
//   if (this.userRole !== 'Customer') {
//     console.error('Access denied. Only Customer can edit Account.');
//     return;
//   }

//   // Toggle edit mode for the selected account
//   this.editModeMap[account.AccountId] = !this.editModeMap[account.AccountId];
// }
editAccount(account: Account): void {
  if (!this.editModeMap[account.AccountId]) {
    this.editModeMap[account.AccountId] = true; // Enable edit mode for the selected account
  } else {
    this.editModeMap[account.AccountId] = false; // Disable edit mode for the selected account
  }
}



}
