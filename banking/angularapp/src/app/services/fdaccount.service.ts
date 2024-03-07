import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FdaccountService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io';

  constructor(private http: HttpClient) {}

  addFdAccount(fdData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/FDAccount`, fdData);
  }
}
