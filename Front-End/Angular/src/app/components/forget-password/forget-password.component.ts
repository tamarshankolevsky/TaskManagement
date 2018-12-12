import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { stringValidatorArr, UserService } from '../../imports';

@Component({
  selector: 'app-forget-password',
  templateUrl: './forget-password.component.html',
  styleUrls: ['./forget-password.component.css']
})
export class ForgetPasswordComponent {

  //----------------PROPERTIES-------------------

  formGroup: FormGroup;
  obj: typeof Object = Object;

  //----------------CONSTRUCTOR------------------

  constructor(
    private userService: UserService,
    private router: Router
  ) {
    let formGroupConfig = { userName: new FormControl("", stringValidatorArr("name", 2, 15)) };
    this.formGroup = new FormGroup(formGroupConfig);
  }

  //----------------METHODS-------------------

  submitLogin() {
    try {
      this.userService.VerifyUserName(this.formGroup.value.userName).subscribe(res => {
        if (res == "ok")
          this.router.navigateByUrl('taskManagment/verifyPassword');
        else
          alert("Your user name doesnt exist");
      });
    }
    catch (e) {
      alert("Login failed!!!");
    }
  }

}
