import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '@app/_services';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
  users = null;
  items = [];
  pageOfItems: Array<any>;
  params = {
    page: 1,
    rows: 10,
    sortBy: "createdOn",
    orderBy: "ASC"
  };

    constructor(private accountService: AccountService) {}

  ngOnInit() {


    this.getUsers(this.params);
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

  getUsers(params: any) {
    return this.accountService.getAll(params)
      .pipe(first())
      .subscribe(users => this.items = this.users = users);
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }
}
