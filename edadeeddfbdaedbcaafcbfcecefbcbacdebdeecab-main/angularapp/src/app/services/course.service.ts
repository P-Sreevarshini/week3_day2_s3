import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError  } from 'rxjs';
import { Course } from 'src/app/models/course.model';
import { JwtService } from './jwt.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  public apiUrl = 'http://localhost:8080';

  constructor(private http: HttpClient, private jwtService: JwtService) {}

  private getHeaders(): HttpHeaders {
    // const token = this.jwtService.getToken();
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`
    });
  }

  getAllCourses(): Observable<Course[]> {
    const role = localStorage.getItem('userRole');
    console.log(role);

    let endpoint;
    if (role === 'ADMIN' || role === 'admin') {
      endpoint = `${this.apiUrl}/api/course`;
    } else if (role === 'STUDENT' || role === 'student') {
      endpoint = `${this.apiUrl}/student/getAllcourses`;
    } else {
      console.error('Access denied. Invalid role.');
      return;
    }
    
    const authToken = localStorage.getItem('token');
    const headers = authToken ? new HttpHeaders({ 'Authorization': `Bearer ${authToken}` }) : undefined;
    const options = { headers };
    console.log(headers);

    return this.http.get<Course[]>(endpoint, options).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );

  }
  

  saveCourseByAdmin(course: Course): Observable<Course> {
    // const role = this.jwtService.getUserRole();
    const role = localStorage.getItem('userRole');
    // const role1 = localStorage.getItem('token');
    console.log(role);
    
    if (role !== 'ADMIN' && role !== 'admin') {
      console.error('Access denied. Only admins can add courses.');
      return;
    }
    const endpoint = `${this.apiUrl}/api/course`;
    const authToken = localStorage.getItem('token');
    const headers = authToken ? new HttpHeaders({ 'Authorization': `Bearer ${authToken}` }) : undefined;
    const options = { headers };
    console.log(headers);


    return this.http.post<Course>(endpoint, course, options).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );

    // return this.http.post(this.apiUrl, course, { headers: this.getHeaders() });
  }

  updateCourseByAdmin(course: Course): Observable<Course> {
    // const role = this.jwtService.getUserRole();
    const role = localStorage.getItem('userRole');
    // const role1 = localStorage.getItem('token');
    console.log(role);
    
    if (role !== 'ADMIN' && role !== 'admin') {
      console.error('Access denied. Only admins can add courses.');
      return;
    }
    const endpoint = `${this.apiUrl}/api/course/${course.courseID}`;
    const authToken = localStorage.getItem('token');
    const headers = authToken ? new HttpHeaders({ 'Authorization': `Bearer ${authToken}` }) : undefined;
    const options = { headers };
    console.log(headers);

    return this.http.put<Course>(endpoint, course, options).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  deleteCourseByAdmin(course: Course): Observable<Course> {
    // const role = this.jwtService.getUserRole();
    const role = localStorage.getItem('userRole');
    // const role1 = localStorage.getItem('token');
    console.log(role);
    
    if (role !== 'ADMIN' && role !== 'admin') {
      console.error('Access denied. Only admins can add courses.');
      return;
    }
    const endpoint = `${this.apiUrl}/api/course/${course.courseID}`;
    const authToken = localStorage.getItem('token');
    const headers = authToken ? new HttpHeaders({ 'Authorization': `Bearer ${authToken}` }) : undefined;
    const options = { headers };
    console.log(headers);

    return this.http.delete<Course>(endpoint, options).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );
  }

  getStudentCourses(): Observable<Course[]> {
    const role = localStorage.getItem('userRole');
    console.log(role);

    let endpoint;
    if (role === 'ADMIN' || role === 'admin') {
      endpoint = `${this.apiUrl}/api/course`;
    } else if (role === 'STUDENT' || role === 'student') {
      endpoint = `${this.apiUrl}/api/student/course`;
    } else {
      console.error('Access denied. Invalid role.');
      return;
    }
    
    const authToken = localStorage.getItem('token');
    const headers = authToken ? new HttpHeaders({ 'Authorization': `Bearer ${authToken}` }) : undefined;
    const options = { headers };
    console.log(headers);

    return this.http.get<Course[]>(endpoint, options).pipe(
      catchError((error) => {
        if (error.status === 401) {
          console.error('Authentication error: Redirect to login page or handle accordingly.');
        }
        return throwError(error);
      })
    );

  }
}
