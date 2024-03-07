import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { Account } from '../models/account.model';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const authToken = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${authToken}`
    });
  }
  addAccount(account: Account): Observable<Account> {
    const endpoint = `${this.apiUrl}/api/account`;
    const headers = this.getHeaders();
  
    return this.http.post<Account>(endpoint, account, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }
  deleteAccount(userId: number, accountId: number): Observable<void> {
    const endpoint = `${this.apiUrl}/api/account/${userId}/${accountId}`;
    const headers = this.getHeaders();
  
    return this.http.delete<void>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }
  
  

  editAccount(accountId: number, updatedAccountData: Account): Observable<Account> {
    const endpoint = `${this.apiUrl}/api/account/${accountId}`;
    const headers = this.getHeaders();
  
    return this.http.put<Account>(endpoint, updatedAccountData, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  getAllAccounts(): Observable<Account[]> {
    const endpoint = `${this.apiUrl}/api/account`;
    const headers = this.getHeaders();
  
    return this.http.get<Account[]>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  getCustomerAccounts(userId: number): Observable<Account[]> {
    const endpoint = `${this.apiUrl}/api/account/${userId}`;
    const headers = this.getHeaders();
  
    return this.http.get<Account[]>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

}