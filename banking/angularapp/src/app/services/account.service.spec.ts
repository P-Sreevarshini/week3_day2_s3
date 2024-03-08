import { TestBed, async } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AccountService } from './account.service';
import { Account } from '../models/account.model';

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
  fit('should retrieve customer accounts by user ID', async(() => {
    const userId = 123; // Replace with a valid user ID

    const mockAccounts: Account[] = [
      { AccountId: 1, UserId: userId, AccountType: 'Savings', Balance: 1000 },
      { AccountId: 2, UserId: userId, AccountType: 'Checking', Balance: 500 }
    ];

    service.getCustomerAccounts(userId).subscribe((accounts) => {
      expect(accounts.length).toBe(2);
      expect(accounts).toEqual(mockAccounts);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/api/account/${userId}`);
    expect(req.request.method).toBe('GET');

    req.flush(mockAccounts);
  }));
});
