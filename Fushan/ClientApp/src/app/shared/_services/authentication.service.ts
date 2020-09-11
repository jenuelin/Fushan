import { Injectable, OnInit } from '@angular/core';
import decode from 'jwt-decode';
import { AccountService } from '@utils/services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements OnInit {
  user
  tokenPayload
  constructor(private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.user = this.accountService.userValue
  }
  //set user(user) {
  //  this.user = user;
  //  this.tokenPayload = decode(this.user.token);
  //}

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
