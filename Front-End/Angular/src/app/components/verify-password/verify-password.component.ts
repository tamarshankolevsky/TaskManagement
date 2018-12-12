import { Component } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../../imports';

@Component({
  selector: 'app-verify-password',
  templateUrl: './verify-password.component.html',
  styleUrls: ['./verify-password.component.css']
})
export class VerifyPasswordComponent {

  //----------------PROPERTIES-------------------

  formGroup: FormGroup;
  obj: typeof Object = Object;

  //----------------CONSTRUCTOR------------------

  constructor(
    private userService: UserService,
    private router: Router
  ) {
    let formGroupConfig = { userPassword: new FormControl("") };
    this.formGroup = new FormGroup(formGroupConfig);
  }

  //----------------METHODS-------------------

  submitLogin() {
    console.log(this.formGroup.value);////////////
    console.log(this.formGroup.controls);
    try {
      this.userService.VerifyPassword(this.formGroup.value.userPassword).subscribe(res => {
        if (res != 'e') {
          this.router.navigateByUrl('taskManagment/newPassword/' + res.UserId);
        }
        else {
          alert("Incorrect Password");
        }
      });
    }
    catch (e) {
      alert("Login failed!!!");
    }
  }

}
