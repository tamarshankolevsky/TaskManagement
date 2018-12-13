import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { TreeTable } from '../../imports';

@Injectable({
  providedIn: 'root'
})
export class TreeTableService {

  //----------------CONSTRUCTOR------------------

  constructor(private httpClient: HttpClient) { }

  //----------------METHODS-------------------
 
  //GET
  GetTreeTable(): Observable<TreeTable[]> {
    return this.httpClient.get("http://localhost:53728/api/getTreeTable")
      .pipe(
        map((res: TreeTable[]) => res),
        catchError((r: Response) => Observable.throw(r))
      );
  }

}
