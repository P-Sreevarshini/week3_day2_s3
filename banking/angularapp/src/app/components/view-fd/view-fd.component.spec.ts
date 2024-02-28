import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewFdComponent } from './view-fd.component';

describe('ViewFdComponent', () => {
  let component: ViewFdComponent;
  let fixture: ComponentFixture<ViewFdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewFdComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewFdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
