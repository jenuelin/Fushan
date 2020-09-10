import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationComponent } from '@shared/_components/dataTable/pagination/pagination.component';
import { DatepickerComponent } from '@shared/_components/datepicker/datepicker.component'
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [PaginationComponent, DatepickerComponent],
  exports: [PaginationComponent, DatepickerComponent],
  imports: [
    CommonModule,
    BsDatepickerModule.forRoot()
  ]
})
export class SharedModule { }
