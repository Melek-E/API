import { Component } from '@angular/core';
import { DxButtonModule } from 'devextreme-angular/ui/button';



@Component({
  selector: 'app-dev-extreme-test',
  standalone: true,
  imports: [DxButtonModule],
  templateUrl: './dev-extreme-test.component.html',
  styleUrl: './dev-extreme-test.component.css'
})
export class DevExtremeTestComponent {
  helloWorld() {
    alert('Hello world!');

  }
}
