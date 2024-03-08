import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ViewFdAccountsComponent } from './view-fdaccount.component';


describe('ViewFdAccountsComponent', () => {
  let component: ViewFdAccountsComponent;
  let fixture: ComponentFixture<ViewFdAccountsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ViewFdAccountsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewFdAccountsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
