import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

export interface IUser {
  email: string;
  avatarUrl?: string;
}

const defaultPath = '/';
const defaultUser = {
  email: 'sandra@example.com',
  avatarUrl: 'https://js.devexpress.com/Demos/WidgetsGallery/JSDemos/images/employees/06.png'
};

@Injectable()
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
      const response = await this.http.post<any>(`${this.apiUrl}/login`, loginData).toPromise();

      this._user = { ...defaultUser, email };
      this.router.navigate([this._lastAuthenticatedPath]);

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
      const response = await this.http.get<IUser>(`${this.apiUrl}/get-user`).toPromise();

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

  async createAccount(email: string, username: string, password: string): Promise<any> {
    try {
      const registerData = { email, Username:username, passwordHash: password };

      // Send request to register API
      const response = await this.http.post<any>(`${this.apiUrl}/register`, registerData).toPromise();

      this.router.navigate(['/create-account']);
      return {
        isOk: true
      };
    } catch (error) {
      return {
        isOk: false,
        message: 'Failed to create account', error
      };
    }
  }

  async changePassword(email: string, recoveryCode: string): Promise<any> {
    try {
      const changePasswordData = { email, recoveryCode };

      // Send request to change password API
      const response = await this.http.post<any>(`${this.apiUrl}/change-password`, changePasswordData).toPromise();

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
      const response = await this.http.post<any>(`${this.apiUrl}/reset-password`, { email }).toPromise();

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
    // Send request to logout API (if needed) or handle session cleanup
    this._user = null;
    this.router.navigate(['/login-form']);
  }
}

@Injectable()
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
