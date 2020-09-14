import { Injectable, OnInit } from '@angular/core';
import decode from 'jwt-decode';
import { AccountService } from '@utils/services/account.service';
import * as _ from 'lodash';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  user
  tokenPayload
  constructor(private accountService: AccountService) {
    this.accountService.user.subscribe(x => {
      this.user = x
      if (this.user) {
        this.tokenPayload = decode(this.user.token);
      }
    });
  }

  get isAuthenticated() {
    return !!this.user;
  }

  get role() {
    return this.tokenPayload.role;
  }

  get isAdmin() {
    return _.includes(this.tokenPayload.roles,"Admin") ;
  }

  hasPermission(roles) {
    return _.every(roles, role => {
      return _.includes(this.tokenPayload.roles, role);
    });
  }
}
