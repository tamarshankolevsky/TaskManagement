import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Global } from '../../imports';

@Injectable()
export class AuthenticationService {

    //----------------PROPERTIES-------------------

    basicURL: string = "http://localhost:53728" + '/api';

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) { }

    //----------------METHODS-------------------

    //POST
    login(userName: string, password: string): Observable<any> {
        let url: string = `${this.basicURL}/login`;
        let data = { userName: userName, password: password };
        return this.httpClient.post(url, data);
    }

    logout() {
        //remove user from local storage to log user out
        localStorage.removeItem(Global.CurrentUser);
    }

}