export class Utils{
    
    //----------------PROPERTIES-------------------

    static  isPhpServer: boolean=false;
    static basicURL: string = "http://localhost:53728" + '/api';
    static  basicURLPhp:string="http://localhost:80/WebApi/index.php";

    //-----------------METHODS--------------------

    public static getUrl(functionName: FunctionName): string {
        var url;
        if (this.isPhpServer){
            url = this.basicURLPhp+"?function="+functionName.toString();
        }
        else{
            url = this.basicURL+"/"+ "login";
        }
        return url;
    }
}

export enum FunctionName {
    getAllUsers=1,
    getAllManagers=2,
    getAllTeamLeaders=3,
    LOGIN
}