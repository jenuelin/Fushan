import { Component, OnInit, Renderer2, OnDestroy } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { AppService } from 'src/app/utils/services/app.service';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '@app/utils/services/account.service';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';
import { User } from '@shared/_models';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit, OnDestroy {
  public registerForm: FormGroup;
  public submitted = false;
  public goRegister = false;
  constructor(
    private renderer: Renderer2,
    private toastr: ToastrService,
    private appService: AppService,
    private accService: AccountService,
    private router: Router,
    private user: User,
  ) {}

  ngOnInit() {
    this.renderer.addClass(document.querySelector('app'), 'register-page');
    this.registerForm = new FormGroup({
      username: new FormControl(this.user.username, Validators.required),
      email: new FormControl(this.user.email, [Validators.required, Validators.email]),
      password: new FormControl(this.user.password, Validators.required),
      retypePassword: new FormControl(this.user.retypePassword, [Validators.required]),
      terms: new FormControl(this.user.terms, (terms: AbstractControl) => { return terms.value ? null : { 'err': true }; }),
    }, (form: AbstractControl) => { return form.get('password').value == form.get('retypePassword').value ? null : { equals: true }; });
  }

  get username() { return this.registerForm.get('username'); }
  get email() { return this.registerForm.get('email'); }
  get password() { return this.registerForm.get('password'); }
  get retypePassword() { return this.registerForm.get('retypePassword'); }
  get terms() { return this.registerForm.get('terms'); }

  register() {
    this.submitted = true;
    if (this.registerForm.valid) {
      this.goRegister = true;
      this.accService.register(this.registerForm.value)
        .pipe(first())
        .subscribe((data) => {
          console.log(data);
          this.router.navigate(['login']);
        }, error => {
            this.goRegister = false;
            this.toastr.error(error.DuplicateUserName, "錯誤");
        });
    } else {
      //this.toastr.error('Hello world!', 'Toastr fun!');
    }
  }

  ngOnDestroy() {
    this.renderer.removeClass(
      document.querySelector('app'),
      'register-page'
    );
  }
}
