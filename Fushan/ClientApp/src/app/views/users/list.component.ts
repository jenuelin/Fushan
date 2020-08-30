import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '@app/_services';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
  form: FormGroup;
  users = null;
  items: any;
  pageOfItems: Array<any>;

  constructor(
    private accountService: AccountService,
    private formBuilder: FormBuilder) { }

  ngOnInit() {

    this.form = this.formBuilder.group({
      page: [1, Validators.required],
      rows: [10, Validators.required],
      sortBy: ["createdOn", Validators.required],
      orderBy: ["ASC", Validators.required]
    });

    this.getUsers(this.form.value);
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
    return this.accountService.getAll(this.form.value)
      .pipe(first())
      .subscribe(table => this.items = this.users = table.table);
  }

  onChangePage(pageOfItems: Array<any>) {
    // update current page of items
    this.pageOfItems = pageOfItems;
  }
}
