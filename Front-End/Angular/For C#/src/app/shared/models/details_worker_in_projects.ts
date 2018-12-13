import { WorkerHours } from "../../imports";

export class DetailsWorkerInProjects {
    public  UserId :number;
    public  Name :string;
    public  Status :string;
    public  Hours :number
    public  ActualHours:WorkerHours[]=[];
    public  TeamLeaderName :string;   
}
