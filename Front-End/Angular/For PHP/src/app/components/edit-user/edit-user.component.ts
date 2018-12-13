import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from 'ng2-bootstrap-modal';
import { User, UserService, ConfirmComponent } from '../../imports';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})

export class EditUserComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  user: User;
  userName: string;
  title: string;
  sub: any;

  //----------------CONSTRUCTOR------------------

  constructor(private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private dialogService: DialogService,
    private messageService: MessageService) {
    this.title = 'Edit The Emloyee';
  }

  //-----------------METHODS--------------------

  ngOnInit() {
    //get the user to edit
    this.sub = this.activatedRoute.params.subscribe(params => {
      this.userService.getUser(+params['id']).subscribe(
        (user: User) => {
          this.user = user;
        },
        err => console.log(err));
    });
  }

  onSubmit(data: { user: User, imageFile: string }) {
    this.initUser(data.user);
  }

  initUser(user: any) {
    this.user.UserName = user.userName;
    this.user.Name = user.name;
    this.user.EMail = user.email;
    this.user.StatusId = user.statusId;
    this.user.ManagerId = user.managerId;
  }

  uploadImage(data: { user: User, imageFile: string }) {
    this.updateUser();
  }

  /**
 * function
 * update the user  
 */
  updateUser() {
    this.userService.updateUser(this.user).subscribe(
      (created: boolean) => {
        if (created) {
          this.messageService.add({ key: 'tc', severity: 'success', summary: '', detail: `${this.user.Name} updated succsesully` });
          this.userService.updateUserListSubject.next();
        }
      },
      err => console.log(err));
  }

}


