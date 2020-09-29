import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from '@app/pages/main/main.component';
import { ListComponent } from './list/list.component';
import { AddEditComponent } from './add-edit/add-edit.component';
import { AuthGuard } from '@utils/guards/auth.guard';
import { DepartmentResolver, DepartmentsResolver } from '@utils/resolves';

const routes: Routes = [
  {
    path: 'departments', component: MainComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin"] },
    children: [
      { path: '', component: ListComponent },
      {
        path: 'add', component: AddEditComponent, resolve: {
          departments: DepartmentsResolver,
        } },
      {
        path: 'edit/:id', component: AddEditComponent, resolve: {
          departments: DepartmentsResolver,
          department: DepartmentResolver
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartmentRoutingModule { }
