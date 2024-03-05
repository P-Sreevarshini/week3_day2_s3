import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';



@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<string | null>;
  public currentUser: Observable<string | null>;
  public apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; 
  private userRoleSubject = new BehaviorSubject<string>('');
  userRole$: Observable<string> = this.userRoleSubject.asObservable();

  isAdmin$: Observable<boolean> = this.userRole$.pipe(map(role => role === 'Admin'));
  isCustomer$: Observable<boolean> = this.userRole$.pipe(map(role => role === 'customer'));

  private isAuthenticatedSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(this.isAuthenticated());
  isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<string | null>(
      localStorage.getItem('currentUser')
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }
register(userName: string, password: string, userRole: string, email: string, mobileNumber: string): Observable<any>
 {
  const body = { userName, password, userRole, email, mobileNumber };
  return this.http.post<any>(`${this.apiUrl}/api/register`, body);
}

checkEmailExists(email: string): Observable<boolean> {
  return this.http.get<any>(`${this.apiUrl}/api/check-email/${email}`).pipe(
    catchError(error => {
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
    localStorage.setItem('userToken', user.Token);
    localStorage.setItem('userRole', user.UserRole);
    localStorage.setItem('user', user.UserId);
    console.log('The userId'+localStorage.getItem('user'));
    console.log('The user role'+localStorage.getItem('userRole'));

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
          localStorage.setItem('token', response.Token);
          localStorage.setItem('currentUser', response.username);
          localStorage.setItem('userRole', response.role);
          localStorage.setItem('user', response.userId);
          console.log(localStorage.getItem('userRole'));

          this.userRoleSubject.next(response.role);
          this.isAuthenticatedSubject.next(true); // Notify observers that the user is authenticated
          this.storeUserData(response); // Call storeUserData to store user data in local storage

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
      if(role === 'Admin'){
        return true;
      }
      var decode = JSON.parse(atob(token.split('.')[1]));

      const decodedToken = this.decodeToken(token);
      const uname = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      if(decode.role === 'Admin')
      console.log(decode.role); 

      return true;
    }
    return false; 
  }


  isCustomer(): boolean {
    const token = localStorage.getItem('token');
    const role = localStorage.getItem('userRole');
    if (token) {
      if(role === 'Customer'){
        return true;
      }
      const decodedToken = this.decodeToken(token);
      const uname = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
      return true;
    }
    return false; 
  }
  decodeToken(token: string): any {
    try {
      var decode = JSON.parse(atob(token.split('.')[1]));
      localStorage.setItem('email', decode.sub);
      console.log('Decoded Token:', decode);
      console.log('Decoded Token name:', decode.name);
      console.log('Decoded Role name:', decode.role);
      return decode
    } catch (error) {
      return null;
    }
  }
}
