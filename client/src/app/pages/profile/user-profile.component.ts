import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

import { DxFormModule } from 'devextreme-angular';

@Component({
  selector: 'app-user-profile',
  standalone: true,
  imports: [DxFormModule],
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {
  
  user = {
    Username: '',
    Email: ''
  };
  colCountByScreen: object;

  private apiUrl = 'http://localhost:7112/api/auth/profile';

  constructor(private http: HttpClient,private router:Router) {
    this.colCountByScreen = {
      xs: 1,
      sm: 2,
      md: 3,
      lg: 4
    };
    
  }

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
            Username: data.Username || '',
            Email: data.Email || ''
          };
        }
      },
      
      error: (err) => {
        console.error('Error loading profile:', err);
        this.router.navigate(['/unauthorized']);

      }
    });
  }

  saveProfile() {
    console.log('Profile saved:', this.user);
  }
}
