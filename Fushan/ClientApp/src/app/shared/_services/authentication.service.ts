import { Injectable, OnInit } from '@angular/core';
import decode from 'jwt-decode';
import { AccountService } from '@utils/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  tokenPayload
  constructor(private accountService: AccountService) {
  }

  set user(user) {
    this.user = user;
    this.tokenPayload = decode(this.user.token);
  }

  get isAuthenticated() {
    return !!this.user;
  }

  get role() {
    return this.tokenPayload.role;
  }

  get isAdmin() {
    return this.tokenPayload.role === "Admin";
  }
}
