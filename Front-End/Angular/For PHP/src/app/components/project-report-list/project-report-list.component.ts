import { Component, OnInit, Input } from '@angular/core';
//import { Tree, TreeNode } from 'primeng/primeng';
import { TreeTableModule } from 'primeng/treetable';
import { TreeNode } from 'primeng/api';
import { formatDate } from '@angular/common';
import { CalendarModule } from 'primeng/calendar';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { FormBuilder, FormGroup } from '@angular/forms';
import {
  User, Project, DetailsWorkerInProjects, TreeTable,
  UserService, ProjectService, TreeTableService, ExcelService
} from '../../imports';

@Component({
  selector: 'app-project-report-list',
  templateUrl: './project-report-list.component.html',
  styleUrls: ['./project-report-list.component.css']
})

export class ProjectReportListComponent implements OnInit {

  //----------------PROPERTIES-------------------

  one: boolean = false;
  treeTable: TreeTable[] = [];
  allProjects: Project[] = [];
  files1: TreeNode[] = [];
  filterd: TreeNode[] = [];
  filterdUser: TreeNode[] = [];
  cols: any[];
  arr: any[] = [];
  startDateValue: Date = null;
  endDateValue: Date = null;
  date12: Date;
  rangeDates: Date[];
  minDate: Date;
  maxDate: Date;
  es: any;
  tr: any;
  invalidDates: Array<Date>
  teamLeaders: User[] = [];
  allUsers: User[] = [];
  projectInput: string = "";
  userInput: string = "";
  teamLeaderInput: string = "";
  startChoose: boolean = false;
  endChoose: boolean = false;
  reportFormGroup: FormGroup;
  state: string;

  //----------------CONSTRUCTOR-------------------

  constructor(
    private formBuilder: FormBuilder,
    private treeTableService: TreeTableService,
    private userService: UserService,
    private projectService: ProjectService,
    private excelService: ExcelService
  ) {
    this.treeTableService.GetTreeTable().subscribe(res => {
      this.treeTable = res;
      this.userService.getAllUsers().subscribe(res => {
        this.allUsers = res;
        //  get all projects from service
        this.projectService.getAllProjects().subscribe(res => {
          //get all team leaders 
          this.allProjects = res;
          this.userService.getAllTeamLeaders().subscribe(res => { this.teamLeaders = res });
        })
      });
      //this.fillDate();
      this.treeTable.forEach(element => {
        this.files1.push({
          data: {
            ProjectName: element["Project"].Name
          }
        })
      });
      this.initProjectsInfo();
      this.cols = [
        { field: 'name', header: 'Name' },
        { field: 'teamLeader', header: 'TeamLeader' },
        { field: 'hours', header: 'Hours' },
        { field: 'actualHours', header: 'ActualHours' },
        { field: 'percent', header: 'Percent' },
        { field: 'customer', header: 'Customer' },
        { field: 'startDate', header: 'Start' },
        { field: 'endDate', header: 'End' }
      ];
    })
  }

  //-----------------METHODS--------------------

  ngOnInit() {
    this.initFormGroup();
  }

  exportToExcel() {
    var arr = [];
    this.filterd.forEach(v => arr.push(v.data));
    this.excelService.exportAsExcelFile(arr, "report");
  }

  initFormGroup() {
    this.reportFormGroup = this.formBuilder.group({
      teamLeaderId: [''],
      userId: [''],
      projectId: ['']
    });
  }

  fillDate() {
    this.es = {
      firstDayOfWeek: 1,
      dayNames: ["domingo", "lunes", "martes", "miércoles", "jueves", "viernes", "sábado"],
      dayNamesShort: ["dom", "lun", "mar", "mié", "jue", "vie", "sáb"],
      dayNamesMin: ["D", "L", "M", "X", "J", "V", "S"],
      monthNames: ["enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre"],
      monthNamesShort: ["ene", "feb", "mar", "abr", "may", "jun", "jul", "ago", "sep", "oct", "nov", "dic"],
      today: 'Hoy',
      clear: 'Borrar'
    }

    this.tr = {
      firstDayOfWeek: 1
    }

    let today = new Date();
    let month = today.getMonth();
    let year = today.getFullYear();
    let prevMonth = (month === 0) ? 11 : month - 1;
    let prevYear = (prevMonth === 11) ? year - 1 : year;
    let nextMonth = (month === 11) ? 0 : month + 1;
    let nextYear = (nextMonth === 0) ? year + 1 : year;
    this.minDate = new Date();
    this.minDate.setMonth(prevMonth);
    this.minDate.setFullYear(prevYear);
    this.maxDate = new Date();
    this.maxDate.setMonth(nextMonth);
    this.maxDate.setFullYear(nextYear);

    let invalidDate = new Date();
    invalidDate.setDate(today.getDate() - 1);
    this.invalidDates = [today, invalidDate];
  }

