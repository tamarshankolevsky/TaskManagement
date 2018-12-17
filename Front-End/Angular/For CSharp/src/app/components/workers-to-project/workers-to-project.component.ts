import { Component, OnInit } from '@angular/core';
import { ConfirmModel } from '../confirm/confirm.component';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Project, ProjectService, User, UserService, TeamLeaderService } from '../../imports';

@Component({
  selector: 'app-workers-to-project',
  templateUrl: './workers-to-project.component.html',
  styleUrls: ['./workers-to-project.component.css']
})

export class WorkersToProjectComponent extends DialogComponent<ConfirmModel, boolean> implements ConfirmModel, OnInit {

  //----------------PROPERTIES-------------------

  title: string;
  msg: string;
  autoClosing: boolean;
  project: Project;
  workersFormGroup: FormGroup;
  workers: User[];
  workersToAdd: User[] = [];
  workersToSelect: User[];
  workersProject: User[];

  //----------------CONSTRUCTOR------------------

  constructor(
    dialogService: DialogService,
    private formBuilder: FormBuilder,
    private projectService: ProjectService,
    private userService: UserService,
    private teamLeaderService: TeamLeaderService
  ) {
    super(dialogService);
  }

  //----------------METHODS-------------------

  ngOnInit() {

    //get the current project
    this.projectService.getAllProjects().subscribe((projects: Project[]) => {
      this.project = projects.find(p => p.Id == +this.title);

      //get the users of this project
      this.teamLeaderService.getWorkersForProject(this.project.TeamLeaderId).subscribe(
        (users: User[]) => {
          this.workersProject = users;

          //get the users to add the project
          this.userService.getAllUsers().subscribe(
            (users: User[]) => {
              this.workers = users;
              this.initFormGroup();
              this.theWorkersToAdd();
            }
          )
        }
      )
    });
  }

  initFormGroup() {
    this.workersFormGroup = this.formBuilder.group({
      workersToProject: ['']
    });
  }

  /**
 * function
 * add the project and the workers of the project  
 */
  onSubmit() {
    let project: any = this.workersFormGroup.value;
    let i = 0;
    this.workersToAdd = this.workersToProject.value;
    this.addWorkers(project);
  }

  addWorkers(project) {
    if (this.workersToAdd)
      this.projectService.addWorkersToProject(this.workersToAdd, this.project.Name).subscribe(
        (created: boolean) => {
          if (created) {
            this.result = true;
            this.close();
            // this.messageService.add({ key: 'tc', severity: 'success', summary: '', detail: 'The workers added succsesully' });
          }
          else
            this.result = true;
        },
        err => console.log(err));
    else
      this.close();
  }

  /**
   * function
   ** show the workers to add
  */
  theWorkersToAdd() {
    this.workersToSelect = [];
    if (this.workers&&this.workersProject)
      this.workers.forEach(
        w => {
          //is not a team leader
          let worker: User = null;
          worker = this.workersProject.find(wp => wp.Id == w.Id);
          if (w.StatusId > 2 && worker == null)
            this.workersToSelect.push(w);
        });
  }

  //-----------------GETTERS--------------------

  //getters of the form group controls

  get workersToProject() {
    return this.workersFormGroup.controls["workersToProject"];
  }

}
