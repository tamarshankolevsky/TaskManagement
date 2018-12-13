import { Component, OnInit } from '@angular/core';
import { 
  User, Project, Estatus, Global, 
  ProjectService, WorkerHoursService, HoursService 
} from '../../imports';

@Component({
  selector: 'app-hours-chart',
  templateUrl: './hours-chart.component.html',
  styleUrls: ['./hours-chart.component.css']
})

export class HoursChartComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  user: User;
  projects: Project[] = [];
  isExistProject: boolean = true;
  currentProject: Project = null;
  workersHours: any;

  //----------------CONSTRACTOR-------------------

  constructor(
    private projectService: ProjectService,
    private workerhoursService: WorkerHoursService,
    private hoursService: HoursService
  ) { }

  //-----------------METHODS--------------------

  ngOnInit() {

    this.user = JSON.parse(localStorage.getItem(Global.CurrentUser));

    //get all projects
    this.projectService.getAllProjects().subscribe(
      (projects: Project[]) => {
        this.isExistProject = true;
        this.projects = projects;

        //if the user is team leader- show just his projects
        if (this.user.StatusId == Estatus.TEAMLEADER && this.projects.length > 0)
          this.projects = this.projects.filter(p => p.TeamLeaderId == this.user.Id);

        if (!(this.projects.length > 0))
          this.isExistProject = false;
      },
      err => console.log(err));
  }

  //show the chart of selected project
  showChart(choosenProject) {
    this.currentProject = choosenProject;
    this.workerhoursService.getWorkersHours(this.currentProject.Id).subscribe(
      res => {
        this.workersHours = res;
        this.hoursService.subjectUpdateChart.next(this.workersHours);
      })
  }

}
