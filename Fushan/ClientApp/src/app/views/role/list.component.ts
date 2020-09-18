import { Component, OnInit } from '@angular/core';
import { ConstantsService } from '@shared/_services'
import { RoleService } from '@app/utils/services/role.service'
import { RoleRequest, RoleTable } from '@shared/_models';

@Component({
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.less']
})
export class ListComponent implements OnInit {
  form: any;
  items: any;

  constructor(
    private constantsService: ConstantsService,
    private roleService: RoleService,) { }

  ngOnInit(): void {
    this.form = new RoleRequest();

    this.getRoles(this.form, this.form['page']);
  }

  getRoles(params: any, page: number) {
    this.items = null;
    return this.roleService.getAll<RoleTable>(params, this.constantsService.roleApi.getAll)
      //.pipe(table => {
      //  this.items = this.users = table.table;
      //})
      .subscribe(table => {
        this.items = table.table;
        this.form.totalPages = table.totalPages;
        this.form.totalCount = table.count;
      });
  }

  deleteDepartments(item: any) {
    item.isDeleting = true;
    this.roleService.delete(`${this.constantsService.departmentApi.delete}${item.id}`)
      .subscribe(() => {
        this.getRoles(this.form, 1)
      });
  }

  onChangePage(page: any) {
    this.form['page'] = page;
    this.getRoles(this.form, page);
  }

  search() {
    this.form.page = 1;
    this.getRoles(this.form, this.form.page);
  }
}
