import { Component } from '@angular/core';

import { Login } from '@shared/_models';
import { AccountService } from '@app/utils/services/account.service';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
  user: Login;

  constructor(private accountService: AccountService) {
    this.user = this.accountService.userValue;
  }
}
