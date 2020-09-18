import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConstantsService {
  readonly sex: Array<any> = [{ 0: '女' }, { 1: '男' }];
  readonly employeeCategory: Array<any> = [{ id: '0', name: '種類0' }, { id: '1', name: '種類1' }];
  readonly employmentStatus: Array<any> = [{ id: '0', name: 'Good' }, { id: '1', name: 'NoGood' }];

  readonly distLocation: string = 'MyApplication/';

  private readonly authDefaultApi = '/api/auth/';
  readonly authApi = {
    login: `${this.authDefaultApi}login`,
    logout: `${this.authDefaultApi}logout`,
    register: `${this.authDefaultApi}signUp`
  };

  private readonly userDefaultApi = '/api/user/';
  readonly userApi = {
    getAll: this.userDefaultApi,
    get: this.userDefaultApi,
    create: this.userDefaultApi,
    update: this.userDefaultApi,
    delete: this.userDefaultApi,
  };

  private readonly roleDefaultApi = '/api/role/';
  readonly roleApi = {
    getAll: this.roleDefaultApi,
    get: this.roleDefaultApi,
    create: this.roleDefaultApi,
    update: this.roleDefaultApi,
    delete: this.roleDefaultApi,
  };

  private readonly departmentDefaultApi = '/api/department/';
  readonly departmentApi = {
    getAll: this.departmentDefaultApi,
    get: this.departmentDefaultApi,
    create: this.departmentDefaultApi,
    update: this.departmentDefaultApi,
    delete: this.departmentDefaultApi,
  };
  constructor() { }
}
