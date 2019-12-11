import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
    private isAuthorized: boolean;
    constructor(private accountService: AccountService) {
        this.accountService.tokenSubject.subscribe(data => {
            this.isAuthorized = data.refreshToken != null;
        });
    }

    public signOut() {
        this.accountService.signOut();
    }

    public log() {
        this.accountService.log();
    }
}
