import { Component, OnInit, NgModule } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ValidationCallbackData } from 'devextreme-angular/common';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import { DxTagBoxModule } from 'devextreme-angular/ui/tag-box';
import notify from 'devextreme/ui/notify';
import { AuthService } from '../../services';
import { DataService } from '../../services/data.service';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Framework } from '../../../types/Framework';



@Component({
  selector: 'app-create-account-form',
  templateUrl: './create-account-form.component.html',
  styleUrls: ['./create-account-form.component.scss']
})
export class CreateAccountFormComponent implements OnInit {
  loading = false;
  formData: any = {
    email: '',
    username: '',
    password: '',
    confirmedPassword: '',
    frameworks: [] // Selected frameworks by the user
  };
  availableFrameworks: Framework[] = []; // Available frameworks from the API

  constructor(private authService: AuthService, private dataService: DataService, private router: Router) { }

  ngOnInit() {
    this.loadFrameworks();
  }

  loadFrameworks() {
    this.dataService.getFrameworks().subscribe(
      (frameworks: Framework[]) => {
        console.log('Loaded frameworks:', frameworks); // Log data for debugging
        this.availableFrameworks = frameworks;
      },
      error => {
        notify('Failed to load frameworks', 'error', 2000);
      }
    );
  }

  async onSubmit(e: Event) {
    e.preventDefault();

    const { email, username, password, frameworks } = this.formData;

    this.loading = true;

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

  onCustomItemCreating(e: any) {
    const newItem = e.text.trim();
    if (newItem && !this.availableFrameworks.some(f => f.Name === newItem)) {
      const newFramework = { Id: this.availableFrameworks.length + 1, Name: newItem };
      this.availableFrameworks.push(newFramework);
      this.formData.frameworks.push(newFramework.Name);
    }
  }
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
    DxTagBoxModule // Import DxTagBoxModule for multi-select dropdown
  ],
  providers: [DataService],
  declarations: [CreateAccountFormComponent],
  exports: [CreateAccountFormComponent]
})
export class CreateAccountFormModule { }
