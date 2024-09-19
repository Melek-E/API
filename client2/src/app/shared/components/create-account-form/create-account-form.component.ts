import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { ValidationCallbackData } from 'devextreme-angular/common';
import { DxFormModule } from 'devextreme-angular/ui/form';
import { DxLoadIndicatorModule } from 'devextreme-angular/ui/load-indicator';
import { DxTagBoxModule } from 'devextreme-angular/ui/tag-box';
import notify from 'devextreme/ui/notify';
import { AuthService } from '../../services';
import { DataService } from '../../services/data.service'; // Import the DataService
import { Observable } from 'rxjs';

// Define the structure for Frameworks
type Framework= {
  Id: number,

  Name: string;
}

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

  // Load frameworks on component initialization
  ngOnInit() {
    this.loadFrameworks();
  }

  // Fetch available frameworks from the API using the DataService
  loadFrameworks() {
    this.dataService.getFrameworks().subscribe((frameworks: Framework[]) => {
      this.availableFrameworks = frameworks;
    }, error => {
      notify('Failed to load frameworks', 'error', 2000);
    });
  }

  // Method to handle form submission
  async onSubmit(e: Event) {
    e.preventDefault();

    const { email, username, password, frameworks } = this.formData;

    this.loading = true;

    // Pass form data to the AuthService for registration
    const result = await this.authService.createAccount(email, username, password, frameworks);
    this.loading = false;

    if (result.isOk) {
      // Navigate to login form on success
      this.router.navigate(['/login-form']);
    } else {
      // Display error notification if account creation failed
      notify(result.message, 'error', 2000);
    }
  }

  // Method to confirm password matches
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
    DxTagBoxModule // Import DxTagBoxModule for multi-select dropdown
  ],
  providers:[DataService],
  declarations: [CreateAccountFormComponent],
  exports: [CreateAccountFormComponent]
})
export class CreateAccountFormModule { }
