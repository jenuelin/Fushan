import {
  Component,
  OnInit,
  ViewChild,
  HostListener,
  ElementRef,
  Renderer2,
} from '@angular/core';
import { AppService } from 'src/app/utils/services/app.service';
import { AccountService } from '@utils/services/account.service';
import { Login } from '@shared/_models';

@Component({
  selector: 'app-user-dropdown-menu',
  templateUrl: './user-dropdown-menu.component.html',
  styleUrls: ['./user-dropdown-menu.component.scss'],
})
export class UserDropdownMenuComponent implements OnInit {
  public user: Login;

  @ViewChild('dropdownMenu', { static: false }) dropdownMenu;
  @HostListener('document:click', ['$event'])
  clickout(event) {
    if (!this.elementRef.nativeElement.contains(event.target)) {
      this.hideDropdownMenu();
    }
  }

  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2,
    private appService: AppService,
    private accountService: AccountService
  ) {
    this.user = this.accountService.userValue;
  }

  ngOnInit(): void {
    //this.user = this.appService.user;
  }

  toggleDropdownMenu() {
    if (this.dropdownMenu.nativeElement.classList.contains('show')) {
      this.hideDropdownMenu();
    } else {
      this.showDropdownMenu();
    }
  }

  showDropdownMenu() {
    if (this.dropdownMenu.nativeElement) {
      this.renderer.addClass(this.dropdownMenu.nativeElement, 'show');
    }
  }

  hideDropdownMenu() {
    if (this.dropdownMenu.nativeElement) {
      this.renderer.removeClass(this.dropdownMenu.nativeElement, 'show');
    }
  }

  logout() {
    this.accountService.logout();
  }
}
