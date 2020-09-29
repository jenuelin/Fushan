import { Component } from '@angular/core';

import { AccountService } from './utils/services/account.service';
import { Login } from '@shared/_models';
import { deLocale } from 'ngx-bootstrap/locale';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { LanguageService } from './shared/_services';

@Component({
  selector: 'app',
  templateUrl: 'app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  user: Login;
  locale = 'zh-cn';

  constructor(
    private accountService: AccountService,
    private localeService: BsLocaleService,
    private languageService: LanguageService) {
    this.accountService.user.subscribe(x => this.user = x);
    this.localeService.use(this.locale);
    this.languageService.setInitState();
  }

  logout() {
    this.accountService.logout();
  }
}
