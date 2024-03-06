import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Account } from '../models/account.model';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private apiUrl = 'api/account';

  constructor(private http: HttpClient) { }

  addAccount(account: any): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}`, account)
      .pipe(
        catchError(this.handleError) // Handle errors
      );
  }
  private handleError(error: any): Observable<never> {
    console.error('An error occurred:', error);
    throw error;
  }
}