  initProjectsInfo() {
    this.files1 = this.treeTable.map(project => this.getProjectInfo(project));
    this.filterd = this.files1;
  }

  openRequestForm() {
    this.state = "in";
  }

  closeRequestForm() {
    this.state = "out"
  }

  //----------update the filter ----------

  //by team leader
  onChangeTeamLeader(event: any) {
    this.teamLeaderInput = event.value;
    this.updateFilter();
  }

  //by project
  onChangeProject(event: any) {
    this.projectInput = event.value;
    this.updateFilter();
  }

  //by worker
  onChangeUser(event: any) {
    this.userInput = event.value;
    this.updateFilter();
  }

  //by start date
  onChangeStartDate() {
    this.startChoose = true;
    this.updateFilter();
    //this.checkFilter();
  }

  //by end date
  onChangeEndDate() {
    this.endChoose = true;
    this.updateFilter();
    //this.checkFilter();
  }

  ChangeStartDate() {
    this.filterd = this.filterd.filter(t => new Date(t.data["StartDate"]).getMonth() == this.startDateValue.getMonth() && new Date(t.data["StartDate"]).getFullYear() == this.startDateValue.getFullYear());
  }

  ChangeEndDate() {
    this.filterd = this.filterd.filter(t => new Date(t.data["EndDate"]).getMonth() == this.endDateValue.getMonth() && new Date(t.data["EndDate"]).getFullYear() == this.endDateValue.getFullYear());
  }

  /**
   * function
   * update the filter by the chooosen parameters
   */
  updateFilter() {
    this.filterd = this.files1;
    let x = [];
    this.filterdUser = [];
    this.filterd.filter(project => {
      if (
        (this.userInput == "" ||this.userInput == undefined || project.children.some(d => d.children.some(u => u.data["name"] == this.userInput))) &&
        (this.teamLeaderInput == "" ||this.teamLeaderInput == undefined || project.data["teamLeader"] == this.teamLeaderInput) &&
        (this.projectInput == "" ||this.projectInput==undefined || project.data["name"] == this.projectInput) &&
        (!this.startChoose || new Date(project.data["startDate"]).getMonth() >= this.startDateValue.getMonth()) &&
        (!this.endChoose || new Date(project.data["endDate"]).getMonth() <= this.endDateValue.getMonth())
      ) {
        x.push(project);
      }
    }
    );
    this.startChoose = false;
    this.endChoose = false;
    this.filterd = x;
  }


