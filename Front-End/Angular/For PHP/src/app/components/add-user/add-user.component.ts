import { Component, } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'ng2-bootstrap-modal';
import { User, UserService, ConfirmComponent } from '../../imports';
import { MessageService } from 'primeng/api';

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

  constructor(private userService: UserService,
     private router: Router, 
     private dialogService: DialogService,
    private messageService:MessageService) {
    this.user = new User(0, '', '', '', 0, '', null, '');
    this.title = 'Add a new emloyee';
  }

  //-----------------METHODS--------------------

  onSubmit(data: { user: User, imageFile: string }) {
    this.user = data.user;
    this.addUser();
  }

  /**
 * function
 * add the user  
 */
  async addUser() {
    this.userService.addUser(this.user).subscribe(
      (created: boolean) => {
        if (created) {
          this.messageService.add({key: 'tc', severity:'success', summary: '', detail:'The User added succsesully'});
          this.userService.updateUserListSubject.next();
        }
      },
      err => console.log(err));
  }

}
