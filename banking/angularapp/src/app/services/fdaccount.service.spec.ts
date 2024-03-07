import { TestBed } from '@angular/core/testing';

import { FdaccountService } from './fdaccount.service';

describe('FdaccountService', () => {
  let service: FdaccountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FdaccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
