import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from '@app/pages/main/main.component';
import { AuthGuard } from '@utils/guards/auth.guard';
import { AddEditComponent } from './add-edit.component';
import { ListComponent } from './list.component';

const routes: Routes = [
  {
    path: 'roles', component: MainComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin"] },

    children: [
      { path: '', component: ListComponent },
      {
        path: 'add', component: AddEditComponent, resolve: {
          //departments: DepartmentsResolver
        }
      },
      {
        path: 'edit/:id', component: AddEditComponent, resolve: {
          //departments: DepartmentsResolver,
          //user: UserResolver
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RolesRoutingModule { }
