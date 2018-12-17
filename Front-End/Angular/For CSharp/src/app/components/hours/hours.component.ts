import { Component, Input } from '@angular/core';
import { formatDate } from '@angular/common';
import { HoursService, WorkerHoursService, Global } from '../../imports';

@Component({
  selector: 'app-hours',
  templateUrl: './hours.component.html',
  styleUrls: ['./hours.component.css']
})

export class HoursComponent {

  //----------------PROPERTIES-------------------

  //allow access types via interpolation
  localStorage: Storage = localStorage;
  global: any = Global;
  json = JSON;
  @Input()
  private hour: any;
  @Input()
  private isProject: boolean = false;
  @Input()
  private list: any;
  @Input()
  private isEdit: boolean;
  currectProject: any;
  isStart: boolean = true;
  t: any;
  timer;
  time: any;
  startTask: any;
  endTask: any;
  sumTime: any;
  timerSeconds: number = 0;
  timerMinutes: number = 0;
  timerHours: number = 0;
  zeroFlagS: boolean = true;
  zeroFlagM: boolean = true;
  zeroFlagH: boolean = true;
  timerInterval: any = null;
  projects: any;
  enableStop: boolean = false;

  //----------------CONSTRUCTOR------------------

  constructor(private workerHoursService: WorkerHoursService, private hoursService: HoursService) { }

  //------------------METHODS---------------------

  StartTimer() {
    this.enableStop = true;
    this.timerHours = 0;
    this.timerMinutes = 0;
    this.timerSeconds = 0;

    this.zeroFlagS = true;
    this.zeroFlagM = true;
    this.zeroFlagH = true;
    //the timer
    if (this.timerInterval == null) {
      this.timerInterval = setInterval(() => {
        if (this.timerSeconds > 8)
          this.zeroFlagS = false;
        if (this.timerMinutes > 9)
          this.zeroFlagM = false;
        if (this.timerHours > 9)
          this.zeroFlagH = false;
        else
          this.zeroFlagH = true;

        if (this.timerSeconds == 59) {
          this.timerMinutes++;
          this.timerSeconds = 0;
          this.zeroFlagS = true;
        }
        if (this.timerMinutes == 59) {
          this.timerHours++;
          this.timerMinutes = 0;
          this.zeroFlagM = true;
        }
        this.timerSeconds++;
      }, 1000);
    }

    this.currectProject = this.hour;
    this.hoursService.projectStart = this.hour;
    this.startTask = formatDate(new Date(), 'yyyy/MM/dd hh:mm:ss', 'en');
    //update the start time
    this.workerHoursService.updateStartHour(this.startTask, this.currectProject.Id, true).subscribe(
      res => {
        this.isStart = !this.isStart;
      })
  }

  StopTimer() {
    this.enableStop = false;
    if (this.timerInterval) {
      clearInterval(this.timerInterval);
      this.time = this.timerHours + this.timerMinutes / 60;
      this.endTask = formatDate(new Date(), 'yyyy/MM/dd hh:mm:ss', 'en');
      //update the end time
      this.workerHoursService.updateStartHour(this.endTask, this.currectProject.Id, false).subscribe(
        res => {
          //get the updated details 
          this.workerHoursService.getProject(JSON.parse(localStorage.getItem(Global.CurrentUser)).Id).subscribe(projects => {
            this.projects = projects;
            this.hoursService.projectStart = null;
            this.hour["Hours"] = this.projects.find(p => p.Id == this.currectProject.Id).Hours;
            this.hoursService.subjectUpdateChart.next(this.projects);
          });
        }
      )
      this.timerInterval = null;
    }
  }
  
}
