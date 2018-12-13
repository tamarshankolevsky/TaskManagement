import { Component } from '@angular/core';
import { Project, User, Estatus } from '../../imports';
import { ProjectService } from '../../shared/services/project.service';
import { Global } from '../../shared/global';

@Component({
  selector: 'app-all-projects',
  templateUrl: './all-projects.component.html',
  styleUrls: ['./all-projects.component.css']
})

export class AllProjectsComponent {

  //----------------PROPERTIRS-------------------

  user: User;
  projects: Project[] = [];
  isExistProject: boolean = true;

  //----------------CONSTRUCTOR------------------

  constructor(private projectService: ProjectService) { }

  //----------------METHODS-------------------

  ngOnInit() {

    this.user = JSON.parse(localStorage.getItem(Global.CurrentUser));

    //get All Projects
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
}
