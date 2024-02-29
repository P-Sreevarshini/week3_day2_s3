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
  userName: string = "";
  password: string = "";
  confirmPassword: string = "";
  mobileNumber: string = "";
  role: string = ""; 
  email: string;
  passwordMismatch: boolean = false;
  emailExistsError: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  register(): void {
    if (this.password !== this.confirmPassword) {
      this.passwordMismatch = true;
      return;
    }
  
    this.passwordMismatch = false;
  
    if (!this.isPasswordComplex(this.password)) {
      return; 
    }
  
    // Check if the email already exists
    this.authService.checkEmailExists(this.email).subscribe(
      (exists) => {
        if (exists) {
          this.emailExistsError = true; 
          alert('Registration failed. User already exists.');
          return; 
        } else if (this.email.endsWith('@admin.com') && this.role === UserRoles.User) {
          alert('Registration failed. Customer cannot register with email ending with \'@admin.com\'.');
          return;
        }
        else {
          // Proceed with registration if email doesn't exist
          this.authService.register(
            this.userName, 
            this.password, 
            this.role, 
            this.email, 
            this.mobileNumber
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
              if (error.error && error.error.Message) {
                alert(error.error.Message); // Display backend error message as alert
              } else {
                alert('Registration failed. An error occurred. Please try again.');
              }
            }
          );
        }
      },
      (error) => {
        console.log(error);
      }
    );
  }

  isPasswordComplex(password: string): boolean {
    const hasUppercase = /[A-Z]/.test(password);
    const hasLowercase = /[a-z]/.test(password);
    const hasDigit = /\d/.test(password);
    const hasSpecialChar = /[!@#$%^&*()_+{}\[\]:;<>,.?~\-]/.test(password);

    return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
  }
}
