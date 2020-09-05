import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '@app/utils/services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PagingService } from '@shared/_services/paging.service';
import { ConstantsService } from '@shared/_services'

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
  form: any;
  users = null;
  items: any;
  pageOfItems: Array<any>;

  constructor(
    private accountService: AccountService,
    private constantsService: ConstantsService,
    private _pagingService: PagingService) { }

  ngOnInit() {
    this.form = {
      page: 1,
      rows: 2,
      sortBy: "createdOn",
      orderBy: "ASC",
      totalPages: 0,
      totalCount: 0
    };
    //this.form = this.formBuilder.group({
    //  page: [1, Validators.required],
    //  rows: [2, Validators.required],
    //  sortBy: ["createdOn", Validators.required],
    //  orderBy: ["ASC", Validators.required]
    //});

    this.getUsers(this.form, this.form['page']);
  }

  deleteUser(id: string) {
    const user = this.users.find(x => x.id === id);
    user.isDeleting = true;
    this.accountService.delete(id)
      .pipe(first())
      .subscribe(() => {
        this.users = this.users.filter(x => x.id !== id)
      });
  }

  getUsers(params: any, page: number) {
    return this.accountService.getAll(params, this.constantsService.userApi.getAll)
      //.pipe(table => {
      //  this.items = this.users = table.table;
      //})
      .subscribe(table => {
        this.items = this.users = table.table;
        this.form['totalPages'] = table.totalPages;
        this.form['totalCount'] = table.count;
        this._pagingService.changeNav(page);
      });
  }

  onChangePage(page: any) {
    this.form['page'] = page;
    this.getUsers(this.form, page);
    // update current page of items
    //this.pageOfItems = pageOfItems;
  }
}
