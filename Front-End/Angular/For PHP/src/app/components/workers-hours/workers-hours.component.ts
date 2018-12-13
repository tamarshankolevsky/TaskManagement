import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HoursService, WorkerHours } from '../../imports';


@Component({
  selector: 'app-workers-hours',
  templateUrl: './workers-hours.component.html',
  styleUrls: ['./workers-hours.component.css']
})

export class WorkersHoursComponent {

  //----------------PROPERTIRS-------------------

  workersHours: WorkerHours[]=[];

  //----------------CONSTRUCTOR------------------

  constructor( private router: Router, private hoursService: HoursService) { }

  //----------------METHODS-------------------

  /**
   * function - 
   * get the hours of the workers for each project
   */
  getWorkersHours(){
    this.hoursService.getWorkersHours().subscribe(
      (workersHours:WorkerHours[]) => {
        this.workersHours =workersHours ;
      },
      err => console.log(err));
  }
}
