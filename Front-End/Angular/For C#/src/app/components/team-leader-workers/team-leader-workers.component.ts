import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User, TeamLeaderService, Global } from '../../imports';

@Component({
  selector: 'app-team-leader-workers',
  templateUrl: './team-leader-workers.component.html',
  styleUrls: ['./team-leader-workers.component.css']
})

export class TeamLeaderWorkersComponent implements OnInit {
  
  //----------------PROPERTIRS-------------------

  currentUser: User;
  private workers: User[];

  //----------------CONSTRUCTOR------------------

  constructor(private teamLeaderService: TeamLeaderService, private router: Router) { }

  //----------------METHODS-------------------

  ngOnInit() {
    this.currentUser = JSON.parse(localStorage.getItem(Global.CurrentUser));
    //get the workers whow work in the team leader's projects
    this.teamLeaderService.getWorkersForProject(this.currentUser.Id).subscribe(
      user => {
        this.workers = user;
      })
  }

   openWorkerDeatails(worker: User) {
    this.router.navigate([`taskManagement/teamLeader/WorkerDeatails`,worker.Id]);
  }
  
}
