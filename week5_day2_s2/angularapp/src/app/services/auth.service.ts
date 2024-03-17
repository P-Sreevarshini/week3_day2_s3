// auth.service.ts

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { JwtService } from './jwtservice.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'your_api_url';

  constructor(private http: HttpClient, private jwtService: JwtService) { }

  // Function to login user and save JWT token
  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { username, password }).pipe(
      tap(response => {
        if (response && response.token) {
          this.jwtService.saveToken(response.token);
        }
      })
    );
  }

  // Function to logout user and remove JWT token
  logout(): void {
    this.jwtService.destroyToken();
  }

  // Function to check if user is logged in
  isLoggedIn(): boolean {
    return this.jwtService.isLoggedIn();
  }
}
