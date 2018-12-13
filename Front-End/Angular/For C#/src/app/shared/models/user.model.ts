export class User {
    constructor(
        public Id: number,
        public Name: string,
        public UserName: string,
        public Password: string,
        public StatusId: number,
        public EMail: string,
        public ManagerId: number,
        public profileImageName: string
    ) { }
}


export enum Estatus {
    MANAGER=1,
    TEAMLEADER=2,
    QA=3,
    UIUX=4,
    DEVELOPER=5
}