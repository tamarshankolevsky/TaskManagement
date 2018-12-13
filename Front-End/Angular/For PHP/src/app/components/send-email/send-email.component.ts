import { Component } from '@angular/core';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { WorkerHoursService } from '../../imports';

export interface ConfirmModel {
  title: string;
  message: string;
}

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.css']
})

export class SendEmailComponent extends DialogComponent<ConfirmModel, boolean> implements ConfirmModel {

  //----------------PROPERTIRS-------------------

  title: string;
  message: string;
  workerList: any;

  //----------------CONSTRUCTOR------------------

  constructor(dialogService: DialogService, private workerHoursService: WorkerHoursService) {
    super(dialogService);
  }

  //-----------------METHODS--------------------

  confirm() {
    this.workerHoursService.subStr = this.title;
    this.workerHoursService.bodyStr = this.message;
    this.result = true;
    this.close();
  }

}
