import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DashboardComponent } from './dashboard.component';
import { AuthService } from 'src/app/services/auth.service';
import { of } from 'rxjs';

describe('DashboardComponent', () => {
  let component: DashboardComponent;
  let fixture: ComponentFixture<DashboardComponent>;
  let authService: jasmine.SpyObj<AuthService>;

  beforeEach(async () => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['isAdmin']);
    await TestBed.configureTestingModule({
      declarations: [DashboardComponent],
      providers: [{ provide: AuthService, useValue: authServiceSpy }]
    }).compileComponents();

    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardComponent);
    component = fixture.componentInstance;
  });

  fit('DashboardComponent_should_be_create', () => {
    expect(component).toBeTruthy();
  });

  fit('DashboardComponent_should_set_isAdmin_to_true_for_admin', () => {
    authService.isAdmin.and.returnValue(true);
    component.ngOnInit();
    expect(component.isAdmin).toBe(true);
  });

  // it('should set isAdmin to false when user is not an admin', () => {
  //   authService.isAdmin.and.returnValue(false);
  //   component.ngOnInit();
  //   expect(component.isAdmin).toBe(false);
  // });
});
