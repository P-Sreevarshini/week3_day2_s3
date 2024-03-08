import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AccountService } from './account.service';

describe('AccountService', () => {
  let service: AccountService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AccountService],
    });

    service = TestBed.inject(AccountService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_should get all accounts by admin', () => {
    const mockToken = 'mockToken';

    // Mock the localStorage getItem method to return the mock token
    spyOn(localStorage, 'getItem').and.returnValue(mockToken);

    service.getAllAccounts().subscribe((accounts) => {
      expect(accounts).toBeTruthy();
    });

    const req = httpMock.expectOne(`${service.apiUrl}/api/account`);
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe('Bearer ' + mockToken);
  });
});
