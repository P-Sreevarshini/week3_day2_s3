import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { FixedDeposit } from '../models/fixedDeposit.model';

@Injectable({
  providedIn: 'root'
})
export class FixedDepositService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const authToken = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${authToken}`
    });
  }

  getAllFd(): Observable<FixedDeposit[]> {
    const endpoint = `${this.apiUrl}/api/fixeddeposit`;
    const headers = this.getHeaders();

    return this.http.get<FixedDeposit[]>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }
  saveFdByAdmin(fd: FixedDeposit): Observable<FixedDeposit> {
    const endpoint = `${this.apiUrl}/api/fixeddeposit`;
    const headers = this.getHeaders();

    return this.http.post<FixedDeposit>(endpoint, fd, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  updateFdByAdmin(fdId: number, updatedFdData: FixedDeposit): Observable<FixedDeposit> {
    const endpoint = `${this.apiUrl}/api/fixeddeposit/{FixedDepositId}${fdId}`;
    const headers = this.getHeaders();

    return this.http.put<FixedDeposit>(endpoint, updatedFdData, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  deleteFdByAdmin(fdId: number): Observable<void> {
    const endpoint = `${this.apiUrl}/api/fixeddeposit/{FixedDepositId}${fdId}`;
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

  getCustomerFd(customerId: number): Observable<FixedDeposit[]> {
    const endpoint = `${this.apiUrl}/customers/${customerId}/fixed-deposits`;
    const headers = this.getHeaders();

    return this.http.get<FixedDeposit[]>(endpoint, { headers }).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }
}
