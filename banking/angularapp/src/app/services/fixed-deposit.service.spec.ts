import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

import { FixedDepositService } from './fixed-deposit.service';
import { FixedDeposit } from '../models/fixedDeposit.model';

describe('FixedDepositService', () => {
  let service: FixedDepositService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [FixedDepositService],
    });

    service = TestBed.inject(FixedDepositService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  fit('Frontend_should save fixed deposit by admin', () => {
    const mockFdData: FixedDeposit = {
      FixedDepositId: 1,
      amount: 10000,
      tenureMonths: 12,
      interestRate: 6,
    };

    const mockToken = 'mockToken';

    (service as any).saveFdByAdmin(mockFdData, mockToken).subscribe();

    const req = httpMock.expectOne(`${(service as any).apiUrl}/api/fixeddeposit`);
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe('Bearer ' + mockToken);

    // provide a mock response
    req.flush(mockFdData);
  });
});
