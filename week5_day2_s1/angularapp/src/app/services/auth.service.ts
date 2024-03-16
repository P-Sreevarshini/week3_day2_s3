
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators'; // Import the map operator
import { Router } from '@angular/router'; // Import Router

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io';

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/api/login`, { username, password })
      .pipe(
        map(response => {
          // Check if login was successful
          if (response && response.message === "Login successful") {
            // Redirect to the dashboard component
            this.router.navigate(['/dashboard']);
          }
          return response;
        })
      );
  }

  isLoggedIn(): boolean {
    // Implement your logic to check if user is logged in
    // For now, returning false since no actual authentication logic is implemented
    return false;
  }
}
