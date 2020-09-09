import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { DepartmentTable, TableRequestBase } from "@shared/_models";
import { DepartmentService } from "@utils/services/department.service";
import { ConstantsService } from "@shared/_services";

@Injectable({ providedIn: 'root' })
export class DepartmentsResolver implements Resolve<any> {
  constructor(private service: DepartmentService,
    private constantsService: ConstantsService,
    private paging: TableRequestBase) { }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {
    return this.service.getAll<DepartmentTable>(Object.assign({}, this.paging, { showAll: true }), this.constantsService.departmentApi.getAll);
  }
}
