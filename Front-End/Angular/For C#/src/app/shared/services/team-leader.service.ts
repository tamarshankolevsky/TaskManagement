import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TeamLeaderService {

  //----------------PROPERTIES-------------------

  basicURL: string = "http://localhost:53728" + '/api';

  //----------------CONSTRUCTOR------------------

  constructor(private httpClient: HttpClient) { }

  //-----------------METHODS--------------------

   //GET
   getWorkersForProject(id: number): Observable<any>  {
    let url: string = `${this.basicURL}/getWorkersForProject`;
    return this.httpClient.get(url+ `/${id}`);
  }

  //GET
  getProjectsHours(idTeamLeader: number, id: number): Observable<any>  {
    let url: string = `${this.basicURL}/getWorkerHours`;
    return this.httpClient.get(url + `/${idTeamLeader }/${id}`);
  }
  
  //PUT
  setAlloactedHours(numHours: number, id: number): Observable<any>  {
    let url: string = `${this.basicURL}/updateWorkerHours`;
    return this.httpClient.put(url, {
      "numHours": numHours,
      "projectWorkerId": id
    });
  }

}
