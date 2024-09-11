import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DxTextBoxModule, DxButtonModule, DxFormModule } from 'devextreme-angular';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [DxTextBoxModule, DxButtonModule, DxFormModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  user = {
    username: '',
    email: ''
  };
  
  private apiUrl = 'http://localhost:7112/api/auth/profile';

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadProfile();
  }

  loadProfile() {
    this.http.get<any>(this.apiUrl).subscribe({
      next: (data) => {
        this.user = data;
      },
      error: (err) => {
        console.error('Error loading profile:', err);
      }
    });
  }

  saveProfile() {
    console.log('Profile saved:', this.user);
    // Add logic to save profile data if needed
  }
}
