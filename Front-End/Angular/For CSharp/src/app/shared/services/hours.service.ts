import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Global } from '../../imports';

@Injectable()
export class HoursService {

    //----------------PROPERTIES-------------------

    basicURL: string = Global.Host + '/api';
    subjectUpdateChart=new Subject();
    projectStart:any=null;

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) { }

    //----------------METHODS-------------------

    //GET
    getWorkersHours(): Observable<any> {
        let url: string = `${this.basicURL}/getWorkersHours`;
        return this.httpClient.get(url + `${Global.CurrentUser}`);
    }

    //GET
    getWorkerHours(userId: number): Observable<any> {
        let url: string = `${this.basicURL}/getWorkerHours`;
        return this.httpClient.get(url + '/id');
    }

    //GET
    updaterWorkerHours(userId:number): Observable<any> {
        let url: string = `${this.basicURL}/updaterWorkerHours`;
        return this.httpClient.get(url+`/${userId}`);
    }
    
}