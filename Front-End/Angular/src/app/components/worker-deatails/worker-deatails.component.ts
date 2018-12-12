import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';

import {
  Global, User, Project,
  TeamLeaderService, UserService, ProjectService
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
  status: any;
  ProjectName: string;
  theProject: Project;

  //----------------CONSTRUCTOR------------------

  constructor(
    private teamLeaderService: TeamLeaderService,
    private userService: UserService,
    private projectService: ProjectService,
    private messageService: MessageService
  ) { }

  //------------------METHODS--------------------

  ngOnInit() {
    this.currentWorker = JSON.parse(localStorage.getItem(Global.CurrentUser));
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

