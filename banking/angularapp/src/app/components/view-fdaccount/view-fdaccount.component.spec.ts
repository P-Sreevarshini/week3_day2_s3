import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewFdaccountComponent } from './view-fdaccount.component';

describe('ViewFdaccountComponent', () => {
  let component: ViewFdaccountComponent;
  let fixture: ComponentFixture<ViewFdaccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewFdaccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewFdaccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
