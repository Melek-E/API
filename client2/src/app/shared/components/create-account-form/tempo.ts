import { CommonModule } from '@angular/common';
import { Component, NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ValidationCallbackData } from 'devextreme-angular/common';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import notify from 'devextreme/ui/notify';
import { AuthService } from '../../services';
import dxTextBox from 'devextreme/ui/text_box';
import { DxButtonModule, DxTextBoxModule } from 'devextreme-angular';


// registerpage.component.ts
export class CreateAccountFormComponent {
  loading = false;
  formData: any = {
    frameworks: []
  };
  
  constructor(private authService: AuthService, private router: Router) { }

  addFramework() {
    this.formData.frameworks.push({ Name: '' });
  }
  removeFramework(index: number) {
    this.formData.frameworks.splice(index, 1); // Remove the framework at the given index
  }

  async onSubmit(e: Event) {
    e.preventDefault();
    
    const { email, username, password, frameworks } = this.formData; // Extract frameworks

    this.loading = true;

    // Pass frameworks to the createAccount method
    const result = await this.authService.createAccount(email, username, password, frameworks);
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
@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
    DxTextBoxModule,
    DxButtonModule
  ],

})
export class temp { }
