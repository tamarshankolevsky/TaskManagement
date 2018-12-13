export class DailyPresence {
    constructor(
        public id: number,
        public userProjectId: number,
        public date: Date,
        public startTime: Date,
        public endTime: Date
    ) { }
}