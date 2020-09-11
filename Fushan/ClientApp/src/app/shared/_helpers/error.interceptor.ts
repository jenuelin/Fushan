import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

import { AccountService } from '@app/utils/services/account.service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(catchError(err => {
      const error = err.error || err.message || err.statusText;
      if (err.status === 401) {
        // auto logout if 401 response returned from api
        this.toastr.error(error, '發生錯誤!!');
        this.accountService.logout();
      }
      return throwError(error.message || error);
    }))
  }
}
