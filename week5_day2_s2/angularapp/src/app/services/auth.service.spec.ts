import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { JwtService } from './jwt.service';

describe('AuthService', () => {
  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService, JwtService]
    });
    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('should be created', () => {
    expect(service).toBeTruthy();
  });

  fit('should call the API to login and save token', () => {
    const mockResponse = { token: 'sample_token' };
    const username = 'testUser';
    const password = 'testPassword';
  
    service.login(username, password).subscribe(response => {
      expect(response).toEqual(mockResponse);
      expect(service.isLoggedIn()).toBeTruthy(); 
    });
  
    const request = httpMock.expectOne(`${service.apiUrl()}/api/login`);
    expect(request.request.method).toBe('POST');
    request.flush(mockResponse);
  });
  

  fit('should logout user and remove token', () => {
    spyOn(service.jwtService, 'destroyToken');
    service.logout();
    expect(service.jwtService.destroyToken).toHaveBeenCalled();
    expect(service.isLoggedIn()).toBeFalsy(); // Check if logged out after logout
  });

});
