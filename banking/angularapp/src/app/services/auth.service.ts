import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<string | null>;
  public currentUser: Observable<string | null>;
  public apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; 
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$: Observable<string> = this.userRoleSubject.asObservable();
  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.isAuthenticated());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();


  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<string | null>(
      localStorage.getItem('currentUser')
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

register(userName: string, password: string, userRole: string, email: string, mobileNumber: string): Observable<any> {
  const body = { userName, password, userRole, email, mobileNumber };
  return this.http.post<any>(`${this.apiUrl}/api/register`, body);
}

// Method to check if the email already exists
checkEmailExists(email: string): Observable<boolean> {
  return this.http.get<any>(`${this.apiUrl}/api/check-email/${email}`).pipe(
    catchError(error => {
      // If the error status is 404, it means the email doesn't exist
      if (error.status === 404) {
        return of(false);
      } else {
        // For other errors, re-throw the error
        throw error;
      }
    })
  );
}
  isLoggedIn(): boolean {
    console.log(localStorage.getItem('token'));
    return !!localStorage.getItem('token');
  }

  getUserRole(): Observable<string> {
    return this.userRole$;
  }

  private storeUserData(user: any): void {
    localStorage.setItem('userToken', user.token);
    localStorage.setItem('userRole', user.role);
    localStorage.setItem('user', user.userId);
    console.log('The userID'+localStorage.getItem('user'));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }

  login(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    console.log(loginData);
    return this.http.post<any>(`${this.apiUrl}/api/login`, loginData)
      .pipe(
        tap(response => {
          console.log(response);
          localStorage.setItem('token', response.token);
          localStorage.setItem('currentUser', response.username);
          localStorage.setItem('userRole', response.role);
          localStorage.setItem('user', response.userId);
          this.userRoleSubject.next(response.role);
          this.isAuthenticatedSubject.next(true); // Notify observers that the user is authenticated
        })
      );
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('currentUser');
    localStorage.removeItem('userRole');
    localStorage.removeItem('email');
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');
    console.log(token);
    return !!token; // Return true if the token exists
  }

  isAdmin(): boolean {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('userRole');
    if (token) {
      // console.log("token:"+token);
      if(role === 'admin' || role === 'Admin'){
        return true;
      }
      const decodedToken = this.decodeToken(token);
      return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'admin';
    }
    return false; // Return false if the token is not present or doesn't have 'admin' role
  }

  isCustomer(): boolean {
    // Check if the user has the 'admin' role based on your token structure
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('userRole');
    if (token) {
      // console.log("token:asd"+token);
      if(role === 'customer' || role === 'CUSTOMER'){
        return true;
      }

      // Decode the token and check if it contains the 'admin' role
      const decodedToken = this.decodeToken(token);
      const uname = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      // console.log("dummy"+decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
      if(decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] === 'customer')
      return true;
     // else  return true && uname;
    }
    return false; // Return false if the token is not present or doesn't have 'admin' role
  }
  private decodeToken(token: string): any {
    try {
      var decode = JSON.parse(atob(token.split('.')[1]));
      localStorage.setItem('email', decode.sub);

      console.log(decode['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);

      return decode
    } catch (error) {
      return null;
    }
  }
}
