import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConstantsService {
  readonly sex: Array<any> = [{ 0: '女' }, { 1: '男' }];
  readonly distLocation: string = 'MyApplication/';


  readonly userApi = { getAll: '/api/user', create: '/api/user', update: '/api/user/' };
  readonly departmentApi = { getAll: '/api/department', create: '/api/department', update: '/api/department/' };
  constructor() { }
}
