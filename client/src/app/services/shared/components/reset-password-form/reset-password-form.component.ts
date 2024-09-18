import { CommonModule, NgIf } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { Router, RouterModule, RouterLink } from '@angular/router';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { DxTemplateModule } from 'devextreme-angular/core';
import { DxiItemModule, DxiValidationRuleModule, DxoLabelModule, DxoButtonOptionsModule } from 'devextreme-angular/ui/nested';
import { AuthService } from '../../auth.service';

const notificationText = 'We\'ve sent a link to reset your password. Check your inbox.';

@Component({
    selector: 'app-reset-password-form',
    templateUrl: './reset-password-form.component.html',
    styleUrls: ['./reset-password-form.component.scss'],
    standalone: true,
    imports: [DxFormModule, DxiItemModule, DxiValidationRuleModule, DxoLabelModule, DxoButtonOptionsModule, RouterLink, DxTemplateModule, NgIf, DxLoadIndicatorModule]
})
export class ResetPasswordFormComponent {
  loading = false;
  formData: any = {};

  constructor(private authService: AuthService, private router: Router) { }

  async onSubmit(e: Event) {
    e.preventDefault();
    const { email } = this.formData;
    this.loading = true;

    const result = await this.authService.resetPassword(email);
    this.loading = false;

    if (result.isOk) {
      this.router.navigate(['/login-form']);
      notify(notificationText, 'success', 2500);
    } else {
      notify(result.message, 'error', 2000);
    }
  }
}

