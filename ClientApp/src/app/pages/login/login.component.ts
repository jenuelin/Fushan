import { Component, OnInit, OnDestroy, Renderer2 } from '@angular/core';
import { AppService } from '../../utils/services/app.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../../utils/services/account.service';
import { first } from 'rxjs/operators';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit, OnDestroy {
  public loginForm: FormGroup;
  public isAuthLoading = false;
  returnUrl: string;
  constructor(
    private renderer: Renderer2,
    private toastr: ToastrService,
    private appService: AppService,
    private accService: AccountService,
    private router: Router,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.renderer.addClass(document.querySelector('app'), 'login-page');
    this.loginForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.email]),
      password: new FormControl(null, Validators.required),
    });
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  login() {
    if (this.loginForm.valid) {
      this.accService.login(this.loginForm.get('email').value, this.loginForm.get('password').value)
        .pipe(first())
        .subscribe(
          data => {
            this.router.navigate([this.returnUrl]);
          },
          error => {
            //this.alertService.error(error);
            //this.loading = false;
          });
    } else {
      this.toastr.error('錯誤訊息', '請輸入正確帳號密碼');
    }
  }

  ngOnDestroy() {
    this.renderer.removeClass(document.querySelector('app'), 'login-page');
  }
}
