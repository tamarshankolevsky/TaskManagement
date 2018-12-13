import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable, Subject } from 'rxjs';
import { Global, WorkerHours } from '../../imports';

@Injectable()
export class WorkerHoursService {

    //----------------PROPERTIES-------------------

    basicURL: string = "http://localhost:53728/api";
    changeWorkerHoursSubject: Subject<WorkerHours>;
    deleteWorkerHoursSubject: Subject<WorkerHours>;
    addWorkerHoursSubject: Subject<WorkerHours>;
    subStr: string = "";
    bodyStr: string = "";

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) {
        this.changeWorkerHoursSubject = new Subject<WorkerHours>();
        this.deleteWorkerHoursSubject = new Subject<WorkerHours>();
        this.addWorkerHoursSubject = new Subject<WorkerHours>();
    }

    //----------------METHODS-------------------

    //GET
    getAllWorkerHours(workerId: number): Observable<any> {
        let url: string = `${this.basicURL}/getAllWorkerHours?workerId=${workerId}`;
        return this.httpClient.get(url);
    }

    //POST
    addWorkersHours(workerHoursList: WorkerHours[]): Observable<any> {
        let url: string = `${this.basicURL}/addWorkersHours`;
        return this.httpClient.post(url, workerHoursList);
    }

    //GET
    getWorkersHours(id: number): any {
        let url: string = `${this.basicURL}/getWorkersHours/${id}`;
        return this.httpClient.get(url);
    }

    //PUT
    editWorkersHours(workerHoursList: WorkerHours[]): Observable<any> {
        let url: string = `${this.basicURL}/editWorkersHours`;
        return this.httpClient.put(url, workerHoursList);
    }

    //POST
    deleteWorkersHours(userIdList: number[]): Observable<any> {
        let url: string = `${this.basicURL}/deleteWorkersHours`;
        return this.httpClient.post(url, userIdList);
    }

    //GET
    getProject(id: number): Observable<any> {
        let url: string = `${this.basicURL}/getProject`;
        return this.httpClient.get(url + `/${id}`);
    }

    //POST
    sendMsg(): any {
        let url: string = `${this.basicURL}/sendMsg`;
        let data = { body: this.bodyStr, sub: this.subStr, id: JSON.parse(localStorage.getItem(Global.CurrentUser)).Id };
        return this.httpClient.post(url, data);
    }

    //POST
    updateStartHour(time: number, id: number, isFirst: boolean): Observable<any> {
        let url: string = `${this.basicURL}/updateStartHour`;
        let data = { hour: time, idProjectWorker: id, isFirst: isFirst };
        return this.httpClient.post(url, data);
    }

}
