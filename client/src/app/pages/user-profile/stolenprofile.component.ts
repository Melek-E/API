import { Component } from '@angular/core';
import { DxFormModule } from 'devextreme-angular/ui/form';


@Component({
  templateUrl: 'stolenprofile.component.html',
  styleUrls: [ './stolenprofile.component.scss' ],
  standalone: true,
  imports: [DxFormModule]
})

export class StolenProfileComponent {
  employee: any;
  colCountByScreen: object;

  constructor() {
    this.employee = {
      ID: 7,
      FirstName: 'Sandra',
      LastName: 'Johnson',
      Prefix: 'Mrs.',
      Position: 'Controller',
      Picture: 'images/employees/06.png',
      BirthDate: new Date('1974/11/5'),
      HireDate: new Date('2005/05/11'),
      /* tslint:disable-next-line:max-line-length */
      Notes: 'Dumb as hell.',
      Address: '4600 N Virginia Rd.',
      NewColum: "HUH"
    };
    this.colCountByScreen = {
      xs: 1,
      sm: 2,
      md: 3,
      lg: 4
    };
  }
}   