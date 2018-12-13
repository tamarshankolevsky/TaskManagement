import { Component, OnInit, Input } from '@angular/core';
import * as CanvasJS from '../../../assets/js/canvasjs.min';
import { HoursService } from '../../shared/services/hours.service';

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  //----------------PROPERTIRS-------------------

  @Input()
  projects: any[];
  projectDeatails = new Array();
  projectDeatails2 = new Array();
  chart: any;

  //----------------CONSTRACTOR-------------------

  constructor(private hoursService: HoursService) {
   //update chart hours
    this.hoursService.subjectUpdateChart.subscribe(
      {
        next: (projects: any[]) => {
          this.projects = projects;
          this.createChart();
        }
      });
  }

  //-----------------METHODS--------------------

  ngOnInit() {
    this.createChart();
  }
 
  createChart() {
    this.projectDeatails=[];
    this.projectDeatails2=[];
    for (let index = 0; index < this.projects.length; index++) {
      var r = this.projects[index].Hours;
      this.projectDeatails.push({
        y: this.projects[index].allocatedHours, label: this.projects[index].Name
      })
      if(this.projects[index].Hours!=null)
      var t = this.projects[index].Hours.split(":");
      else
       t=[0,0];
      this.projectDeatails2.push({
        y: parseInt(t[0]) + (parseInt(t[1]) / 100), label: this.projects[index].Name
      })
    };
    this.chart = new CanvasJS.Chart("chartContainer", {
      animationEnabled: true,
      exportEnabled: true,
      title: {
        text: "project hours"
      },
      data: [{
        type: "column",
        dataPoints: this.projectDeatails,
        name: "allocated hours",
        showInLegend: true,
      },
      {
        type: "column",
        dataPoints: this.projectDeatails2,
        name: "worked hours",
        showInLegend: true,
      }],
    });
    this.chart.render();
  }

}
