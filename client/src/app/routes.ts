import { Routes } from '@angular/router';
import { LoginFormComponent } from './services/login-form.component';
import { HomeComponent } from './home/home.component';
import { CreateAccountFormComponent } from './services/create-account-form/create-account-form.component';
import { ResetPasswordFormComponent } from './services/reset-password-form/reset-password-form.component';

const routeConfig: Routes = [
  { path: 'login', component: LoginFormComponent, title: 'Login' },
  { path: 'home', component: HomeComponent, title: 'Home' },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'create-account', component: CreateAccountFormComponent, title: 'Create Account' },
  { path: 'reset-password', component: ResetPasswordFormComponent, title: 'Reset Password' }

];

export default routeConfig;