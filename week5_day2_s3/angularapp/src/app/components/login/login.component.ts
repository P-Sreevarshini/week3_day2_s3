import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { JwtService } from 'src/app/services/jwt.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username: string;
  password: string;

  constructor(private authService: AuthService, private router: Router, private jwtService: JwtService) {}

  ngOnInit(): void {
    // Ensure token is destroyed when user navigates to login page
    this.authService.logout();
  }

  login(): void {
    this.authService.logout();
    this.authService.login(this.username, this.password).subscribe(
      response => {
        this.router.navigate(['/dashboard']);
      },
      error => {
        // Handle login error
        console.error('Login failed:', error);
      }
    );
  }
}
