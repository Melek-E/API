import { bootstrapApplication, provideProtractorTestingSupport } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';

import { provideRouter } from '@angular/router';


import routeConfig from './app/routes';
import { provideHttpClient } from '@angular/common/http';
import { AuthService } from './app/services/shared/auth.service';



bootstrapApplication(AppComponent, {
  providers: [provideHttpClient(), provideProtractorTestingSupport(), provideRouter(routeConfig), AuthService],
}).catch((err) => console.error(err));