import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-date-picker',
  templateUrl: './date-picker.component.html',
  styleUrls: ['./date-picker.component.css']
})
export class DatePickerComponent {

  //----------------PROPERTIES-------------------

  @Input()
  date: FormControl;

  @Input()
  placeholder: string;

  @Output()
  dateEvent: EventEmitter<Date>;

  @Input()
  minDate: Date;

  //allow access 'Object' type via interpolation
  objectHolder: typeof Object = Object;

  //----------------CONSTRUCTOR------------------

  constructor() {
    this.dateEvent = new EventEmitter<Date>();
    let date: Date = new Date();
    this.minDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }

}
