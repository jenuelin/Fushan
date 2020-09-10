import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
//import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AddEditComponent } from './add-edit.component';
//import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { SharedModule } from '@shared/shared.module';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
    UsersRoutingModule,
    SharedModule,
    FormsModule,
    BsDatepickerModule.forRoot(),
    //NgxDatatableModule
    ],
    declarations: [
        //LayoutComponent,
        ListComponent,
      AddEditComponent,
    ]
})
export class UsersModule { }
