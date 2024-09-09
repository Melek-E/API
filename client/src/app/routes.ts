import { Routes } from '@angular/router';
import { LoginFormComponent } from './services/login-form.component';
import { HomeComponent } from './home/home.component';

const routeConfig: Routes = [
  { path: 'login', component: LoginFormComponent, title: 'Login' },
  { path: 'home', component: HomeComponent, title: 'Home' },
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // Default redirect

];

export default routeConfig;
