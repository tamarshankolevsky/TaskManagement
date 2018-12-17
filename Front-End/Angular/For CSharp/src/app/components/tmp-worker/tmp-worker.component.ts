import { Component, OnInit, Input } from '@angular/core';
import { Router } from '@angular/router';
import { User, UserService } from '../../imports';

@Component({
  selector: 'app-tmp-worker',
  templateUrl: './tmp-worker.component.html',
  styleUrls: ['./tmp-worker.component.css']
})

export class TmpWorkerComponent implements OnInit {

  //----------------PROPERTIES-------------------

  @Input()
  worker: User;
  statusList: any[] = [];
  status: string;

  //----------------CONSTRUCTOR------------------

  constructor(private userService: UserService, private router: Router) { }

  //-----------------METHODS--------------------

  ngOnInit() {
    this.userService.getAllStatuses().subscribe(
      res => {
        this.statusList = res;
        this.status = this.statusList.find(p => p.Id == this.worker.StatusId).Name;
      });
  }

  openWorkerDeatails(worker:any) {
    this.router.navigate([`taskManagement/teamLeader/WorkerDeatails`, worker.Id]);
  }
}
