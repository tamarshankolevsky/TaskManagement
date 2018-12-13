import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Router } from '@angular/router';
import { Global, User, Estatus } from '../../imports';
import { Utils, FunctionName } from './utils';

@Injectable()
export class UserService {

    //----------------PROPERTIES-------------------

    basicURL: string = "http://localhost:8080/managerPHP/index.php/";
    updateUserListSubject: Subject<void>;
    user: User;
    // isPhpServer: boolean=false;
    // basicURLPhp:string="http://localhost:80/WebApi/index.php";

    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient, private router: Router) {
        this.updateUserListSubject = new Subject<void>();
        this.user = JSON.parse(localStorage.getItem(Global.CurrentUser));
    }

    //----------------METHODS-------------------

    // getUrl(functionName: string): string {
    //     var url;
    //     if (this.isPhpServer){
    //         url = this.basicURLPhp+"?function="+functionName;
    //     }
    //     else{
    //         url = this.basicURL+"/"+ functionName;
    //     }
    //     return url;
    // }

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
        let url: string = `${this.basicURL}/user/addUser`;
        return this.httpClient.post(url, newUser); // + `/${this.user.UserName}`
    }

    //PUT
    updateUser(user: User): Observable<any> {
        let url: string = `${this.basicURL}/user/updateUser`;
        return this.httpClient.put(url, user);//+ `/${this.user.UserName}`
    }

    //DELETE
    deleteUser(userId: number): Observable<any> {
        let url: string = `${this.basicURL}/user/deleteUser`;
        return this.httpClient.get(url +`?userId=${userId}`);//+ `/${this.user.UserName}`
    }

    //GET
    getUser(userId): Observable<any> {
        let url: string = `${this.basicURL}/user/getUser`;
        return this.httpClient.get(url + `?userId=${userId}`);
    }

    //GET
    getAllUsers(): Observable<any> {
        let url: string = `${this.basicURL}/user/getAllUsers`;
        // let url: string = Utils.getUrl(FunctionName.getAllUsers);
        return this.httpClient.get(url);//+ '/userName'
    }

    //GET
    getAllTeamLeaders(): Observable<any> {
        let url: string = `${this.basicURL}/user/getAllTeamLeaders`;
        return this.httpClient.get(url);
    }

    //GET
    getAllManagers(): Observable<any> {
        let url: string = `${this.basicURL}/user/getAllManagers`;
        return this.httpClient.get(url);
    }

    //GET
    getAllStatuses(): Observable<any> {
        let url: string = `${this.basicURL}/status/getAllStatuses`;
        return this.httpClient.get(url);
    }
    //GET
    getTeamLedaWorkers(teamLeaderId: number): Observable<any> {
        let url: string = `${this.basicURL}/user/getWorkersDeatails`;
        return this.httpClient.get(url + `?teamLeaderId=${teamLeaderId}`);
    }
    //POST
    uploadImageProfile(image: any): Observable<any> {
        let url: string = `${this.basicURL}/uploadImageProfile`;
        let formData: FormData = new FormData();
        formData.append('file', image, image.name);
        return this.httpClient.post(url, formData);
    }

    //POST
    removeUploadedImage(profileImageName: string): Observable<any> {
        let url: string = this.basicURL + `/removeUploadedImage`;
        let formData: FormData = new FormData();
        formData.append('profileImageName', profileImageName);
        return this.httpClient.post(url, formData);
    }

     //GET
     getRemainingHours(userProjectid:number,statusId:number):Observable<any>{
        let url: string = `${this.basicURL}/teamLeader/getRemainingHours`;
        return this.httpClient.get(url + `?userProjectid=${userProjectid}&statusId=${statusId}`);
    }
}