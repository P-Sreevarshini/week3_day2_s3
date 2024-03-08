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
  isAdmin: boolean = false;
  isCustomer: boolean = false;

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
        this.isAdmin = this.authService.isAdmin();
        this.isCustomer = this.authService.isCustomer();
      });
    } else {
      // If the user is not logged in, set isAdmin and isCustomer to false
      this.isAdmin = false;
      this.isCustomer = false;
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
// import { Component, OnDestroy, OnInit } from '@angular/core';
// import { Router } from '@angular/router';
// import { AuthService } from '../../services/auth.service';
// import { Subscription } from 'rxjs';

// @Component({
//   selector: 'app-navbar',
//   templateUrl: './navbar.component.html',
//   styleUrls: ['./navbar.component.css']
// })
// export class NavbarComponent implements OnInit, OnDestroy {
//   isLoggedIn: boolean = true;
//   isAdmin: boolean = false;
//   isCustomer: boolean = false;
//   isAuthenticatedSubscription: Subscription;

//   constructor(private authService: AuthService, private router: Router) {}

//   ngOnInit(): void {
//     this.isAuthenticatedSubscription = this.authService.isAuthenticated$.subscribe((authenticated) => {
//       this.isLoggedIn = authenticated;
//       if (this.isLoggedIn) {
//         this.authService.getUserRole().subscribe((role) => {
//           this.isAdmin = role === 'Admin';
//           this.isCustomer = role === 'Customer';
//         });
//       } else {
//         this.isAdmin = false;
//         this.isCustomer = false;
//       }
//     });
//   }

//   ngOnDestroy(): void {
//     if (this.isAuthenticatedSubscription) {
//       this.isAuthenticatedSubscription.unsubscribe();
//     }
//   }

//   logout(): void {
//     this.isLoggedIn = false;
//     this.isAdmin = false;
//     this.isCustomer = false;
//     this.authService.logout();
//     this.router.navigate(['login']);
//   }
// }
