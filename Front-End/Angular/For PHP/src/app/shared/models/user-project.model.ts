export class UserProject {
    constructor(
        public id: number,
        public userId: number,
        public projectId: number,
        public allocatedHours: Date
    ) { }
}