import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Account } from './account.model';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'api/account';

  constructor(private http: HttpClient) { }

  getAllAccounts(): Observable<Account[]> {
    return this.http.get<Account[]>(this.apiUrl);
  }

  getAccountById(accountId: number): Observable<Account> {
    const url = `${this.apiUrl}/${accountId}`;
    return this.http.get<Account>(url);
  }

  addAccount(account: Account): Observable<any> {
    return this.http.post<any>(this.apiUrl, account);
  }

  updateAccount(accountId: number, account: Account): Observable<any> {
    const url = `${this.apiUrl}/${accountId}`;
    return this.http.put<any>(url, account);
  }

  deleteAccount(accountId: number): Observable<any> {
    const url = `${this.apiUrl}/${accountId}`;
    return this.http.delete<any>(url);
  }
}
