import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { JwtInterceptor } from './jwt.interceptor';
import { JwtService } from '../services/jwt.service';

describe('JwtInterceptor', () => {
  let interceptor: JwtInterceptor;
  let httpMock: HttpTestingController;
  let httpClient: HttpClient;
  let jwtService: JwtService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        JwtInterceptor,
        JwtService,
        {
          provide: HTTP_INTERCEPTORS,
          useClass: JwtInterceptor,
          multi: true,
        },
      ],
    });

    interceptor = TestBed.inject(JwtInterceptor);
    httpMock = TestBed.inject(HttpTestingController);
    httpClient = TestBed.inject(HttpClient);
    jwtService = TestBed.inject(JwtService);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('JwtInterceptor_should_add_authorization_header_with_token', () => {
    const token = 'sample_token';
    spyOn(jwtService, 'getToken').and.returnValue(token);

    httpClient.get('/api/data').subscribe();
    const httpRequest = httpMock.expectOne('/api/data');

    expect(httpRequest.request.headers.has('Authorization')).toBeTruthy();
    expect(httpRequest.request.headers.get('Authorization')).toBe(`Bearer ${token}`);
  });

  fit('JwtInterceptor_should_not_add_authorization_header_if_token_is_not_present', () => {
    spyOn(jwtService, 'getToken').and.returnValue(null);

    httpClient.get('/api/data').subscribe();
    const httpRequest = httpMock.expectOne('/api/data');

    expect(httpRequest.request.headers.has('Authorization')).toBeFalsy();
  });
});
