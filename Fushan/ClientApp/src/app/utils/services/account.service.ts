import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, first } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Login, Paging, UserTable, TableList, Registration, User } from '@shared/_models';
import * as _ from 'lodash';
import { BaseService } from './base.service';
import { ConstantsService } from '@shared/_services';

@Injectable({ providedIn: 'root' })
export class AccountService extends BaseService {
  private userSubject: BehaviorSubject<Login>;
  public user: Observable<Login>;
  //private headers: HttpHeaders = new HttpHeaders();
  //private httpOptions = {
  //  headers: new HttpHeaders().set('Content-Type', 'application/json',)
  //};

  constructor(
    private router: Router,
    http: HttpClient,
    private constantsService: ConstantsService
  ) {
    super(http);
    let user = JSON.parse(localStorage.getItem('user'));
    this.userSubject = new BehaviorSubject<Login>(user);
    this.user = this.userSubject.asObservable();
    //this.httpOptions = {
    //  headers: new HttpHeaders({
    //    'Content-Type': 'application/json',
    //    'Authorization': 'Bearer ' + _.get(user, 'token', '')
    //  })
    //};
    //this.httpOptions.headers.set('Authorization', 'Bearer ' + _.get(user,'token'));
  }

  public get userValue(): Login {
    return this.userSubject.value;
  }

  login(email, password) {
    return this.http.post<Login>(this.constantsService.authApi.login, { email, password }, this.httpOptions)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
        //super.httpOptions.headers = super.httpOptions.headers.set('Authorization', 'Bearer ' + _.get(user, 'token'));
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/login']);
    // remove user from local storage and set current user to null
  }

  register(user: Registration) {
    return this.http.post(this.constantsService.authApi.register, user, this.httpOptions);
  }

  //getAll(paging: Paging) {
  //  this.httpOptions["params"] = paging;
  //  return this.http.get<TableList<UserTable>>(`${environment.apiUrl}/api/user`, this.httpOptions);
  //}

  //getById(id: string) {
  //  return this.http.get<User>(`/user/${id}`);
  //}

  //update(id, params) {
  //  return this.http.put(`/user/${id}`, params)
  //    .pipe(map(x => {
  //      // update stored user if the logged in user updated their own record
  //      if (id == this.userValue.id) {
  //        // update local storage
  //        const user = { ...this.userValue, ...params };
  //        localStorage.setItem('user', JSON.stringify(user));

  //        // publish updated user to subscribers
  //        this.userSubject.next(user);
  //      }
  //      return x;
  //    }));
  //}

  //delete(id: string) {
  //  return this.http.delete(`/user/${id}`)
  //    .pipe(map(x => {
  //      // auto logout if the logged in user deleted their own record
  //      if (id == this.userValue.id) {
  //        this.logout();
  //      }
  //      return x;
  //    }));
  //}
}
