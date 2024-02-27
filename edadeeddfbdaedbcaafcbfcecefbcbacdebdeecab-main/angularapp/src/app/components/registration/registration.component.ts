import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

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
  emailID: string;
  passwordMismatch: boolean = false; // New property to track password mismatch

  constructor(private authService: AuthService, private router: Router) { }

  register(): void {
    if (this.password !== this.confirmPassword) {
      this.passwordMismatch = true;
      return;
    }

    this.passwordMismatch = false;

    if (!this.isPasswordComplex(this.password)) {
      return; // Password complexity check failed
    }

    this.authService.register(this.userName, this.password, this.role, this.emailID, this.mobileNumber).subscribe(
      (user) => {
        console.log(user);

        console.log(this.role)
        if (user == true && this.role === 'ADMIN') {
          this.router.navigate(['/login']);
        } else if ( user == true && this.role === 'STUDENT') {
          this.router.navigate(['/login']);
        }
      },
      (error) => {
        console.log(error);

        // Handle registration error, display a message, etc.
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
