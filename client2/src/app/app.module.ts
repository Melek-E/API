import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { DxHttpModule } from 'devextreme-angular/http';
import { SideNavOuterToolbarModule, SideNavInnerToolbarModule, SingleCardModule } from './layouts';
import { FooterModule, ResetPasswordFormModule, CreateAccountFormModule, ChangePasswordFormModule, LoginFormComponent } from './shared/components';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { UnauthenticatedContentModule } from './unauthenticated-content';
import { AppRoutingModule } from './app-routing.module';
import { DxButtonModule, DxValidatorModule } from 'devextreme-angular';
import { SignalRService } from './shared/services/signalr.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    DxHttpModule,
    SideNavOuterToolbarModule,
    SideNavInnerToolbarModule,
    SingleCardModule,
    FooterModule,
    ResetPasswordFormModule,
    CreateAccountFormModule,
    ChangePasswordFormModule,
    LoginFormComponent,
    UnauthenticatedContentModule,
    AppRoutingModule,
    // DxButtonModule,
    // DxValidatorModule,

  ],
  providers: [
    AuthService,
    ScreenService,
    AppInfoService,
    SignalRService,
    {provide: APP_INITIALIZER, useFactory: ()=>{}
  }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
