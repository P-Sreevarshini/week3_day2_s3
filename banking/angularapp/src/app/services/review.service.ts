import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { Review } from '../models/review.model';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) {}

  addReview(review: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });

    return this.http.post(`${this.apiUrl}/api/Review`, review, { headers });
  }

  getAllReviews(): Observable<Review[]> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Review[]>(`${this.apiUrl}/api/Review`, { headers });
  }

  getReviewsByUserId(userId: string): Observable<Review[]> {
    // localStorage.setItem('user', response.userId);

    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<Review[]>(`${this.apiUrl}/api/Review/${userId}`, { headers });
  }


  deleteReview(review: Review): Observable<any> {
    if (!review || !review.UserId) {
      console.error('Invalid review object or UserId is undefined.');
      return throwError('Invalid review object or UserId is undefined.');
    }
  
    const role = localStorage.getItem('userRole');
    
    if (role !== 'Customer') {
      console.error('Access denied. Only customers can delete reviews.');
      return throwError('Access denied. Only customers can delete reviews.');
    }
    
    const userId = localStorage.getItem('user'); // Get the user ID
    const endpoint = `${this.apiUrl}/reviews/${userId}`; // Pass the user ID instead of review ID
    const authToken = localStorage.getItem('token');
    const headers = authToken ? new HttpHeaders({ 'Authorization': `Bearer ${authToken}` }) : undefined;
    const options = { headers };
  
    return this.http.delete(endpoint, options).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }
  
}
