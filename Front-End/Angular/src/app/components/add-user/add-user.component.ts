import { Component, } from '@angular/core';
import { MessageService } from 'primeng/api';
import { User, UserService } from '../../imports';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})

export class AddUserComponent {

  //----------------PROPERTIRS-------------------

  user: User;
  title: string;

  //----------------CONSTRUCTOR------------------

  constructor(
    private userService: UserService,
    private messageService: MessageService
  ) {
    this.user = new User(0, '', '', '', 0, '', null, '');
    this.title = 'Add User';
  }

  //-----------------METHODS--------------------

  onSubmit(data: { user: User, imageFile: string }) {
    this.user = data.user;
    this.addUser();
  }

  async addUser() {
    this.userService.addUser(this.user).subscribe(
      (created: boolean) => {
        if (created) {
          this.messageService.add({ key: 'tc', severity: 'success', summary: '', detail: 'The User added succsesully' });
          this.userService.updateUserListSubject.next();
        }
      },
      err => console.log(err));
  }

}
