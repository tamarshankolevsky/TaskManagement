export class Project {
    constructor(
        public Id:number,
        public Name: string,
        public TeamLeaderId: number,
        public Customer: string,
        public DevelopHours: number,
        public QAHours: number,
        public UiUxHours: number,
        public StartDate: Date,
        public EndDate:Date,
        public IsComplete:boolean
    ) { }
}
