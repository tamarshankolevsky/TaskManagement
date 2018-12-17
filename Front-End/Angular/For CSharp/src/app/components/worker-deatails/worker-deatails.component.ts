import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

import {
  Global, User, Project,
  TeamLeaderService, UserService, ProjectService, WorkerHoursService
} from '../../imports';
import { ActivatedRoute } from '@angular/router';

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
    private messageService:MessageService
  ) { }

  //------------------METHODS--------------------

  ngOnInit() {
    this.currentWorker = JSON.parse(localStorage.getItem(Global.CurrentUser));

     //get the current worker
     this.sub = this.route.params.subscribe(params => {
      this.userService.getUser(+params['id']).subscribe(
        (user: User) => {
          this.worker = user[0];
          this.teamLeaderService.getProjectsHours(this.currentWorker.Id, this.worker.Id).subscribe(
            res => {
              this.projectsHours = res;
            });
        },
        err => console.log(err));
    });
  }

  changeHours() {
    //get the current project
    this.projectService.getAllProjects().subscribe((p: Project[]) => {
      let pList = p;
      this.theProject = pList.find(p => p.Name == this.ProjectName);
      //get the maximus hours to define
      this.userService.getRemainingHours(this.projectId, this.worker.StatusId).subscribe(
        res => {
          if (+this.numHours <= res) {
            this.teamLeaderService.setAlloactedHours(this.numHours, this.projectId).subscribe(
              () => {
                this.projectsHours.find(p => p.Id == this.projectId).allocatedHours = this.numHours;
                this.projectId = null;
              });
          }
          else {
            this.messageService.add({ key: 'tc', severity: 'warn', summary: 'Warn Message', detail: `the value must be maximum ${res}` })
          }
        }
      )
    })
  }

  setAllocatedHours(projectId: number, ProjectName: string) {
    this.ProjectName = ProjectName;
    this.projectId = projectId;
  }

}

