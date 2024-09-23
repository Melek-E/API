import { Component, NgModule, OnInit } from '@angular/core';
import { DxFormModule, DxTagBoxModule, DxDataGridModule, DxLoadIndicatorModule, DxButtonGroupModule, DxButtonComponent } from 'devextreme-angular';
import { Observable } from 'rxjs';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DataService } from '../../shared/services/data.service';
import { IUser } from '../../shared/services';
import { DxButtonTypes } from 'devextreme-angular/ui/button';

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

  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    // Fetch the profile data on initialization
    this.dataService.getProfile().subscribe(
      (response: IUser[]) => {
        // Assuming the API returns an array, get the first profile
        if (response.length > 0) {
          this.userProfile = response[0]; // Store the first profile
        }
      },
      (error) => {
        console.error('Error fetching profile data', error);
      }
    );
  }

  loadPreviousTests(): void {
    // Mock data, replace with actual API call if needed
    this.previousTests = [
      { testName: 'Math Test', date: new Date('2023-05-18'), score: 85 },
      { testName: 'Physics Test', date: new Date('2023-06-12'), score: 90 }
    ];
  }

  saveProfile(): void {
    console.log('Profile saved!', this.userProfile);
    // Add save functionality here
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
