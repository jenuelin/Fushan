import { Component, OnInit, OnChanges, Input, Output, EventEmitter, SimpleChanges } from '@angular/core';
import moment from 'moment';

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html',
  styleUrls: ['./datepicker.component.less']
})
export class DatepickerComponent implements OnInit {
  @Input() date = new Date(moment().toString());
  bsValue2: any;

  constructor() { }

  ngOnInit(): void {
    this.bsValue2 = new Date("2020/01/05");
  }
}
