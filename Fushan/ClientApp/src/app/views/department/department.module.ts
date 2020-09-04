import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { DepartmentRoutingModule } from './department-routing.module';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './add-edit/add-edit.component';
import { SharedModule } from '@shared/shared.module';


@NgModule({
  declarations: [
    ListComponent,
    AddEditComponent,
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DepartmentRoutingModule,
    SharedModule
  ]
})
export class DepartmentModule { }
