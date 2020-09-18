import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { DepartmentService } from "@utils/services/department.service";
import { ConstantsService } from "@shared/_services";
import { Department } from "@shared/_models/index";

@Injectable({ providedIn: 'root' })
export class DepartmentResolver implements Resolve<any> {
  constructor(private service: DepartmentService,
    private constantsService: ConstantsService) { }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {
    return this.service.get<Department>(`${this.constantsService.departmentApi.get}${route.paramMap.get('id')}`);
  }
}
