import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  isCustomer: boolean = false;

  constructor(private authService: AuthService, private router: Router) {
    this.authService.isAuthenticated$.subscribe((authenticated) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.authService.getUserRole().subscribe((role) => {
          this.isAdmin = this.authService.isAdmin();
          this.isCustomer = this.authService.isCustomer();
        });
      } else {
        this.isAdmin = false;
        this.isCustomer = false;
      }
    });
  }

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    if (this.isLoggedIn) {
      this.authService.getUserRole().subscribe((role) => {
        this.isAdmin = this.authService.isAdmin();
        this.isCustomer = this.authService.isCustomer();
      });
    }
  }

  logout(): void {
    this.isLoggedIn = false;
    this.isAdmin = false;
    this.isCustomer = false;
    this.authService.logout();
    this.router.navigate(['login']);
  }
}