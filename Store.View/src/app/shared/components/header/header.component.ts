import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { RoleName } from '../../enums';
import { TokenModel } from '../../models';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
    private isAuthorized: boolean;
    private isAdmin: boolean;
    constructor(private accountService: AccountService) {
        this.accountService.getTokens().subscribe((tokenModel: TokenModel) => {
            this.isAuthorized = tokenModel.refreshToken != null;
        });

        this.accountService.getRole().subscribe((role: RoleName) => {
            this.isAdmin = role === RoleName.Admin;
        });
    }

    public signOut() {
        this.accountService.signOut();
    }

    public log() {
        this.accountService.log();
    }
}
