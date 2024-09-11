import { Routes } from '@angular/router';
import { LoginFormComponent } from './services/login-form.component';
import { HomeComponent } from './home/home.component';
import { CreateAccountFormComponent } from './services/create-account-form/create-account-form.component';
import { ResetPasswordFormComponent } from './services/reset-password-form/reset-password-form.component';
import { SideNavInnerToolbarComponent } from './services/side-nav-inner-toolbar/side-nav-inner-toolbar.component';
import { UserProfileComponent } from './pages/profile/user-profile.component';
import { UnauthorizedComponent } from './errors/unauthorized/unauthorized.component';


const routeConfig: Routes = [
  { path: 'login', component: LoginFormComponent, title: 'Login' },
  { path: 'home', component: HomeComponent, title: 'Home' },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: 'create-account', component: CreateAccountFormComponent, title: 'Create Account' },
  { path: 'reset-password', component: ResetPasswordFormComponent, title: 'Reset Password' },
  { path: 'testtest', component: SideNavInnerToolbarComponent, title: 'Sidepiece' },
  { path: 'secondtest', component: UserProfileComponent, title: 'Sidepiece' },
    { path: 'unauthorized', component: UnauthorizedComponent },
    { path: '**', redirectTo: '/unauthorized' }



];

export default routeConfig;
//don't worry about it
