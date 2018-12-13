import { Component } from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';

export interface ConfirmModel {
  title: string;
  msg: string;
  autoClosing: boolean
}

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})

export class ConfirmComponent extends DialogComponent<ConfirmModel, boolean> implements ConfirmModel {

  //----------------PROPERTIRS-------------------

  //implementaition of 'ConfirmModel' interface
  title: string;
  msg: string;
  autoClosing: boolean = false;

  //----------------CONSTRUCTOR------------------

  constructor(dialogService: DialogService) {
    super(dialogService);
    setTimeout(() => {
      this.close();
    }, 3000);
  }

  //----------------METHODS-------------------

  /**
  *@method
  * set dialog result as true on click on confirm button,  
 * then we can get dialog result from caller code 
  */
  confirm() {
    this.result = true;
    this.close();
  }
  
}
