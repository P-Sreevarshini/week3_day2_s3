import { TestBed, async } from '@angular/core/testing';
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
    const mockToken = 'mockToken';
    const mockFixedDeposit: FixedDeposit = {
      FixedDepositId: 1,
      amount: 10000,
      tenureMonths: 12,
      interestRate: 5
    };

    spyOn(localStorage, 'getItem').and.returnValue(mockToken);

    service.saveFdByAdmin(mockFixedDeposit).subscribe();

    const req = httpMock.expectOne(`${service.apiUrl}/api/fixeddeposit`);
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe('Bearer ' + mockToken);

    req.flush(mockFixedDeposit);
  });
});
