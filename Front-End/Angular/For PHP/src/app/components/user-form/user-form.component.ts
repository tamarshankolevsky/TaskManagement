import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DialogService } from 'ng2-bootstrap-modal';
import * as  sha256 from 'async-sha256';
import {
  User, Project, Status, Global,
  UserService, ProjectService,
  stringValidatorArr, confirmPasswordValidator
} from '../../imports';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})

export class UserFormComponent implements OnInit {

  //----------------PROPERTIES-------------------

  userFormGroup: FormGroup;
  //allow access from html page to 'Object' type
  objectHolder: typeof Object = Object;
  isExistUserName: boolean = false;
  allStatuses: Status[] = [];
  allProjects: Project[] = [];
  allTeamLeaders: User[] = [];
  showPassword: boolean = false;
  imageFile: any;
  imageUrl: string;
  pass: string;
  validatePass: string = "";
  @Input()
  title: string;

  @Input()
  user: User;

  types: string[];

  @Output()
  dataEvent: EventEmitter<{ user: User, imageFile: string }>;

  //----------------CONSTRUCTOR------------------

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.types = ['text', 'text', 'password', 'text'];
    this.imageUrl = null;
    this.dataEvent = new EventEmitter<{ user: User, imageFile: string }>();
  }

  //------------------METHODS---------------------

  ngOnInit() {
    if (this.user.profileImageName != null)
      this.imageUrl = `${Global.Uploads}/Images/${this.user.profileImageName}`;
    if (this.user.Password == "") {
      this.showPassword = true;
    }
    else {
      this.pass = this.user.Password;
    }
    this.initFormGroup();
    this.getAllStatuses();
  }

  initFormGroup() {
    if (this.showPassword == true) {
      this.userFormGroup = this.formBuilder.group({
        name: [this.user.Name, stringValidatorArr("name", 2, 15, /^[A-Za-z]+$/)],
        userName: [this.user.UserName, stringValidatorArr("user name", 2, 15, /^[A-Za-z]+$/)],
        password: [this.user.Password, stringValidatorArr("password", 5, 10)],
        email: [this.user.EMail, stringValidatorArr("email", 15, 30,/^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/)],
        statusId: [this.user.StatusId],
        managerId: [this.user.ManagerId]
      });
    }
    else {
      this.userFormGroup = this.formBuilder.group({
        name: [this.user.Name, stringValidatorArr("name", 2, 15, /^[A-Za-z]+$/)],
        userName: [this.user.UserName, stringValidatorArr("user name", 2, 15, /^[A-Za-z]+$/)],
        password: ['', null],
        email: [this.user.EMail, stringValidatorArr("email", 15, 30, /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[a-z]{2,4}$/)],
        statusId: [this.user.StatusId],
        managerId: [this.user.ManagerId]
      });
      this.password.setValue("111111");
    }
  }

  getAllStatuses() {
    this.userService.getAllStatuses().subscribe(
      statuses => {
        this.allStatuses = statuses;
        this.getAllTeamLeaders();
      },
      err => console.log(err));
  }

  getAllTeamLeaders() {
    this.userService.getAllTeamLeaders().subscribe(
      (teamLeaders: User[]) => {
        this.allTeamLeaders = teamLeaders;
      },
      err => console.log(err));
  }



  async onSubmit() {
    this.isExistUserName = false;
    let user: User = this.userFormGroup.value;
    user.Id = this.user.Id;
    if (!this.showPassword)
      user.Password = this.pass;
    else
      user.Password = await sha256(this.password.value);
    let imageFile: string = this.imageFile;
    let data = { user, imageFile };
    this.dataEvent.emit(data);
    this.router.navigate(['/taskManagement/manager/userManagement/userList']);
  }

  /**
   * @method
   * get image from event emitter of 'upload-image' component 
   * when user choose his profile image
   */
  getImage(value: any) {
    this.imageFile = value;
  }
  //----------------GETTERS-------------------

  //getters of the form group controls

  get name() {
    return this.userFormGroup.controls["name"];
  }

  get email() {
    return this.userFormGroup.controls["email"];
  }

  get userName() {
    return this.userFormGroup.controls["userName"];
  }

  get password() {
    return this.userFormGroup.controls["password"];
  }

  get confirmPassword() {
    return this.userFormGroup.controls["confirmPassword"];
  }

  get statusId() {
    return this.userFormGroup.controls["statusId"];
  }

  get project() {
    return this.userFormGroup.controls["project"];
  }

  get manager() {
    return this.userFormGroup.controls["managerId"];
  }

}
