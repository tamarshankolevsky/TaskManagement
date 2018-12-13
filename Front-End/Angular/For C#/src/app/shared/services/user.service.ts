import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Router } from '@angular/router';
import { Global, User, Estatus } from '../../imports';

@Injectable()
export class UserService {

    //----------------PROPERTIES-------------------

    basicURL: string = "http://localhost:53728" + '/api';//Global.Host;//todo
    updateUserListSubject: Subject<void>;
    user: User;

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient, private router: Router) {
        this.updateUserListSubject = new Subject<void>();
        this.user = JSON.parse(localStorage.getItem(Global.CurrentUser));
    }

    //----------------METHODS-------------------

    /**
  * function -
  * navigate the current user to the match page
  */
    navigateToMatchPage() {
        if (localStorage.getItem(Global.CurrentUser)) {
            this.router.navigate(['/taskManagement/login']);
        }
        let status = JSON.parse(localStorage.getItem(Global.CurrentUser)).status;
        if (status == Estatus.MANAGER) {
            this.router.navigate(['/taskManagement/manager']);
        }
        else
            if (status == Estatus.TEAMLEADER) {
                this.router.navigate(['/taskManagement/teamLeader']);
            }
            else
                this.router.navigate(['/taskManagement/worker']);
    }

    //POST
    addUser(newUser: User): Observable<any> {
        let url: string = `${this.basicURL}/addUser`;
        return this.httpClient.post(url, newUser); 
    }

    //PUT
    updateUser(user: User): Observable<any> {
        let url: string = `${this.basicURL}/updateUser`;
        return this.httpClient.put(url, user);
    }

    //DELETE
    deleteUser(userId: number): Observable<any> {
        let url: string = `${this.basicURL}/deleteUser`;
        return this.httpClient.delete(url + `/${userId}`);
    }

    //GET
    getUser(userId): Observable<any> {
        let url: string = `${this.basicURL}/getWorkerDetails`;
        return this.httpClient.get(url + `/${userId}`);
    }

    //GET
    getAllUsers(): Observable<any> {
        let url: string = `${this.basicURL}/getAllUsers`;
        return this.httpClient.get(url);
    }

    //GET
    getAllTeamLeaders(): Observable<any> {
        let url: string = `${this.basicURL}/getAllTeamLeaders`;
        return this.httpClient.get(url);
    }

    //GET
    getAllManagers(): Observable<any> {
        let url: string = `${this.basicURL}/getAllManagers`;
        return this.httpClient.get(url);
    }

    //GET
    getAllStatuses(): Observable<any> {
        let url: string = `${this.basicURL}/getAllStatuses`;
        return this.httpClient.get(url);
    }

    //GET
    getTeamLedaWorkers(teamLeaderId: number): Observable<any> {
        let url: string = `${this.basicURL}/getWorkersDeatails`;
        return this.httpClient.get(url + `/${teamLeaderId}`);
    }

    //GET
    getRemainingHours(userProjectid: number, statusId: number): Observable<any> {
        let url: string = `${this.basicURL}/getRemainingHours`;
        return this.httpClient.get(url + `/${userProjectid}` + `/${statusId}`);
    }

}