  getProjectInfo(project: TreeTable) {
   
    let hours = project.Project.QAHours + project.Project.UiUxHours + project.Project.DevelopHours;
    let actualhorsForProject = this.getActualHoursForProject(project);
    let root = {
      data: {
        name: project.Project.Name,
        teamLeader: project.User.UserName,
        hours: hours,
        percent: this.getPrecentOfNumbers(hours, actualhorsForProject),
        customer: project.Project.Customer,
        startDate: project.Project.StartDate,//formatDate(project.Project.StartDate, 'dd-mm-yyyy','en'),
        endDate: project.Project.EndDate,//formatDate(project.Project.EndDate,'dd-mmmm-yyyy','en'), 
        actualHours: actualhorsForProject
      },
      children: []
    };

    let actualHoursForDepartment = this.getActualHoursForDepartment(project, "developer");
    let departmentNode = {
      data: {
        name: "Developers Hours",
        hours: project.Project.DevelopHours,
        actualHours: actualHoursForDepartment,
        percent: this.getPrecentOfNumbers(hours, actualHoursForDepartment),
      },

      children: []
    };

    project.DetailsWorkerInProjects.forEach(worker => {
      if (worker.Status == "developer") {
        let actualHoursforWorker = this.getCountHours(worker)
        let workerNode = {
          data: {
            name: worker.Name,
            actualHours: actualHoursforWorker,
            hours: worker.Hours,
            percent: this.getPrecentOfNumbers(hours, actualHoursforWorker),
            teamLeader: worker.TeamLeaderName
          }
        };
        departmentNode.children.push(workerNode);
      }
    })
    root.children.push(departmentNode);
    let actualHoursForDepartment1 = this.getActualHoursForDepartment(project, "QAHours");
    let departmentNode1 = {
      data: {
        name: "QA Hours",
        hours: project.Project.QAHours,
        actualHours: actualHoursForDepartment1,
        percent: this.getPrecentOfNumbers(hours, actualHoursForDepartment1),
      },

      children: []
    };

    project.DetailsWorkerInProjects.forEach(worker => {
      if (worker.Status == "QAHours") {
        let actualHoursforWorker = this.getCountHours(worker)
        let workerNode = {
          data: {
            name: worker.Name,
            actualHours: actualHoursforWorker,
            hours: worker.Hours,
            percent: this.getPrecentOfNumbers(hours, actualHoursforWorker),
            teamLeader: worker.TeamLeaderName
          }
        };
        departmentNode1.children.push(workerNode);
      }
    })
    root.children.push(departmentNode1);
    let actualHoursForDepartment2 = this.getActualHoursForDepartment(project, "UIUXHours");
    let departmentNode2 = {
      data: {
        name: "UI/UX Hours",
        hours: project.Project.UiUxHours,
        actualHours: actualHoursForDepartment2,
        percent: this.getPrecentOfNumbers(hours, actualHoursForDepartment2),
      },
      children: [

      ]
    };
 

    project.DetailsWorkerInProjects.forEach(worker => {
      if (worker.Status == "UIUXHours") {
        let actualHoursforWorker = this.getCountHours(worker)
        let workerNode = {
          data: {
            name: worker.Name,
            actualHours: actualHoursforWorker,
            percent: this.getPrecentOfNumbers(hours, actualHoursforWorker),
            teamLeader: worker.TeamLeaderName,
            hours: worker.Hours
          }
        };
        departmentNode2.children.push(workerNode);
      }
    })
    root.children.push(departmentNode2);
    return <TreeNode>(root);
  }

  /**
   * function
   * @param worker to integrate his actual hours
   */
  getCountHours(worker: DetailsWorkerInProjects) {
    let count = 0;
    if (worker.ActualHours)
      worker.ActualHours.forEach(actualHours => { let st: string = String(actualHours.Hours); count += actualHours.Hours; })
    return count;
  }

  getActualHoursForDepartment(treeTable: TreeTable, department: string) {
    let count = 0;
    if (treeTable.DetailsWorkerInProjects.length > 0)
      treeTable.DetailsWorkerInProjects.forEach(worker => {
        if (worker.Status == department && worker.ActualHours)
          worker.ActualHours.forEach(ah => count += ah.Hours)
      });
    return count;
  }

  getActualHoursForProject(treeTable: TreeTable) {
    let count = 0;
    if (treeTable.DetailsWorkerInProjects.length > 0)
      treeTable.DetailsWorkerInProjects.forEach(worker => {
        if (worker.ActualHours)
          worker.ActualHours.forEach(ah => count += ah.Hours)
      });
    return count;
  }

  getPrecentOfNumbers(num1: number, num2: any) {
    if (isNaN(num2)) {
      var actualHours = num2.split(":");
      num2 = parseInt(actualHours[0]) + (parseInt(actualHours[1]) / 100);
    }
    let result = (num2 / num1) * 100 + '%';
    result = parseFloat(result).toFixed(3);
    return result;
  }

  //-----------------GETTERS--------------------

  //getters of the form group controls

  get teamLeaderId() {
    return this.reportFormGroup.controls["teamLeaderId"];
  }

  get userId() {
    return this.reportFormGroup.controls["userId"];
  }

  get projectId() {
    return this.reportFormGroup.controls["projectId"];
  }
}