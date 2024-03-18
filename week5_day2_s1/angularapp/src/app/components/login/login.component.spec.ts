import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
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
    const authServiceSpy = jasmine.createSpyObj('AuthService', ['login']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      imports: [RouterTestingModule],
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

  fit('Login_component_should_be_create', () => {
    expect(component).toBeTruthy();
  });

//   fit('Login_component_should_call_authService.login_navigate to dashboard on successful login', () => {
//     const username = 'testUser';
//     const password = 'testPassword';
//     const mockResponse = { message: 'Login successful' };

//     authService.login.and.returnValue(of(mockResponse));

//     component.username = username;
//     component.password = password;
//     component.login();

//     expect(authService.login).toHaveBeenCalledWith(username, password);
//     expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
//   });

//   fit('should handle login failure', () => {
//     const errorMessage = 'Invalid credentials';
//     const errorResponse = new Error(errorMessage);

//     authService.login.and.returnValue(throwError(errorResponse));

//     component.login();

//     expect(authService.login).toHaveBeenCalled();
//     expect(console.error).toHaveBeenCalledWith('Login failed:', errorResponse);
//   });

fit('should call authService.login on login attempt', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { message: 'Login successful' };
  
    authService.login.and.returnValue(of(mockResponse));
  
    component.username = username;
    component.password = password;
    component.login();
  
    expect(authService.login).toHaveBeenCalledWith(username, password);
  });
  
  fit('should navigate to dashboard on successful login', () => {
    const username = 'testUser';
    const password = 'testPassword';
    const mockResponse = { message: 'Login successful' };
  
    authService.login.and.returnValue(of(mockResponse));
  
    component.username = username;
    component.password = password;
    component.login();
  
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  });
  


});
