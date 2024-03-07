import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FdaccountService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; 

  constructor(private http: HttpClient) {}

  addFDAccount(formData: any): Observable<any> {
    const endpoint = `${this.apiUrl}/api/FDAccounts`; // Adjust the endpoint according to your API
    return this.http.post<any>(endpoint, formData);
  }
}
