import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Global } from '../../imports';
import { Utils, FunctionName } from './utils';

@Injectable()
export class AuthenticationService {

    //----------------PROPERTIES-------------------

    basicURL: string = "http://localhost:8080/managerPHP/index.php/";

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) { }

    //----------------METHODS-------------------

    //POST
    login(userName: string, password: string): Observable<any> {
        //   let url: string = Utils.getUrl(FunctionName.LOGIN);
        let url: string = `${this.basicURL}/user/login`;
        let data = { userName: userName, password: password };
        return this.httpClient.post(url, data);
    }

    logout() {
        //remove user from local storage to log user out
        localStorage.removeItem(Global.CurrentUser);
    }

}