import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Global, Estatus } from '../imports';

@Injectable()
export class AuthGuard implements CanActivate {

    //----------------CONSTRUCTOR------------------

    constructor(private router: Router) { }

    //----------------METHODS-------------------

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {//todo
        if (state.url == '/taskManagement/login') {

            if (localStorage.getItem(Global.CurrentUser) == null)
                return true;

            return false;

            // this.userService.navigateToMatchPage();

        }
        else if (route.url[0].path == 'manager') {
            if (localStorage.getItem(Global.CurrentUser) != undefined) {
                let status = +localStorage.getItem(JSON.parse(localStorage.getItem(Global.CurrentUser)).StatusId);
                if (status == Estatus.MANAGER) {
                    return true;
                }
            }
            else {

                this.router.navigate(['/taskManagement/login']);
                return false;
            }
            //   this.userService.navigateToMatchPage();
            return false;
        }
        if (route.url[0].path == 'teamLeader') {
            let status = +localStorage.getItem(JSON.parse(localStorage.getItem(Global.CurrentUser)).StatusId);
            if (status == Estatus.TEAMLEADER)
                return true;
            // this.userService.navigateToMatchPage();
            return false;
        }
        if (route.url[0].path == 'worker') {
            let status = +localStorage.getItem(JSON.parse(localStorage.getItem(Global.CurrentUser)).StatusId);
            if (status != Estatus.MANAGER && status != Estatus.TEAMLEADER)
                return true;
            //  this.userService.navigateToMatchPage();
            return false;
        }
    }

}
