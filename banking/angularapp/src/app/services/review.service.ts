import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  apiUrl = 'https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io'; // Replace this with your API endpoint

  constructor(private http: HttpClient) {}

addReview(review: any): Observable<any> {
  const token = localStorage.getItem('token');
  // console.log(token)
  const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  });

  return this.http.post(`${this.apiUrl}/api/Review`, review, { headers });
}

getAllReviews(){
  const token = localStorage.getItem('token');
  // console.log(token)
  const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  });
  return this.http.get(`${this.apiUrl}/api/Review`, { headers });
}

getReviewsByUserId(){
  const userId = localStorage.getItem('userId');
  const token = localStorage.getItem('token');
  // console.log(token)
  const headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}` // Assuming your token is a bearer token, replace it accordingly
  });
  return this.http.get(`${this.apiUrl}/api/Review/${userId}`, { headers });
}
}