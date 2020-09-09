import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { DepartmentService } from "@utils/services/department.service";
import { ConstantsService } from "@shared/_services";
import { User } from "@shared/_models/user";

@Injectable({ providedIn: 'root' })
export class UserResolver implements Resolve<any> {
  constructor(private service: DepartmentService,
    private constantsService: ConstantsService) { }

  resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<any> | Promise<any> | any {
    return this.service.get<User>(`${this.constantsService.userApi.get}${route.paramMap.get('id')}`);
  }
}
