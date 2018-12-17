import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { DialogComponent, DialogService } from 'ng2-bootstrap-modal';
import { MessageService } from 'primeng/api';
import { ProjectService, numberValidatorArr, stringValidatorArr, Project } from '../../imports';

export interface ConfirmModel {
  title: string;
  message: string;
}

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styleUrls: ['./edit-project.component.css']
})

export class EditProjectComponent extends DialogComponent<ConfirmModel, boolean> implements ConfirmModel, OnInit {

  //----------------PROPERTIES-------------------

  project: Project;
  projectFormGroup: FormGroup;
  //allow access from html page to 'Object' type
  objectHolder: typeof Object = Object;
  title: string;
  message: string;

  //----------------CONSTRUCTOR------------------

  constructor(
    dialogService: DialogService,
    private formBuilder: FormBuilder,
    private projectService: ProjectService,
    private messageService: MessageService
  ) {
    super(dialogService);
  }

  //------------------METHODS---------------------

  ngOnInit() {
    //get the current project
    this.projectService.getAllProjects().subscribe((projects: Project[]) => {
      this.project = projects.find(p => p.Id == +this.title);
      this.initFormGroup();
    })
  }

  initFormGroup() {
    this.projectFormGroup = this.formBuilder.group({
      developHours: [this.project.DevelopHours, numberValidatorArr("developHours", 0)],
      QAHours: [this.project.QAHours, numberValidatorArr("QAHours", 0)],
      UIUXHours: [this.project.UiUxHours, numberValidatorArr("UIUXHours", 0)],
      endDate: ['', stringValidatorArr('endDate')],
      isComplete: [this.project.IsComplete]
    });
  }

  editProject() {
    this.result = true;
    let isComplete = this.isComplete.value == true ? true : false;
    this.projectService.editProject(this.project.Id, this.developHours.value, this.QAHours.value, this.UIUXHours.value, this.endDate.value, isComplete).subscribe(
      (isUpdated: boolean) => {
        if (isUpdated) {
          this.project.DevelopHours = this.developHours.value;
          this.project.QAHours = this.QAHours.value;
          this.project.UiUxHours = this.UIUXHours.value;
          this.project.IsComplete = this.isComplete.value;
          this.messageService.add({ key: 'tc', severity: 'success', summary: '', detail: 'The project updated succsesully' });
        }
        this.close();
      });
  }

  //-----------------GETTERS--------------------

  //getters of the form group controls

  get developHours() {
    return this.projectFormGroup.controls["developHours"];
  }

  get QAHours() {
    return this.projectFormGroup.controls["QAHours"];
  }

  get UIUXHours() {
    return this.projectFormGroup.controls["UIUXHours"];
  }

  get endDate() {
    return this.projectFormGroup.controls["endDate"];
  }

  get isComplete() {
    return this.projectFormGroup.controls["isComplete"];
  }

}
