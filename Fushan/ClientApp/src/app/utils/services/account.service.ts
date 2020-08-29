import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User } from '@app/_models';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    Authorization: 'my-auth-token'
  })
};

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;
  private headers: HttpHeaders = new HttpHeaders();

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
      let user = JSON.parse(localStorage.getItem('user'));
      this.userSubject = new BehaviorSubject<User>(user);
      this.user = this.userSubject.asObservable();
      httpOptions.headers =
        httpOptions.headers.set('Authorization', 'Bearer ' + user.token);
    }

    public get userValue(): User {
        return this.userSubject.value;
    }

    login(email, password) {
      return this.http.post<User>(`${environment.apiUrl}/api/auth/signin`, { email, password }, httpOptions)
            .pipe(map(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
              localStorage.setItem('user', JSON.stringify(user));
              httpOptions.headers =
                httpOptions.headers.set('Authorization', 'Bearer ' + user.token);
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
      return this.http.post(`${environment.apiUrl}/api/auth/signup`, user, httpOptions);
    }

  getAll(params) {
    httpOptions["params"] = params;
    return this.http.get<User[]>(`${environment.apiUrl}/api/user`, httpOptions);
    }

    getById(id: string) {
        return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    }

    update(id, params) {
        return this.http.put(`${environment.apiUrl}/users/${id}`, params)
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
        return this.http.delete(`${environment.apiUrl}/users/${id}`)
            .pipe(map(x => {
                // auto logout if the logged in user deleted their own record
                if (id == this.userValue.id) {
                    this.logout();
                }
                return x;
            }));
    }
}
