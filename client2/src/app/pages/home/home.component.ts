import { Component, OnInit } from '@angular/core';
import { DataService } from '../../shared/services/data.service';
import { Framework } from '../../types/Framework';

@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['./home.component.scss'],



  
  providers: [DataService]
})
export class HomeComponent implements OnInit {
  constructor(private dataService: DataService) {}

  ngOnInit(): void {
    // Automatically call test method on component initialization
    this.testGetFrameworks();
  }

  testGetFrameworks(): void {
    this.dataService.getFrameworks().subscribe(
      (frameworks: Framework[]) => {
        console.log('Loaded frameworks:', frameworks); // Log the frameworks
      },
      (error: any) => {
        console.error('Error loading frameworks:', error); // Log any error
      }
    );
  }
}
