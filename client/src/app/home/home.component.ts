import { Component, OnInit, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DxButtonModule } from 'devextreme-angular';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-home',
  standalone: true,
  imports: [DxButtonModule, CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient);
  users: any;
  questions: any;

  helloWorld() {
    alert('Hello world!');
  }

  ngOnInit(): void {
    this.http.get('http://localhost:7112/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log('Request 200Ok')
    });

    this.http.get('http://localhost:7112/api/questions').subscribe({
      next: response => this.questions = response,
      error: error => console.log(error),
      complete: () => console.log('Request 200Ok')
    });
  }
}
