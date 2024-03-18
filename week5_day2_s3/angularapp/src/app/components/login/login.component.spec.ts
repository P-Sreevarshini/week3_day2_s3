import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: jasmine.SpyObj<AuthService>;
  let router: jasmine.SpyObj<Router>;

  beforeEach(waitForAsync(() => {
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['login', 'logout']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      providers: [
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router, useValue: routerSpy }
      ]
    }).compileComponents();

    authService = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('loginComponent_should_be_create', () => {
    expect(component).toBeTruthy();
  });

  fit('loginComponent_should_navigate_to_dashboard_on_successful_login', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { /* response data on successful login */ };

    authService.login.and.returnValue(of(mockResponse));

    component.username = username;
    component.password = password;
    component.login();

    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    expect(authService.login).toHaveBeenCalledWith(username, password); // Additional expectation to ensure login method is called
});


});
