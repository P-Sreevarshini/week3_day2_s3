import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';

describe('AuthService', () => {
  let authService: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });

    authService = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify(); // Verifies that no requests are outstanding after each test
    localStorage.removeItem('loggedIn'); // Clean up localStorage after each test
  });

  fit('should be created', () => {
    expect(authService).toBeTruthy();
  });

  // fit('should set loggedIn to true when login is successful', () => {
  //   const username = 'testUser';
  //   const password = 'testPassword';

  //   const mockResponse = { message: 'Login successful' };

  //   authService.login(username, password).subscribe(() => {
  //     expect(localStorage.getItem('loggedIn')).toBe('true');
  //   });

  //   const req = httpMock.expectOne(`${authService.apiUrl}/api/login`);
  //   expect(req.request.method).toBe('POST');
  //   req.flush(mockResponse);
  // });

  fit('should return true from isLoggedIn when loggedIn is set to true', () => {
    localStorage.setItem('loggedIn', 'true');
    expect(authService.isLoggedIn()).toBe(true);
  });

  fit('should return false from isLoggedIn when loggedIn is set to false', () => {
    localStorage.setItem('loggedIn', 'false');
    expect(authService.isLoggedIn()).toBe(false);
  });

  fit('should return false from isLoggedIn when loggedIn is not set', () => {
    expect(authService.isLoggedIn()).toBe(false);
  });
});
