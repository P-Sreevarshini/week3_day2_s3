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
}
