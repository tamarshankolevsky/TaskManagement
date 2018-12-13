import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { User, HoursService, WorkerHours, numberValidatorArr } from '../../imports';

@Component({
  selector: 'app-worker-hours',
  templateUrl: './worker-hours.component.html',
  styleUrls: ['./worker-hours.component.css']
})

export class WorkerHoursComponent implements OnInit {

  //----------------PROPERTIRS-------------------
  
  workerFormGroup: FormGroup;
  //allow access from html page to 'Object' type
  objectHolder: typeof Object = Object;

  @Input()
  workerHours: WorkerHours;

  //----------------CONSTRUCTOR------------------

  constructor(
    private router: Router,
    private hoursService: HoursService,
    private formBuilder: FormBuilder
  ) {  }

  //-----------------METHODS--------------------

  ngOnInit(): void {
    this.initFormGroup();
  }

  initFormGroup() {
    this.workerFormGroup = this.formBuilder.group({
      allocatedHours: [this.workerHours.AllocatedHours,numberValidatorArr("allocatedHours", 0)],//todo-max-project hours
      hours: [this.workerHours.Hours, numberValidatorArr("hours", 0, this.workerHours.AllocatedHours)]
    });
  }

  /**
   * function - 
   * get the hours of the worker for each project
   */
  getWorkerHours(worker: User) {
    this.hoursService.getWorkerHours(worker.Id).subscribe(
      (workerHours: WorkerHours) => {
        this.workerHours = workerHours;
      },
      err => console.log(err));
  }

  onSubmit() {
    //update the hours 
    this.hoursService.updaterWorkerHours(this.workerHours.Id).subscribe(
      (workerHours: WorkerHours) => {
        this.workerHours = workerHours;
      },
      err => console.log(err));
  }
   //----------------GETTERS-------------------

  //getters of the form group controls

  get allocatedHours() {
    return this.workerFormGroup.controls["allocatedHours"];
  }

  get hours() {
    return this.workerFormGroup.controls["hours"];
  }

}
