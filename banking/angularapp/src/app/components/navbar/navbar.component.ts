import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = true;
  isAdmin: boolean = true;
  isCustomer: boolean = true;

  constructor(private authService: AuthService, private router: Router) {
    this.authService.isAuthenticated$.subscribe((authenticated) => {
      this.isLoggedIn = authenticated;
      if (this.isLoggedIn) {
        this.authService.getUserRole().subscribe(() => {
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
      this.authService.getUserRole().subscribe(() => {
        this.isAdmin = this.authService.isAdmin(); // <-- Correct way to call the isAdmin method
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
