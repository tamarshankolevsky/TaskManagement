import { Component, Input } from '@angular/core';
import { DialogService } from 'ng2-bootstrap-modal';
import { EditProjectComponent, ProjectService, Project, Global } from '../../imports';

@Component({
  selector: 'app-project-details',
  templateUrl: './project-details.component.html',
  styleUrls: ['./project-details.component.css']
})

export class ProjectDetailsComponent {

  //----------------PROPERTIES-------------------

  @Input()
  project: any;

   //allow access types via interpolation
   localStorage: Storage = localStorage;
   global: any = Global;
   json = JSON;
   

  //----------------CONSTRUCTOR------------------

  constructor(
    private projectService: ProjectService,
    private dialogService: DialogService
  ) { }

  //-----------------METHODS--------------------

  updateProject(project) {
    this.projectService.getAllProjects().subscribe((projects: Project[]) => {
      let newProject = projects.find(p => p.Id == project.Id);
      this.project.QAHours = newProject.QAHours;
      this.project.DevelopHours = newProject.DevelopHours;
      this.project.UiUxHours = newProject.UiUxHours;
      this.project.EndDate = newProject.EndDate;
      this.project.IsComplete = newProject.IsComplete;
    },
      err => console.log(err));
  }

  editProject(project) {
    this.dialogService.addDialog(EditProjectComponent, {
      title: this.project.Id
    }).subscribe((isConfirmed) => {
      if (isConfirmed)
        this.updateProject(project);
    });
  }

}
