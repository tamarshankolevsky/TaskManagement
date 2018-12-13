import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import * as  sha256 from 'async-sha256';
import { Global, stringValidatorArr, User, Estatus, UserService } from '../../imports';
import { AuthenticationService } from '../../shared/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  loginFormGroup: FormGroup;
  isExistUser: boolean = true;
  hashPassword: string;
  worker: User;
  //allow access 'Object' type via interpolation
  objectHolder: typeof Object = Object;

  //----------------CONSTRUCTOR------------------

  constructor(private formBuilder: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService) {
    this.loginFormGroup = this.formBuilder.group({
      userName: ['', stringValidatorArr("userName", 2, 15, /^[A-Za-z]+$/)],
      password: ['', stringValidatorArr("password", 5, 10)]
    });
  }

  //----------------METHODS-------------------

  ngOnInit() {
    //if the worker loged in - navigate to the match page
    this.worker = JSON.parse(localStorage.getItem(Global.CurrentUser));
    this.navigateToMatchPage();
  }

  async onSubmit() {
    this.hashPassword = await sha256(this.password.value);
    this.login();
  }

  login() {
    this.authenticationService.login(this.userName.value, this.hashPassword)
      .subscribe(
        (user: User) => {
          if (user != null) {
            //enter user to localStorage
            let myUser: User = user;
            delete myUser['Password'];
            localStorage.setItem(Global.CurrentUser, JSON.stringify(myUser));
            this.worker = user;
            this.navigateToMatchPage();
          }
          else
            this.isExistUser = false;
        },
        err => console.log(err));
  }

  // /**
  //  * function -
  //  * navigate the current user to the match page
  //  */
  navigateToMatchPage() {
    if (this.worker) {
      if (this.worker.StatusId == Estatus.MANAGER)
        this.router.navigate(['/taskManagement/manager']);
      else if (this.worker.StatusId == Estatus.TEAMLEADER)
        this.router.navigate(['/taskManagement/teamLeader']);
      else
        this.router.navigate(['/taskManagement/worker']);
    }
  }

  //----------------GETTERS-------------------

  //getters of the form group controls

  get userName() {
    return this.loginFormGroup.controls["userName"];
  }
  get password() {
    return this.loginFormGroup.controls["password"];
  }

}
