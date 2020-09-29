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
import { MissingTranslationHandler, TranslateCompiler, TranslateLoader, TranslateModule, TranslateParser, TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { LanguageService } from '@shared/_services';
import { map } from 'rxjs/operators';

export function createUserTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, 'assets/i18n/User/', '.json');
}

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    UsersRoutingModule,
    SharedModule,
    FormsModule,
    BsDatepickerModule.forRoot(),
    //TranslateModule.forChild({
    //  loader: {
    //    provide: TranslateLoader,
    //    useFactory: (createUserTranslateLoader),
    //    deps: [HttpClient]
    //  },
    //  isolate: true,
    //  //extend: true
    //})
    //NgxDatatableModule
  ],
  declarations: [
    //LayoutComponent,
    ListComponent,
    AddEditComponent,
  ]
})
export class UsersModule {
  //language$ = this.languageService.language$;
  //constructor(
  //  private translateService: TranslateService,
  //  private languageService: LanguageService,
  //) {
  //  this.language$.pipe(map(language => language.lang)).subscribe(lang => this.translateService.use(lang));
  //}
}
