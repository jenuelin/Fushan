import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AddEditComponent } from './add-edit.component';
import { MainComponent } from '@app/pages/main/main.component';
import { DepartmentsResolver, UserResolver } from '@utils/resolves'
import { AuthGuard } from '@utils/guards/auth.guard';

const routes: Routes = [
  {
    path: 'users', component: MainComponent,
    canActivate: [AuthGuard],
    data: { roles: ["Admin"] },

    children: [
      { path: '', component: ListComponent },
      {
        path: 'add', component: AddEditComponent, resolve: {
          departments: DepartmentsResolver
        }
      },
      {
        path: 'edit/:id', component: AddEditComponent, resolve: {
          departments: DepartmentsResolver,
          user: UserResolver
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
