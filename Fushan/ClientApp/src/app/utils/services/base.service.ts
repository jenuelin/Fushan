import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Paging, TableList } from '@shared/_models';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {
  public httpOptions = {
    headers: new HttpHeaders().set('Content-Type', 'application/json',)
  };
  constructor(public http: HttpClient) { }

  getAll<T>(params: any, route: string) {
    this.httpOptions["params"] = params;
    return this.http.get<TableList<T>>(route, this.httpOptions);
  }

  get<T>(route: string) {
    return this.http.get<T>(route);
    //this.httpOptions["params"] = params;
    //return this.http.get<TableList<T>>(route, this.httpOptions);
  }

  create<T>(obj: T, route: string) {
    this.httpOptions["params"] = null;
    return this.http.post(route, obj, this.httpOptions);
  }

  update(params, route: string) {
    this.httpOptions["params"] = params;
    return this.http.put(route, params);
  }

  delete(route: string) {
    this.httpOptions["params"] = null;
    return this.http.delete(route);
  }
}
