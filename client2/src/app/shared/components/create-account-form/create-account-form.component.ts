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
    frameworks: [] // Selected frameworks by the user
  };
  availableFrameworks: Framework[] = []; // Available frameworks from the API

  constructor(private authService: AuthService, private dataService: DataService, private router: Router) { }
  addFramework() {
    this.formData.frameworks.push({ Name: '' });
  }
  removeFramework(index: number) {
    this.formData.frameworks.splice(index, 1); // Remove the framework at the given index
  }


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
      notify(result.message, 'error KYS', 2000);
      // console.log(frameworks);
      // console.log("pp");
      // console.log(this.formData.frameworks);


    }
  }

  confirmPassword = (e: ValidationCallbackData) => {
    return e.value === this.formData.password;
  }

  onCustomItemCreating(e: any) {
    const newItem = e.text.trim();
  
    if (newItem && !this.availableFrameworks.some(f => f.Name === newItem)) {
      // Wrap the new framework in an array since the backend expects an array
      const newFrameworkArray = [{ Name: newItem }];
  
      // Log the framework array being sent for debugging
      // console.log('Sending framework to backend as array:', newFrameworkArray);
  
      // Send the POST request to save the new framework to the backend
      this.dataService.createFramework(newFrameworkArray).subscribe(
        (createdFramework: Framework[]) => {
          // console.log('Framework successfully added:', createdFramework[0]);
  
          // Add the newly created framework to the availableFrameworks list
          this.availableFrameworks.push(createdFramework[0]);
  
          // Add the newly created framework's name to the formData.frameworks list (for selection)
          this.formData.frameworks.push(createdFramework[0].Name);
  
          // Notify the user about the success
          notify(`Framework '${createdFramework[0].Name}' added successfully.`, 'success', 2000);
        },
        (error) => {
          // Log and notify the user in case of failure
          console.error('Error adding new framework:', error);
          notify('Failed to add new framework.', 'error', 2000);
        }
      );
  
      // Inform dxTagBox that the new custom item is being added
      e.customItem = newItem;
    } else {
      e.customItem = null; // Prevent duplicates
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
