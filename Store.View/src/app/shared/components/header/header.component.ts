import { Component, OnInit } from '@angular/core';

import { AccountService } from 'src/app/shared/services/account.service';
import { TokenModel, UserModelItem } from 'src/app/shared/models';
import { CartService } from 'src/app/shared/services';
import { RoleName } from 'src/app/shared/enums';

import { MatDialog } from '@angular/material';
import { CartComponent } from 'src/app/modules/order-manager';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    private isAuthorized: boolean;
    private isAdmin: boolean;
    private badge: number;
    private userModel: UserModelItem;

    constructor(
        private accountService: AccountService,
        private dialog: MatDialog,
        private cartService: CartService
    ) {
    }

    public ngOnInit(): void {
        this.accountService.getTokens().subscribe((tokenModel: TokenModel) => {
            this.isAuthorized = tokenModel.refreshToken != null;
        });

        this.accountService.getProfile().subscribe((resultModel: UserModelItem) => {
            if (resultModel.role) {
                this.isAdmin = RoleName[resultModel.role] === RoleName.Admin;
                this.userModel = resultModel;
            }
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
    }
}
