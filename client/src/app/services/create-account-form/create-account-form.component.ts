import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ValidationCallbackData } from 'devextreme-angular/common';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { AuthService } from '../shared/auth.service';


@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent {
  loading = false;
  formData: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  async onSubmit(e: Event) {
    e.preventDefault();
    const { email, username, passwordHash } = this.formData;
    this.loading = true;

    const result = await this.authService.createAccount(email, username, passwordHash);
    this.loading = false;

    if (result.isOk) {
      this.router.navigate(['/login']);
    } else {
      notify(result.message, 'error', 2000);
    }
  }

  ConfirmPassword = (e: ValidationCallbackData) => {
    return e.value === this.formData.passwordHash;
  } //test
}
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule
  ],
  declarations: [CreateAccountFormComponent],
  exports: [CreateAccountFormComponent]
})
export class CreateAccountFormModule { }
