import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { RoleName } from '../../enums';
import { TokenModel } from '../../models';
import { CartComponent } from '../../../modules/order';
import { MatDialog } from '@angular/material';
import { CartService } from '../../services';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
    private isAuthorized: boolean;
    private isAdmin: boolean;
    private badge: number;

    constructor(
        private accountService: AccountService,
        private dialog: MatDialog,
        private cartService: CartService
    ) {
        this.accountService.getTokens().subscribe((tokenModel: TokenModel) => {
            this.isAuthorized = tokenModel.refreshToken != null;
        });

        this.accountService.getRole().subscribe((role: RoleName) => {
            this.isAdmin = role === RoleName.Admin;
        });

        this.cartService.getProductsInCart().subscribe((resultModel) => {
            this.badge = resultModel.items.length;
        });
    }

    public signOut() {
        this.accountService.signOut();
    }

    public openCart(): void {
        const dialogRef = this.dialog.open(CartComponent, {
            width: '800px'
        });

        dialogRef.afterClosed()
            .subscribe((resultModel) => {
                console.log(resultModel);
            });
    }

    public log() {
        this.accountService.log();
    }
}
