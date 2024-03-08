import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FDAccount } from '../models/fixedDepositAccount';

@Injectable({
  providedIn: 'root'
})
export class FdAccountService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) { }

  getAllFdAccounts(): Observable<FDAccount[]> {
    const endpoint = `${this.apiUrl}/api/FDAccount`; // Replace 'api/FDAccount' with your actual endpoint
    return this.http.get<FDAccount[]>(endpoint);
  }
  getFdAccountsByUser(id: number): Observable<FDAccount[]> {
    const endpoint = `${this.apiUrl}/api/FDAccount/${id}`; // Assuming the API endpoint for fetching FD accounts by user ID is '/api/FDAccount/user/:userId'
    return this.http.get<FDAccount[]>(endpoint);
  }
  updateFdAccountStatus(fdAccountId: number, newStatus: string): Observable<any> {
    return this.http.put<any>(`/api/fdaccounts/${fdAccountId}/status`, { status: newStatus });
  }

  
}
