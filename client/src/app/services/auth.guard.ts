import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from './shared/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private router: Router, private authService: AuthService) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn; // Use the loggedIn getter
    const isAuthForm = [
      'login-form',
      'reset-password',
      'create-account',
      'change-password/:recoveryCode'
    ].includes(route.routeConfig?.path || '/');

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = '/';
      this.router.navigate(['/']);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/unauthorized']);
      return false;
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig?.path || '/';
    }

    return isLoggedIn || isAuthForm;
  }
}
