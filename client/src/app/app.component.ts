  import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DxButtonModule } from 'devextreme-angular';



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, DxButtonModule, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})

export class AppComponent implements OnInit{
  
  title = 'QuizzMaster';
  http = inject(HttpClient);
  users: any;
  questions: any;
  helloWorld() {
    alert('Hello world!');
  }
  ngOnInit(): void {
    this.http.get('https://localhost:7112/api/users').subscribe(
      {
        next: response => this.users = response,
        error: error => console.log(error),
        complete: () => console.log('Request 200Ok')


      }
    ),
      this.http.get('https://localhost:7112/api/questions').subscribe(
      {
        next: response => this.questions= response,
        error: error => console.log(error),
        complete: () => console.log('Request 200Ok')


      }
    )


  }
}
