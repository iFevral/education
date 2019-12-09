import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class HeaderComponent {
    private isAuthorized: boolean;
    constructor(private accountService: AccountService) {
        this.accountService.getTokens().subscribe(data => this.isAuthorized = data !== null);
    }

    public signOut() {
        this.accountService.logout();
    }

    public log() {
        this.accountService.log();
    }
}
