// auth.guard.ts

import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  username: string;
  password: string;
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    if (this.authService.login(this.username, this.password)) {
      return true;
    } else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
