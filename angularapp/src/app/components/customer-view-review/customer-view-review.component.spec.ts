import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerViewReviewComponent } from './customer-view-review.component';

describe('CustomerViewReviewComponent', () => {
  let component: CustomerViewReviewComponent;
  let fixture: ComponentFixture<CustomerViewReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CustomerViewReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerViewReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
