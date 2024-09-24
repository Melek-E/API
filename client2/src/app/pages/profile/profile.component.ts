import { Component, NgModule, OnInit } from '@angular/core';
import { DxFormModule, DxTagBoxModule, DxDataGridModule, DxLoadIndicatorModule, DxButtonGroupModule, DxButtonComponent } from 'devextreme-angular';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DataService } from '../../shared/services/data.service';
import { IUser } from '../../shared/services';
import { DxButtonTypes } from 'devextreme-angular/ui/button';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import {
  DxSelectBoxModule,
  DxCheckBoxModule,
  DxTextBoxModule,
  DxDateBoxModule,
  DxDateRangeBoxModule,
  DxButtonModule,

  DxValidationSummaryModule,
} from 'devextreme-angular';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  userProfile: IUser | null = null; // This will hold the user profile data
  frameworkOptions: string[] = ['Entity Framework', 'React', 'Angular', 'Vue']; // Example frameworks
  previousTests: any[] = []; // Previous test data, can be fetched from the API
  private apiUrl = 'http://localhost:7112/api/auth/profile';
  isEditing: boolean = false; // Flag to control editing state
  currentField: string | null = null;
  isEditingUsername: boolean = false; // Flag for editing Username
  isEditingEmail: boolean = false; // Flag for editing Email
  isEditingFrameworks: boolean = false; // Flag for editing Frameworks


  user = {
    Username: '',
    Email: '',
    Frameworks:[]
  };
  constructor(private dataService: DataService, private http: HttpClient,private router:Router) {}
  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    this.http.get<any>(this.apiUrl, { withCredentials: true }).subscribe({
      next: (data) => {
        // Log data to verify structure
        console.log('Profile data:', data);

        if (data) {
          this.user = {
            Username: data.userName || '',
            Email: data.email || '',
            Frameworks: data.frameworks|| []

            
          };
          console.log("these are the droids you're looking for ",data);
          console.log(data.userName)
        }
      },
      
      error: (error) => {
        console.error('Error loading profile', error);
      }
    });
  }
  enableEdit(field: string): void {
    if (field === 'Username') {
      this.isEditingUsername = true;
    } else if (field === 'Email') {
      this.isEditingEmail = true;
    } else if (field === 'Frameworks') {
      this.isEditingFrameworks = true;
    }
  }

  disableEdit(field: string): void {
    if (field === 'Username') {
      this.isEditingUsername = false;
    } else if (field === 'Email') {
      this.isEditingEmail = false;
    } else if (field === 'Frameworks') {
      this.isEditingFrameworks = false;
    }
  }

  saveProfile(): void {
    this.http.put(this.apiUrl, this.user, { withCredentials: true }).subscribe({
      next: (response) => {
        console.log('Profile updated successfully!', response);
        this.disableEdit('Username');
        this.disableEdit('Email');
        this.disableEdit('Frameworks');
      },
      error: (error) => {
        console.error('Error updating profile', error);
      }
    });

  

} 
}

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    DxFormModule,
    DxLoadIndicatorModule,
    DxTagBoxModule,
    DxButtonModule,
    DxDataGridModule, 
    DxTextBoxModule

  ],
  providers: [DataService],
  declarations: [ProfileComponent],
  exports: [ProfileComponent]
})
export class ProfileModule { }
