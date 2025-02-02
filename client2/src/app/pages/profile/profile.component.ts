import { Component, NgModule, OnInit } from '@angular/core';
import { DxFormModule, DxTagBoxModule, DxDataGridModule, DxLoadIndicatorModule, DxButtonGroupModule, DxButtonComponent, DxValidatorModule, DxValidatorComponent } from 'devextreme-angular';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DataService } from '../../shared/services/data.service';
import { AuthService, IUser } from '../../shared/services';
import { DxButtonTypes } from 'devextreme-angular/ui/button';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import notify from 'devextreme/ui/notify';

import {
  DxSelectBoxModule,
  DxCheckBoxModule,
  DxTextBoxModule,
  DxDateBoxModule,
  DxDateRangeBoxModule,
  DxButtonModule,
  

  DxValidationSummaryModule,
} from 'devextreme-angular';
import { Framework } from '../../types/Framework';
import { SignalRService } from '../../shared/services/signalr.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  notificationMessage: string = '';
  formData: any = {
    frameworks: [] // Selected frameworks by the user
  };
  userProfile: IUser | null = null; 
  // frameworkOptions: string[] = [];   // Example frameworks
  previousTests: any[] = [];
  private apiUrl = 'http://localhost:7112/api/auth/profile';
  isEditing: boolean = false;
  currentField: string | null = null;
  isEditingUsername: boolean = false;
  isEditingEmail: boolean = false; 
  isEditingFrameworks: boolean = false; 
  availableFrameworks: Framework[] = []; 


  user = {
    Username: '',
    Email: '',
    Frameworks:[]
  };
  constructor(private dataService: DataService, private http: HttpClient,private router:Router, private authService: AuthService, public signal: SignalRService  ) {}
  ngOnInit() {
    this.loadProfile();
    this.loadFrameworks();

    this.signal.startConnection();

     this.signal.addReceiveTestNotificationListener((message: string) => {
       this.notificationMessage = message;
       console.log(message); 
       alert(message);  
     });
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


  loadProfile() {
    this.http.get<any>(this.apiUrl, { withCredentials: true }).subscribe({
      next: (data) => {
        // Log data to verify structure
        console.log('Profile data:', data);

        if (data) {
          this.user = {
            Username: data.userName || '',
            Email: data.email || '',
            Frameworks: data.frameworks

            
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

    const updatedUserProfile = {
      Username: this.user.Username,   // Username should be sent regardless of editing
      Email: this.user.Email,         // Email should be sent regardless of editing
      Frameworks: this.user.Frameworks.map(f => ({ name: f })) // Convert array of strings into array of objects
    };

    console.log('Sending updated profile:', updatedUserProfile);

    this.http.put(this.apiUrl, updatedUserProfile, { withCredentials: true }).subscribe({
      next: async (response) => {
        console.log('Profile updated successfully!', response);
        await this.authService.getUser();

        this.disableEdit('Username');
        this.disableEdit('Email');
        this.disableEdit('Frameworks');
      },
      error: (error) => {
        console.error('Error updating profile', error);
        console.error('Error updating profile', this.user);
        console.error('Error updating profile', this.user);



      }
    });
    
  

} 


onCustomItemCreating(e: any) {
  const newItem = e.text.trim();

  if (newItem && !this.availableFrameworks.some(f => f.Name === newItem)) {


    const newFrameworkArray = [{ Name: newItem }];

  
    this.dataService.createFramework(newFrameworkArray).subscribe(
      (createdFramework: Framework[]) => {
        // console.log('Framework successfully added:', createdFramework[0]);

      
        this.availableFrameworks.push(createdFramework[0]);

        this.formData.frameworks.push(createdFramework[0].Name);

        notify(`Framework added successfully.`, 'success', 2000); //'${createdFramework[0]}'
      },
      (error) => {


        console.error('Error adding new framework:', error);
        notify('Failed to add new framework.', 'error', 2000);
      }
    );

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
    DxTagBoxModule,
    DxButtonModule,
    DxDataGridModule, 
    DxTextBoxModule,
    DxValidatorModule,
    DxValidatorModule


  ],
  providers: [DataService],
  declarations: [ProfileComponent],
  exports: [ProfileComponent]
})
export class ProfileModule { }
//kill yourself melek <3
