import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import sha256 from 'async-sha256';
import { UserService, User, stringValidatorArr } from '../../imports';

@Component({
  selector: 'app-new-password',
  templateUrl: './new-password.component.html',
  styleUrls: ['./new-password.component.css']
})

export class NewPasswordComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  id: any;
  user: User;
  formGroup: FormGroup;
  obj: typeof Object = Object;

  //----------------CONSTRUCTOR------------------

  constructor(
    private activatedRoute: ActivatedRoute,
    private userservice: UserService,
    private router: Router
  ) {
    let formGroupConfig = { userPassword: new FormControl("", stringValidatorArr("password", 6, 64)) };
    this.formGroup = new FormGroup(formGroupConfig);
  }

  //----------------METHODS-------------------

  ngOnInit() {
    this.id = this.activatedRoute.snapshot.paramMap.get('id');
    this.userservice.getUser(this.id).subscribe(
      user => {
        this.user = user;
      });
  }

  async submitLogin() {
    this.user.Password = await sha256(this.formGroup.value.userPassword);
    this.userservice.EditPassword(this.user).subscribe(res => {
      alert("Created new password succeed");
      this.router.navigateByUrl('');
    })
  }

}
