import { Component, OnInit } from '@angular/core';
import { ConstantsService } from '@shared/_services'
import { DepartmentService } from '@app/utils/services/department.service'
import { PagingService } from '@shared/_services/paging.service'
import { first } from 'rxjs/operators';
import { DepartmentTable } from '@shared/_models';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.less']
})
export class ListComponent implements OnInit {
  form: any;
  items: any;

  constructor(
    private constantsService: ConstantsService,
    private departmentService: DepartmentService,
    private _pagingService: PagingService) { }

  ngOnInit(): void {
    this.form = {
      page: 1,
      rows: 2,
      sortBy: "createdOn",
      orderBy: "ASC",
      totalPages: 0,
      totalCount: 0
    };

    this.getDepartments(this.form, this.form['page']);
  }

  getDepartments(params: any, page: number) {
    return this.departmentService.getAll<DepartmentTable>(params, this.constantsService.departmentApi.getAll)
      //.pipe(table => {
      //  this.items = this.users = table.table;
      //})
      .subscribe(table => {
        this.items = table.table;
        this.form['totalPages'] = table.totalPages;
        this.form['totalCount'] = table.count;
        this._pagingService.changeNav(page);
      });
  }

  deleteDepartments(item: any) {
    item.isDeleting = true;
    this.departmentService.delete(item.id)
      .pipe(first())
      .subscribe(() => {
        this.getDepartments(this.form, 1)
      });
  }

  onChangePage(page: any) {
    this.form['page'] = page;
    this.getDepartments(this.form, page);
  }
}
