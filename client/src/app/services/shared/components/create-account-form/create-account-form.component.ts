import { CommonModule, NgIf } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { Router, RouterModule, RouterLink } from '@angular/router';
import { ValidationCallbackData } from 'devextreme-angular/common';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { DxTemplateModule } from 'devextreme-angular/core';
import { DxiItemModule, DxiValidationRuleModule, DxoLabelModule, DxoButtonOptionsModule } from 'devextreme-angular/ui/nested';
import { AuthService } from '../../auth.service';


@Component({
    selector: 'app-create-account-form',
    templateUrl: './create-account-form.component.html',
    styleUrls: ['./create-account-form.component.scss'],
    standalone: true,
    imports: [DxFormModule, DxiItemModule, DxiValidationRuleModule, DxoLabelModule, RouterLink, DxoButtonOptionsModule, DxTemplateModule, NgIf, DxLoadIndicatorModule]
})
export class CreateAccountFormComponent {
  loading = false;
  formData: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  async onSubmit(e: Event) {
    e.preventDefault();
    const { email, password } = this.formData;
    this.loading = true;

    const result = await this.authService.createAccount(email, email, password);
    this.loading = false;

    if (result.isOk) {
      this.router.navigate(['/login-form']);
    } else {
      notify(result.message, 'error', 2000);
    }
  }

  confirmPassword = (e: ValidationCallbackData) => {
    return e.value === this.formData.password;
  }
}

