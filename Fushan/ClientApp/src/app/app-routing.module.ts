import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//import { HomeComponent } from './home';
//import { AuthGuard } from './_helpers';
import { MainComponent } from './pages/main/main.component';
import { BlankComponent } from './views/blank/blank.component';
import { LoginComponent } from './pages/login/login.component';
import { ProfileComponent } from './views/profile/profile.component';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { AuthGuard } from './utils/guards/auth.guard';
import { NonAuthGuard } from './utils/guards/non-auth.guard';
//import { ListComponent } from './views/user/list.component';
//import { AddEditComponent } from './views/user/add-edit.component';

//const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const userModule = () => import('@app/views/user/users.module').then(x => x.UsersModule);
const departmentModule = () => import('@app/views/department/department.module').then(x => x.DepartmentModule);

//const routes: Routes = [
//    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
//    { path: 'users', loadChildren: usersModule, canActivate: [AuthGuard] },
//    { path: 'account', loadChildren: accountModule },

//    // otherwise redirect to home
//    { path: '**', redirectTo: '' }
//];

const routes: Routes = [
  {
    path: '', component: MainComponent, canActivate: [AuthGuard],
    canActivateChild: [AuthGuard],
    children: [
      {
        path: 'profile',
        component: ProfileComponent,
      },
      {
        path: 'blank',
        component: BlankComponent,
      },
      {
        path: 'users',
        loadChildren: userModule
      },
      {
        path: 'departments',
        loadChildren: departmentModule
      },
      //{
      //  path: 'users', canActivate: [AuthGuard],
      //  children: [
      //    { path: '', component: ListComponent, pathMatch: 'full' },
      //    { path: 'add', component: AddEditComponent },
      //    { path: 'edit/:id', component: AddEditComponent }
      //  ]
      //},
      {
        path: '',
        component: DashboardComponent,
      },
    ],
  },
  {
    path: 'login',
    component: LoginComponent,
    canActivate: [NonAuthGuard],
  },
  {
    path: 'register',
    component: RegisterComponent,
    canActivate: [NonAuthGuard],
  },
  { path: '**', redirectTo: '' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
