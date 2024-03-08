import { TestBed, async } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FdAccountService } from './fdaccount.service';
import { FDAccount } from '../models/fixedDepositAccount';

describe('FdAccountService', () => {
  let service: FdAccountService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FdAccountService],
    });

    service = TestBed.inject(FdAccountService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_FdAccountService should get all FD accounts by admin', () => {
    const mockFdAccounts: FDAccount[] = [
      { FDAccountId: 1, UserId: 1, Status: 'Active', FixedDepositId: 1 },
      { FDAccountId: 2, UserId: 2, Status: 'Inactive', FixedDepositId: 2 }
    ];

    service.getAllFdAccounts().subscribe((fdAccounts) => {
      expect(fdAccounts.length).toBe(2);
      expect(fdAccounts).toEqual(mockFdAccounts);
    });

    const req = httpMock.expectOne(`${service.apiUrl}/api/FDAccount`);
    expect(req.request.method).toBe('GET');

    req.flush(mockFdAccounts);
  });
});
