import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { UserRoles } from 'src/app/models/userRole.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  UserName: string = "";
  Password: string = "";
  confirmPassword: string = "";
  MobileNumber: string = "";
  role: string = ""; 
  Email: string;
  passwordMismatch: boolean = false;
  emailExistsError: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  register(): void {
    if (this.Password !== this.confirmPassword) {
      this.passwordMismatch = true;
      return;
    }
  
    this.passwordMismatch = false;
  
    if (!this.isPasswordComplex(this.Password)) {
      return; 
    }
  
    if (this.MobileNumber.length !== 10 || !/^\d+$/.test(this.MobileNumber)) {
      alert('Registration failed. Phone number must have exactly 10 digits.');
      return;
    }
  
    // Proceed with registration
    this.authService.register(
      this.UserName, 
      this.Password, 
      this.role, 
      this.Email, 
      this.MobileNumber
    ).subscribe(
      (success) => {
        if (success) {
          alert('Registration Successful');
          this.router.navigate(['login']);
        } else {
          alert('Registration failed. An error occurred. Please try again.');
        }
      },
      (error) => {
        console.log(error);
        if (error.status === 400 && error.error && error.error.Message === "User already exists") {
          alert('Registration failed. User already exists.');
        } else {
          alert('Registration failed. An error occurred. Please try again.');
        }
      }
    );
  }
  

  isPasswordComplex(Password: string): boolean {
    const hasUppercase = /[A-Z]/.test(Password);
    const hasLowercase = /[a-z]/.test(Password);
    const hasDigit = /\d/.test(Password);
    const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(Password);

    return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
  }
}
