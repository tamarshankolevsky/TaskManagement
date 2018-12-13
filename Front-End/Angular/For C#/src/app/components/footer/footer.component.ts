import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {

  //----------------PROPERTIRS-------------------

  currentYear: number;
  
  //----------------CONSTRUCTOR------------------

  constructor() {
    this.currentYear = new Date().getFullYear();
  }

}
