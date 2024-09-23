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
    this.isEditing = true;
    this.currentField = field; // Track which field is being edited
  }

  disableEdit(): void {
    this.isEditing = false;
    this.currentField = null; // Reset the current field
  }

  saveProfile(): void {
    console.log('Profile saved!', this.user);
    // Implement save functionality to update the profile
    this.disableEdit(); // Exit edit mode after saving
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

  ],
  providers: [DataService],
  declarations: [ProfileComponent],
  exports: [ProfileComponent]
})
export class ProfileModule { }
