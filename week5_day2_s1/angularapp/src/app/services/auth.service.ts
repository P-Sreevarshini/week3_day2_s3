// auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/login`, { username, password });
  }

  isLoggedIn(): boolean {
    // Implement your logic to check if user is logged in
    // For now, returning true for demonstration purpose
    return true;
  }
}
