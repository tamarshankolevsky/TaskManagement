import { Component, Input } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { DialogService } from 'ng2-bootstrap-modal';
import { MessageService } from 'primeng/api';
import {
  User, Project, Estatus, Global,
  UserService, ProjectService,
  DateValidator, numberValidatorArr, stringValidatorArr
} from '../../imports';


@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})

export class AddProjectComponent {

  //----------------PROPERTIRS-------------------

  @Input()
  title: string;

  @Input()
  project: Project;

  projectFormGroup: FormGroup;
  //allow access from html page to 'Object' type
  objectHolder: typeof Object = Object;
  isExistUserName: boolean = false;
  allStatuses: any[];
  allProjects: Project[];
  allManagers: User[];
  user: User;
  users: User[];
  allTeamLeaders: User[] = [];
  totalHours: number;
  workers: User[];
  workersToSelect: User[] = [];
  workersToAdd: User[] = [];

  //----------------CONSTRUCTOR------------------

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private projectService: ProjectService,
    private router: Router,
    private dialogService: DialogService,
    private messageService: MessageService
  ) { }

  //-----------------METHODS--------------------

  ngOnInit() {
    this.initFormGroup();

    //get the users add to the project
    this.userService.getAllUsers().subscribe(
      (users: User[]) => {
        this.workers = users;
        this.getAllTeamLeaders();
      }
    )
  }

  initFormGroup() {
    this.projectFormGroup = this.formBuilder.group({
      name: ['', stringValidatorArr("name", 2, 15, /^[A-Za-z]+$/)],
      customer: ['', stringValidatorArr("customer", 2, 15, /^[A-Za-z]+$/)],
      developHours: ['', numberValidatorArr("developHours", 0)],
      QAHours: ['', numberValidatorArr("QAHours", 0)],
      UIUXHours: ['', numberValidatorArr("UIUXHours", 0)],
      startDate: ['', DateValidator(null)],
      endDate: ['', stringValidatorArr('endDate')],
      teamLeaderId: [''],
      workersToProject: ['']
    });
  }

  getAllTeamLeaders() {
    this.userService.getAllTeamLeaders().subscribe(
      (teamLeaders: User[]) => {
        this.allTeamLeaders = teamLeaders;
      },
      err => console.log(err));
  }

  /**
   * function
   * @param control to edit 
   ** define if the control disable for team leader
   */
  isTeamLeader(control: any) {
    this.user = JSON.parse(localStorage.getItem(Global.CurrentUser));
    return this.user.StatusId == Estatus.TEAMLEADER && control != this.developHours && control != this.QAHours && control != this.UIUXHours;
  }

  /**
   * function
   * add the project and the workers of the project  
   */
  onSubmit() {
    let project: any = this.projectFormGroup.value;
    let i = 0;
    for (i = 0; i < this.workersToProject.value.length; i++) {
      let worker = this.workers.find(worker => this.workersToProject.value[i] == worker.Id);
      this.workersToAdd.push(worker);
    }
    delete project["workersToProject"];
    this.workersToAdd = this.workersToProject.value;
    this.addProject(project);
  }


  /**
   * function
   * add the project 
   */
  addProject(project) {
    this.projectService.addProject(project).subscribe(
      (created: boolean) => {
        if (created) {
          this.messageService.add({ key: 'tc', severity: 'success', summary: '', detail: 'The project added succsesully' });
          //add the workers 
          this.projectService.addWorkersToProject(this.workersToAdd, this.projectFormGroup.value["name"]).subscribe(res => { })
        }
      },
      err => console.log(err));
  }

  /**
   * function
   * show all project after adding it
   */
  navigate() {
    this.router.navigate(['taskManagement/manager/projectManagement/allProjects']);
  }

  /**
   * function
   ** show the workers to add
  */
  theWorkersToAdd() {
    this.workersToSelect = [];
    this.workers.forEach(
      w => {
        //is not a team leader
        if (w.StatusId > 2)
          this.workersToSelect.push(w);
      });
  }


  //-----------------GETTERS--------------------

  //getters of the form group controls

  get name() {
    return this.projectFormGroup.controls["name"];
  }

  get customer() {
    return this.projectFormGroup.controls["customer"];
  }

  get developHours() {
    return this.projectFormGroup.controls["developHours"];
  }

  get QAHours() {
    return this.projectFormGroup.controls["QAHours"];
  }

  get UIUXHours() {
    return this.projectFormGroup.controls["UIUXHours"];
  }

  get startDate() {
    return this.projectFormGroup.controls["startDate"];
  }

  get endDate() {
    return this.projectFormGroup.controls["endDate"];
  }

  get teamLeaderId() {
    return this.projectFormGroup.controls["teamLeaderId"];
  }

  get workersToProject() {
    return this.projectFormGroup.controls["workersToProject"];
  }

}
