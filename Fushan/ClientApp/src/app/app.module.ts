import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UsersModule } from '@app/views/user/users.module';

// used to create fake backend
import { fakeBackendProvider } from '@shared/_helpers';

import { AppRoutingModule } from './app-routing.module';
import { JwtInterceptor, ErrorInterceptor } from '@shared/_helpers';
import { AppComponent } from './app.component';
//import { AlertComponent } from './_components';
import { ConstantsService } from '@shared/_services';
import { SharedModule } from '@shared/shared.module';

//import { HomeComponent } from './home';

import { MainComponent } from './pages/main/main.component';
import { LoginComponent } from './pages/login/login.component';
import { HeaderComponent } from './pages/main/header/header.component';
import { FooterComponent } from './pages/main/footer/footer.component';
import { MenuSidebarComponent } from './pages/main/menu-sidebar/menu-sidebar.component';
import { BlankComponent } from './views/blank/blank.component';
import { ProfileComponent } from './views/profile/profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RegisterComponent } from './pages/register/register.component';
import { DashboardComponent } from './views/dashboard/dashboard.component';
import { ToastrModule } from 'ngx-toastr';
import { MessagesDropdownMenuComponent } from './pages/main/header/messages-dropdown-menu/messages-dropdown-menu.component';
import { NotificationsDropdownMenuComponent } from './pages/main/header/notifications-dropdown-menu/notifications-dropdown-menu.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppButtonComponent } from '@shared/_components/app-button/app-button.component';

import { registerLocaleData } from '@angular/common';
import localezhTw from '@angular/common/locales/zh-Hant';
import { UserDropdownMenuComponent } from './pages/main/header/user-dropdown-menu/user-dropdown-menu.component';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { BreadcrumbComponent } from '@shared/_components/breadcrumb/breadcrumb.component';
import { DepartmentModule } from './views/department/department.module';
import { Login, TableRequestBase } from '@shared/_models';
//import { NgxDatatableModule } from '@swimlane/ngx-datatable';

//import { PaginationComponent } from './_components/dataTable/pagination/pagination.component';
//import { DataTableComponent } from './_components/data-table/data-table.component';
//import { ItemComponent } from './_components/data-table/item/item.component';
//import { JwPaginationModule } from 'jw-angular-pagination';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { zhCnLocale } from 'ngx-bootstrap/locale';

defineLocale('zh-cn', zhCnLocale);
registerLocaleData(localezhTw, 'zh-tw');

@NgModule({
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    HttpClientModule,
    UsersModule,
    DepartmentModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    BreadcrumbModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
    }),
    NgbModule,
    SharedModule,
    BsDatepickerModule.forRoot()
    //NgxDatatableModule,
    //JwPaginationModule,
  ],
  declarations: [
    AppComponent,
    //AlertComponent,
    //HomeComponent,
    MainComponent,
    LoginComponent,
    HeaderComponent,
    FooterComponent,
    MenuSidebarComponent,
    BlankComponent,
    ProfileComponent,
    RegisterComponent,
    DashboardComponent,
    MessagesDropdownMenuComponent,
    NotificationsDropdownMenuComponent,
    AppButtonComponent,
    UserDropdownMenuComponent,
    BreadcrumbComponent,
    //PaginationComponent,
    //DataTableComponent,
    //ItemComponent,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },

    // provider used to create fake backend
    fakeBackendProvider,
    ConstantsService,
    TableRequestBase,
    Login,
  ],
  exports: [BsDatepickerModule],
  bootstrap: [AppComponent]
})
export class AppModule { };
