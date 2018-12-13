import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {MessageService} from 'primeng/api';
import {ToastModule} from 'primeng/toast';

import {
  Global, User, Project,
  TeamLeaderService, UserService, ProjectService, WorkerHoursService
} from '../../imports';

@Component({
  selector: 'app-worker-deatails',
  templateUrl: './worker-deatails.component.html',
  styleUrls: ['./worker-deatails.component.css'],
  providers: [MessageService]
})

export class WorkerDeatailsComponent implements OnInit {

  //----------------PROPERTIES-------------------

  private projectsHours: any[];
  private worker: User;
  private currentWorker: User;
  private numHours: number = 0;
  private projectId: number;
  private sub: any;
  private isEdit: boolean = true;
  status: any;
  ProjectName: string;
  theProject: Project;

  //----------------CONSTRUCTOR------------------

  constructor(
    private route: ActivatedRoute,
    private teamLeaderService: TeamLeaderService,
    private userService: UserService,
    private projectService: ProjectService,
    private workerhoursService: WorkerHoursService,
    private messageService:MessageService
  ) { }

  //------------------METHODS--------------------

  ngOnInit() {
    this.currentWorker = JSON.parse(localStorage.getItem(Global.CurrentUser));

    //get the current worker
    this.sub = this.route.params.subscribe(params => {
      this.userService.getUser(+params['id']).subscribe(
        (user: User) => {
          this.worker = user;
          this.teamLeaderService.getProjectsHours(this.currentWorker.Id, this.worker.Id).subscribe(
            res => {
              this.projectsHours = res;
            });
        },
        err => console.log(err));
    });
  }

  changeHours1() {
    this.projectService.getAllProjects().subscribe((p: Project[]) => {
      let pList = p;
      this.theProject = pList.find(p => p.Name == this.ProjectName);
     this.userService.getRemainingHours(this.projectId,this.worker.StatusId).subscribe(
        res=>
        {
          console.log(res);
          if (+this.numHours<= res)
          {
            this.teamLeaderService.setAlloactedHours(this.numHours, this.projectId).subscribe(
              res => {
                this.projectsHours.find(p => p.Id == this.projectId).allocatedHours = this.numHours;
                this.projectId = null;
              })
          }
          else
          {
            this.messageService.add({key: 'tc', severity:'warn', summary: 'Warn Message', detail:`the value must be maximum ${res}`})
          }
        }
     )
    })
  }
  changeHours() {
    //check if the value is valid:
    //get the status-the department
    this.userService.getAllStatuses().subscribe(
      res => {
        let statusList = res;
        this.status = statusList.find(p => p.Id == this.worker.StatusId).Name;

        //get the hours for this department
        this.projectService.getAllProjects().subscribe((p: Project[]) => {
          let pList = p;
          this.theProject = pList.find(p => p.Name == this.ProjectName);

          //check current department
          let hoursDepartment = 0;
          if (this.status == "QA") {
            hoursDepartment = this.theProject.QAHours;
          }
          else if (this.status == "UIUX") {
            hoursDepartment = this.theProject.UiUxHours;
          }
          else
            hoursDepartment = this.theProject.DevelopHours;

          //integrate the allocated hours of all the workers in that project
          let allHours = 0;
          this.workerhoursService.getWorkersHours(this.theProject.Id).subscribe(
            res => {
              let workersHours = res;
              workersHours.forEach(hours => {
                allHours += hours.AllocatedHours;
              });
              let thisUserHours = this.projectsHours.find(p => p.Id == this.projectId).allocatedHours;
              if ((allHours - thisUserHours) + +this.numHours<= hoursDepartment)
              {
                this.teamLeaderService.setAlloactedHours(this.numHours, this.projectId).subscribe(
                  res => {
                    this.projectsHours.find(p => p.Id == this.projectId).allocatedHours = this.numHours;
                    this.projectId = null;
                  })
              }
              //if the value not valid
              else {
                let max = hoursDepartment - allHours;
                this.messageService.add({key: 'tc', severity:'warn', summary: 'Warn Message', detail:`the value must be less!`});
              }

            })
        })
      })

  }

  setAllocatedHours(projectId: number, ProjectName: string) {
    this.ProjectName = ProjectName;
    this.projectId = projectId;
  }

}

