import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditFdComponent } from './edit-fd.component';

describe('EditFdComponent', () => {
  let component: EditFdComponent;
  let fixture: ComponentFixture<EditFdComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditFdComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditFdComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
