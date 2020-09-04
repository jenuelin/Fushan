import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, first } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User, Paging, UserTable, TableList } from '@shared/_models';
import * as _ from 'lodash';

@Injectable({ providedIn: 'root' })
export class AccountService {
  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  //private headers: HttpHeaders = new HttpHeaders();
  private httpOptions = {
    headers: new HttpHeaders().set('Content-Type', 'application/json',)
  };

  constructor(
    private router: Router,
    private http: HttpClient
  ) {
    let user = JSON.parse(localStorage.getItem('user'));
    this.userSubject = new BehaviorSubject<User>(user);
    this.user = this.userSubject.asObservable();
    //this.httpOptions = {
    //  headers: new HttpHeaders({
    //    'Content-Type': 'application/json',
    //    'Authorization': 'Bearer ' + _.get(user, 'token', '')
    //  })
    //};
    //this.httpOptions.headers.set('Authorization', 'Bearer ' + _.get(user,'token'));
  }

  public get userValue(): User {
    return this.userSubject.value;
  }

  login(email, password) {
    return this.http.post<User>('/api/auth/signin', { email, password }, this.httpOptions)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('user', JSON.stringify(user));
        this.httpOptions.headers = this.httpOptions.headers.set('Authorization', 'Bearer ' + _.get(user, 'token'));
        this.userSubject.next(user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage and set current user to null
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/login']);
  }

  register(user: User) {
    return this.http.post('/api/user', user, this.httpOptions);
  }

  getAll(paging: Paging) {
    this.httpOptions["params"] = paging;
    return this.http.get<TableList<UserTable>>(`${environment.apiUrl}/api/user`, this.httpOptions);
  }

  getById(id: string) {
    return this.http.get<User>(`/user/${id}`);
  }

  update(id, params) {
    return this.http.put(`/user/${id}`, params)
      .pipe(map(x => {
        // update stored user if the logged in user updated their own record
        if (id == this.userValue.id) {
          // update local storage
          const user = { ...this.userValue, ...params };
          localStorage.setItem('user', JSON.stringify(user));

          // publish updated user to subscribers
          this.userSubject.next(user);
        }
        return x;
      }));
  }

  delete(id: string) {
    return this.http.delete(`/user/${id}`)
      .pipe(map(x => {
        // auto logout if the logged in user deleted their own record
        if (id == this.userValue.id) {
          this.logout();
        }
        return x;
      }));
  }
}
