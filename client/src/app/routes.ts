import { Routes } from '@angular/router';
import { LoginFormComponent } from './services/login-form.component';
import { HomeComponent } from './home/home.component';
import { CreateAccountFormComponent } from './services/create-account-form/create-account-form.component';
import { ResetPasswordFormComponent } from './services/reset-password-form/reset-password-form.component';
import { UserProfileComponent } from './pages/profile/user-profile.component';
import { UnauthorizedComponent } from './errors/unauthorized/unauthorized.component';
import { AuthGuardService } from './services/auth.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { StolenProfileComponent } from './pages/user-profile/stolenprofile.component';
import { SideNavigationMenuComponent } from './services/shared/components';
import { SideNavOuterToolbarComponent } from './layouts';
import { AppComponent } from './app.component';


const routeConfig: Routes = [
  { path: 'login', component: LoginFormComponent, title: 'Login' },
  { path: 'home', component: HomeComponent, title: 'Home' },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'create-account', component: CreateAccountFormComponent, title: 'Create Account' },
  { path: 'reset-password', component: ResetPasswordFormComponent, title: 'Reset Password' },
  { path: 'profile', component: UserProfileComponent, title: 'Profile', canActivate: [AuthGuardService] },
  { path: 'stolen', component: StolenProfileComponent, title: 'Profile', canActivate: [AuthGuardService] },
  { path: 'nav', component: SideNavOuterToolbarComponent, title: 'Profile'},
  { path: 'App', component: AppComponent, title: 'Profile'},



    { path: 'unauthorized', component: UnauthorizedComponent }
  

];

export default routeConfig;
//don't worry about it
