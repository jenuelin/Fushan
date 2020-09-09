import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AlertService, ConstantsService } from '@shared/_services';
import { AccountService } from '@app/utils/services/account.service';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
  form: FormGroup;
  id: string;
  isAddMode: boolean;
  loading = false;
  submitted = false;
  location: Location;
  departments: any;
  employeeCategories: any;
  employmentStatus: any;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService,
    private constantsService: ConstantsService,
    location: Location
  ) {
    this.location = location;
  }

  ngOnInit() {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    // password not required in edit mode
    //const passwordValidators = [Validators.minLength(6)];
    //if (this.isAddMode) {
    //    passwordValidators.push(Validators.required);
    //}

    this.form = this.formBuilder.group({
      userID: ['', Validators.required],
      username: ['', Validators.required],
      email: ['', Validators.required],
      sex: ['0', Validators.required],
      department: [null, Validators.required],
      rank: [null, Validators.required],
      level: [null, Validators.required],
      employeeCategory: [null, Validators.required],
      employmentStatus: [null, Validators.required],
      onTheJobDay: ['', Validators.required],
      resignationDay: [''],
      phone: [''],
      workPhone: [''],
      birthday: [''],
      nationality: [''],
      memo: [''],
    });

    this.route.data.subscribe((data: { departments: any, user: any }) => {
      this.departments = data.departments.table;
      this.form.patchValue(data.user);
    });

    this.employeeCategories = this.constantsService.employeeCategory;
    this.employmentStatus = this.constantsService.employmentStatus;
    //if (this.location.getState().data) {
    //  this.form.patchValue(this.location.getState().data);
    //this.form.patchValue(this.location.getState());

    //this.accountService.getById(this.id)
    //    .pipe(first())
    //    .subscribe(x => {
    //        this.f.firstName.setValue(x.firstName);
    //        this.f.lastName.setValue(x.lastName);
    //      this.f.email.setValue(x.email);
    //    });
    //}
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createUser();
    } else {
      this.updateUser();
    }
  }

  private createUser() {
    this.accountService.register(this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('User added successfully', { keepAfterRouteChange: true });
          this.router.navigate(['.', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }

  private updateUser() {
    this.accountService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Update successful', { keepAfterRouteChange: true });
          this.router.navigate(['..', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
