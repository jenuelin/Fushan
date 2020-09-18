import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RolesRoutingModule } from './roles-routing.module';
import { ListComponent } from './list.component';
import { AddEditComponent } from './add-edit.component';
import { SharedModule } from '@shared/shared.module';
import { FormsModule } from '@angular/forms';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

@NgModule({
  declarations: [ListComponent, AddEditComponent],
  imports: [
    CommonModule,
    RolesRoutingModule,
    SharedModule,
    FormsModule,
    BsDatepickerModule
  ]
})
export class RolesModule { }
