import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User, Global, Project } from '../../imports';

@Injectable()
export class ProjectService {
    //----------------PROPERTIRS-------------------

    basicURL: string =  "http://localhost:8080/managerPHP/index.php/";
    user: User;
    idsWorkers: number[];

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) {
        this.user = JSON.parse(localStorage.getItem(Global.CurrentUser));
    }

    //----------------METHODS-------------------

    //PUT
    addProject(project: Project): Observable<any> {
        let url: string = `${this.basicURL}/project/addProject`;
        return this.httpClient.post(url, project);//+ `/${this.user.UserName}`
    }

    // //GET
    // getProject(projectId: number): Observable<any> {
    //     let url: string = `${this.basicURL}/getProject`;
    //     return this.httpClient.get(url + `/${projectId}`);
    // }

    // //PUT
    // updateProject(project: Project): Observable<any> {
    //     let url: string = `${this.basicURL}/updateUser`;
    //     return this.httpClient.put(url, project);
    // }

    //GET
    getAllProjects(): Observable<any> {
        let url: string = `${this.basicURL}/project/getAllProjects`;
        return this.httpClient.get(url);
    }

    // //GET
    // getAllTeamLeaderProjects(): Observable<any> {
    //     let url: string = `${this.basicURL}/getAllTeamLeaderProjects`;
    //     return this.httpClient.get(url);
    // }

    //POST
    addWorkersToProject(workers:any[], name): Observable<any> {/////TODO
        this.idsWorkers = [];
        workers.forEach(worker => {
            this.idsWorkers.push(worker);
           
        });
        let url: string = `${this.basicURL}/project/addWorkersToProject`;
        return this.httpClient.post(url+"/project/addWorkersToProject",
        {
            "projectName":name,
            "ids":this.idsWorkers});
    }

    //PUT
    editProject(projectId: number, DevelopHours: number, QAHours: number, UIUXHours: number, endDate: Date,isComplete:boolean): Observable<any> {
        let url: string = `${this.basicURL}/project/updateProjectDetails`;
        return this.httpClient.post(url, {
            "projectId": projectId,
            "developHours": DevelopHours,
            "QAHours": QAHours,
            "UIUXHours": UIUXHours,
            "endDate": endDate,
            "isComplete":isComplete
        });
    }

}