import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'ng2-bootstrap-modal';
import { WorkerHoursService, SendEmailComponent, ConfirmComponent, User, Global } from '../../imports';
import {MessageService} from 'primeng/api';


@Component({
  selector: 'app-worker-tasks',
  templateUrl: './worker-tasks.component.html',
  styleUrls: ['./worker-tasks.component.css']
})

export class WorkerTasksComponent implements OnInit {

  //----------------PROPERTIES-------------------

  today: any;
  projects: any[];
  currentWorker: User;

  //----------------CONSTRUCTOR------------------

  constructor(private workerHoursService: WorkerHoursService,
     private router: Router,
      private dialogService: DialogService,
    private messageService:MessageService) { }

  //-----------------METHODS--------------------

  ngOnInit() {
    this.today = Date.now();
    this.currentWorker = JSON.parse(localStorage.getItem(Global.CurrentUser));
    this.workerHoursService.getProject(this.currentWorker.Id).subscribe(projects => {
      this.projects = projects;
    });
  }

  //   /**
  //    * function
  //    * send the email
  //    */
  send() {
    this.dialogService.addDialog(SendEmailComponent, {
    }).subscribe((isConfirmed) => {
      if (isConfirmed) {
        this.workerHoursService.sendMsg().subscribe(res => {
          if (res=="yes"){
            this.messageService.add({key: 'tc',severity:'success', summary: '', detail:'The email sent successfuly'});
          }
          
          else{
            this.messageService.add({key: 'tc',severity:'error', summary: '', detail:'faild sending, try again'}); 
          }
            
        })
      }
    });
  }

}

