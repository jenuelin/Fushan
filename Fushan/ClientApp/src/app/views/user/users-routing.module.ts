import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

//import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { AddEditComponent } from './add-edit.component';
import { MainComponent } from '@app/pages/main/main.component';

const routes: Routes = [
    {
    path: 'users', component: MainComponent,
        children: [
            { path: '', component: ListComponent },
            { path: 'add', component: AddEditComponent },
            { path: 'edit/:id', component: AddEditComponent }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UsersRoutingModule { }
