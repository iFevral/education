import { Component } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { RoleName } from '../../enums';
import { TokenModel, UserModelItem } from '../../models';
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
    private userModel: UserModelItem;

    constructor(
        private accountService: AccountService,
        private dialog: MatDialog,
        private cartService: CartService
    ) {
        this.accountService.getTokens().subscribe((tokenModel: TokenModel) => {
            this.isAuthorized = tokenModel.refreshToken != null;
        });

        this.accountService.getProfile().subscribe((resultModel: UserModelItem) => {
            this.isAdmin = RoleName[resultModel.roles[0]] === RoleName.Admin;
            this.userModel = resultModel;
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
}
