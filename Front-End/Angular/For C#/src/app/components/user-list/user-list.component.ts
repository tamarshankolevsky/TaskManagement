import { Component, OnInit } from '@angular/core';
import { UserService, User } from '../../imports';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})

export class UserListComponent implements OnInit {

  //----------------PROPERTIES-------------------

  users: User[] = [];
  isExistUser: boolean = true;

  //----------------CONSTRUCTOR------------------

  constructor(private userService: UserService) { }

  //------------------METHODS---------------------

  ngOnInit() {
    this.getAllUsers();
    //listen to the subject to update all the users
    this.userService.updateUserListSubject.subscribe(
      {
        next: () =>
          this.getAllUsers()
      });
  }

  getAllUsers() {
    this.userService.getAllUsers().subscribe(
      (users: User[]) => {
        this.isExistUser = true;
        this.users = users;
        if (!(this.users.length > 0))
          this.isExistUser = false;
      },
      err => console.log(err));
  }

}
