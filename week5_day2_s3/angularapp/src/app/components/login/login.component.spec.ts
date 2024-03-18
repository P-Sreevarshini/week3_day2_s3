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

  it('should call logout method of AuthService on initialization', () => {
    expect(authService.logout).toHaveBeenCalled();
  });

  it('should call login method of AuthService and navigate to dashboard on successful login', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { /* response data on successful login */ };

    authService.login.and.returnValue(of(mockResponse));

    component.username = username;
    component.password = password;
    component.login();

    expect(authService.login).toHaveBeenCalledWith(username, password);
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });

  it('should handle login failure', () => {
    const errorMessage = 'Invalid credentials';
    const errorResponse = new Error(errorMessage);

    authService.login.and.returnValue(throwError(errorResponse));

    component.login();

    expect(console.error).toHaveBeenCalledWith('Login failed:', errorResponse);
    expect(component.errorMessage).toEqual('Invalid username or password.');
  });
});
