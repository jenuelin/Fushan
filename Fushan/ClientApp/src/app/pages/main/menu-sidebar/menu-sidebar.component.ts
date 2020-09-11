import {
  Component,
  OnInit,
  AfterViewInit,
  ViewChild,
  Output,
  EventEmitter,
} from '@angular/core';
import { Login } from '@shared/_models';
import { AccountService } from '@utils/services/account.service';
import { AuthenticationService } from '@shared/_services';

@Component({
  selector: 'app-menu-sidebar',
  templateUrl: './menu-sidebar.component.html',
  styleUrls: ['./menu-sidebar.component.scss'],
})
export class MenuSidebarComponent implements OnInit, AfterViewInit {
  @ViewChild('mainSidebar', { static: false }) mainSidebar;
  @Output() mainSidebarHeight: EventEmitter<any> = new EventEmitter<any>();

  public user: Login;
  constructor(public accountService: AccountService, private authService: AuthenticationService) {
    this.user = this.accountService.userValue;
  }

  ngOnInit() { }

  ngAfterViewInit() {
    this.mainSidebarHeight.emit(this.mainSidebar.nativeElement.offsetHeight);
  }

  get isAdmin() {
    return this.authService.isAdmin;
  }
}
