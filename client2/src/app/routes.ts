import { Routes } from '@angular/router';

import { SideNavOuterToolbarComponent } from './layouts';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { CreateAccountFormComponent, HeaderComponent, LoginFormComponent, ResetPasswordFormComponent } from './shared/components';
import { AuthGuardService } from './shared/services';
import { UnauthenticatedContentComponent } from './unauthenticated-content';
import { ProfileComponent } from './pages/profile/profile.component';


const routeConfig: Routes = [
  { path: 'login', component: LoginFormComponent, title: 'Login' },
  { path: 'home', component: HomeComponent, title: 'Home' },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'create-account', component: CreateAccountFormComponent, title: 'Create Account' },
  { path: 'reset-password', component: ResetPasswordFormComponent, title: 'Reset Password' },
  { path: 'profile', component: ProfileComponent, title: 'Profile', canActivate: [AuthGuardService] },
  { path: 'nav', component: SideNavOuterToolbarComponent, title: 'Profile'},
  { path: 'applicationdo', component: HeaderComponent, title: 'Profile'},

  { path: 'temp', component: CreateAccountFormComponent, title: 'TEMP PR'},


    { path: 'unauthorized', component: UnauthenticatedContentComponent }
  

];

export default routeConfig;
//don't worry about it
