import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFdaccountComponent } from './add-fdaccount.component';

describe('AddFdaccountComponent', () => {
  let component: AddFdaccountComponent;
  let fixture: ComponentFixture<AddFdaccountComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddFdaccountComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddFdaccountComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
