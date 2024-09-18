import { Component, HostBinding } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NgIf } from '@angular/common';
import { SideNavOuterToolbarComponent } from './layouts/side-nav-outer-toolbar/side-nav-outer-toolbar.component';
import { SideNavInnerToolbarComponent } from './layouts/side-nav-inner-toolbar/side-nav-inner-toolbar.component';

import { FooterComponent } from './services/shared/components';
import { AppInfoService, AuthService, DataService, ScreenService } from './services/shared';




@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
    standalone: true,
    imports: [NgIf, RouterOutlet, SideNavInnerToolbarComponent,SideNavOuterToolbarComponent,FooterComponent],
    providers: [ScreenService, DataService, AppInfoService]
})
export class AppComponent  {
  @HostBinding('class') get getClass() {
    return Object.keys(this.screen.sizes).filter(cl => this.screen.sizes[cl]).join(' ');
  }

  constructor(private authService: AuthService, private screen: ScreenService, public appInfo: AppInfoService) { }

  isAuthenticated() {
    return this.authService.loggedIn;
  }
}
