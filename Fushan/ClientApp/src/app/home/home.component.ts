import { Component } from '@angular/core';

import { User } from '@shared/_models';
import { AccountService } from '@app/utils/services/account.service';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    user: User;

    constructor(private accountService: AccountService) {
        this.user = this.accountService.userValue;
    }
}
