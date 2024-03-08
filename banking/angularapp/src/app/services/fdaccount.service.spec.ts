import { TestBed } from '@angular/core/testing';
import { FdAccountService } from './fdaccount.service';


describe('FdaccountService', () => {
  let service: FdAccountService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FdAccountService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
