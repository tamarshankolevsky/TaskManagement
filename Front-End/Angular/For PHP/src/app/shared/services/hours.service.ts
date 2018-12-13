import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Global } from '../../imports';

@Injectable()
export class HoursService {

    //----------------PROPERTIES-------------------

    basicURL: string =  "http://localhost:8080/managerPHP/index.php/";
    subjectUpdateChart=new Subject();
    projectStart:any=null;

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) { }

    //----------------METHODS-------------------

    //GET
    getWorkersHours(): Observable<any> {
        let url: string = `${this.basicURL}/hours/getWorkersHours`;
        return this.httpClient.get(url + `${Global.CurrentUser}`);
    }

    //GET
    getWorkerHours(userId: number): Observable<any> {
        let url: string = `${this.basicURL}/hours/getWorkerHours`;
        return this.httpClient.get(url + '?params=/id');
    }

    //GET
    updaterWorkerHours(userId:number): Observable<any> {
        let url: string = `${this.basicURL}/updaterWorkerHours`;
        return this.httpClient.get(url+`/${userId}`);
    }
    
}