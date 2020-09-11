import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse
} from '@angular/common/http';
import { NgxSpinnerService } from 'ngx-spinner';
import { catchError, tap } from 'rxjs/operators';

@Injectable()
export class ApiInterceptor implements HttpInterceptor {
  private count: number = 0;

  constructor(private spinner: NgxSpinnerService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.count++;

    if (this.count == 1) this.spinner.show();

    return next.handle(req).pipe(catchError((err: any) => {
      this.count--;
      return Observable.throw(err);
    })).pipe(tap(event => {
      if (event instanceof HttpResponse) {
        this.count--;
        if (this.count == 0) this.spinner.hide();
      }
    }));
  }
}
