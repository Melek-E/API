import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface IUser {
  email: string;
  avatarUrl?: string;
}

const defaultPath = '/';
const defaultUser = {
  email: 'sandra@example.com',
  avatarUrl: ''
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _user: IUser | null = defaultUser;
  private apiUrl = 'http://localhost:7112/api/Auth'; // Backend API base URL

  get loggedIn(): boolean {
    return !!this._user;
  }

  private _lastAuthenticatedPath: string = defaultPath;
  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private router: Router, private http: HttpClient) { }

  async logIn(email: string, password: string): Promise<any> {
    try {
      const loginData = { email, passwordHash: password };

      // Send request to login API
      const response = await this.http.post<any>(`${this.apiUrl}/login`, loginData, { withCredentials: true }).toPromise();
      this._user = { ...defaultUser, email };
      
      // Navigate to the desired path after login
      this.router.navigate(['/profile']);

      return {
        isOk: true,
        data: this._user
      };
    } catch (error) {
      return {
        isOk: false,
        message: 'Authentication failed'
      };
    }
  }

  async getUser(): Promise<any> {
    try {
      // Send request to get user data from API
      const response = await this.http.get<IUser>(`${this.apiUrl}/profile`, { withCredentials: true }).toPromise();

      return {
        isOk: true,
        data: response
      };
    } catch (error) {
      return {
        isOk: false,
        data: null
      };
    }
  }
// auth.service.ts
async createAccount(email: string, username: string, password: string, frameworks: { Name: string }[]): Promise<any> {
  try {
    // Add frameworks to the register data
    const registerData = { email, Username: username, passwordHash: password, frameworks };

    // Send request to register API
    const response = await this.http.post<any>("http://localhost:7112/api/Auth/register", registerData, { withCredentials: true }).toPromise();

    this.router.navigate(['/create-account']);
    return {
      isOk: true
    };
  } catch (error) {
    return {
      isOk: false,
      message: 'Failed to create account',
      error
    };
  }
}

  async changePassword(email: string, recoveryCode: string): Promise<any> {
    try {
      const changePasswordData = { email, recoveryCode };

      // Send request to change password API
      const response = await this.http.post<any>(`${this.apiUrl}/change-password`, changePasswordData, { withCredentials: true }).toPromise();

      return {
        isOk: true
      };
    } catch (error) {
      return {
        isOk: false,
        message: 'Failed to change password'
      };
    }
  }

  async resetPassword(email: string): Promise<any> {
    try {
      // Send request to reset password API
      const response = await this.http.post<any>(`${this.apiUrl}/reset-password`, { email }, { withCredentials: true }).toPromise();

      return {
        isOk: true
      };
    } catch (error) {
      return {
        isOk: false,
        message: 'Failed to reset password'
      };
    }
  }

  async logOut(): Promise<void> {
    try {
      // Send POST request to the logout API
      await this.http.post<any>(`${this.apiUrl}/logout`, {}, { withCredentials: true }).toPromise();

      // Clear user data on the frontend
      this._user = null;

      // Navigate to the login page after logging out
      this.router.navigate(['/login-form']);
    } catch (error) {
      console.error('Failed to log out:', error);
      this._user = null;
      this.router.navigate(['/login-form']);

    }
  }
}

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private router: Router, private authService: AuthService) { }

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const isLoggedIn = this.authService.loggedIn;
    const isAuthForm = [
      'login-form',
      'reset-password',
      'create-account',
      'change-password/:recoveryCode'
    ].includes(route.routeConfig?.path || defaultPath);

    if (isLoggedIn && isAuthForm) {
      this.authService.lastAuthenticatedPath = defaultPath;
      this.router.navigate([defaultPath]);
      return false;
    }

    if (!isLoggedIn && !isAuthForm) {
      this.router.navigate(['/login-form']);
    }

    if (isLoggedIn) {
      this.authService.lastAuthenticatedPath = route.routeConfig?.path || defaultPath;
    }

    return isLoggedIn || isAuthForm;
  }
}
