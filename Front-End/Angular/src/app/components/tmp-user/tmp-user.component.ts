import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'ng2-bootstrap-modal';
import { MessageService } from 'primeng/api';
import { User, Global, ConfirmComponent, UserService, Estatus, Status } from '../../imports';

@Component({
  selector: 'app-tmp-user',
  templateUrl: './tmp-user.component.html',
  styleUrls: ['./tmp-user.component.css']
})

export class TmpUserComponent {

  //----------------PROPERTIES-------------------

  @Input()
  user: User;
  //allow access types via interpolation
  global: any = Global;
  isManager: boolean;
  user_: User;
  status: string;
  statusList: Status[];

  //----------------CONSTRUCTOR------------------

  constructor(
    private router: Router,
    private dialogService: DialogService,
    private userService: UserService,
    private messageService: MessageService
  ) {
    this.user_ = JSON.parse(localStorage.getItem(Global.CurrentUser));
    this.isManager = this.user_.StatusId == Estatus.MANAGER;
    //display the user status 
    this.userService.getAllStatuses().subscribe(
      res => {
        this.statusList = res;
        this.status = this.statusList.find(p => p.Id == this.user.StatusId).Name;
      });
  }

  //-----------------METHODS--------------------

  //before delete
  showConfirm() {
    {
      this.dialogService.addDialog(ConfirmComponent, {
        title: 'Delete Worker',
        msg: 'Are you sure you want to delete this worker?',
      })
        .subscribe((isConfirmed: boolean) => {
          if (isConfirmed) {
            //if the user is teamleader-enable if he has no workers
            if (this.user.StatusId == Estatus.TEAMLEADER) {
              this.userService.getTeamLedaWorkers(this.user.Id).subscribe(
                res => {
                  if (res)
                    this.messageService.add({ key: "tc", severity: "error", summary: "", detail: "can't delete teamleader if he has users" });
                  else {
                    this.deleteUser();
                  }
                }
              )
            }
            else {
              this.deleteUser();
            }
          }
        });
    }
  }

  deleteUser() {
    this.userService.deleteUser(this.user.Id).subscribe(
      (deleted: boolean) => {
        if (deleted) {
          this.messageService.add({ key: "tc", severity: "success", summary: "", detail: "The user deleted succsesully" });
          this.userService.updateUserListSubject.next();
        }
      },
      err => console.log(err));
  }

  //---------------navigation-----------

  edit() {
    this.router.navigate(['/taskManagement/manager/userManagement/editUser', this.user.Id]);
  }

  workerHours() {
    this.router.navigate(['/taskManagement/workerHours', this.user.Id]);
  }

}
