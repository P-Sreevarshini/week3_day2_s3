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

  constructor(private fdAccountService: FdAccountService) { }

  ngOnInit(): void {
    this.getAllFdAccounts();
  }

  getAllFdAccounts(): void {
    this.fdAccountService.getAllFdAccounts().subscribe(accounts => {
      this.accounts = accounts;
    });
  }
}
