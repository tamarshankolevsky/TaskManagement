import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { Global, ProjectFilter } from '../../imports';
import { Utils, FunctionName } from './utils';
//import * as XLSX from 'xlsx';

@Injectable()
export class ReportService {

    //----------------PROPERTIES-------------------

    basicURL: string =  "http://localhost:8080/managerPHP/index.php/";
    filterSubject:Subject<ProjectFilter>;
    
    //----------------CONSTRUCTOR------------------

    constructor(private httpClient: HttpClient) { 
        this.filterSubject=new Subject<ProjectFilter>();
    }

    //----------------METHODS-------------------

    //GET
    getPresence(): Observable<any> {
        let url: string = `${this.basicURL}/getPresence`;
        return this.httpClient.get(url);
    }

    //GET
    getHours(projectId:number): Observable<any> {
        let url: string = `${this.basicURL}/getHours`;
        return this.httpClient.get(url+ `/${projectId}`);
    }

    static toExportFileName(excelFileName: string): string {
        return `${excelFileName}_export_${new Date().getTime()}.xlsx`;
    }

}

