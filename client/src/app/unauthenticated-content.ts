import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

import { Router } from '@angular/router';
import { SingleCardComponent } from './layouts/single-card/single-card.component';

@Component({
    selector: 'app-unauthenticated-content',
    template: `
    <app-single-card [title]="title" [description]="description">
      <router-outlet></router-outlet>
    </app-single-card>
  `,
    styles: [`
    :host {
      width: 100%;
      height: 100%;
    }
  `],
    standalone: true,
    imports: [SingleCardComponent, RouterOutlet]
})
export class UnauthenticatedContentComponent {

  constructor(private router: Router) { }

  get title() {
    const path = this.router.url.split('/')[1];
    switch (path) {
      case 'login-form': return 'Sign In';
      case 'reset-password': return 'Reset Password';
      case 'create-account': return 'Sign Up';
      case 'change-password': return 'Change Password';
      default: return '';
    }
  }

  get description() {
    const path = this.router.url.split('/')[1];
    switch (path) {
      case 'reset-password': return 'Please enter the email address that you used to register, and we will send you a link to reset your password via Email.';
      default: return '';
    }
  }
}

