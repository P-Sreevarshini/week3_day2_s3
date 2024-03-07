import { TestBed } from '@angular/core/testing';

import { FixedDepositAccountService } from './fixed-deposit-account.service';

describe('FixedDepositAccountService', () => {
  let service: FixedDepositAccountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FixedDepositAccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
