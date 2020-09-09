import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertService, ConstantsService } from '@shared/_services';
import { first } from 'rxjs/operators';
import { DepartmentService } from '@utils/services/department.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.less']
})
export class AddEditComponent implements OnInit {
  form: FormGroup;
  id: string;
  isAddMode: boolean;
  loading = false;
  submitted = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private departmentService: DepartmentService,
    private alertService: AlertService,
    private constantsService: ConstantsService) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;
    this.form = this.formBuilder.group({
      id: [''],
      departmentId: ['', Validators.required],
      name: ['', Validators.required],
      memo: [''],
    });

    if (!this.isAddMode) {
    }
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
      this.createDepartment();
    } else {
      this.updateDepartment();
    }
  }

  private createDepartment() {
    this.departmentService.create(this.form.value, this.constantsService.departmentApi.create)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Department added successfully', { keepAfterRouteChange: true });
          this.router.navigate(['.', { relativeTo: this.route }]);
        });
  }

  private updateDepartment() {
    this.departmentService.update(this.form.value, `${this.constantsService.departmentApi.update}${this.id}`)
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
