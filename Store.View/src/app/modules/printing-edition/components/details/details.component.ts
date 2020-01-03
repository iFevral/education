import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PrintingEditionService, AccountService, CartService } from 'src/app/shared/services';
import { PrintingEditionModelItem, CartModelItem, UserModelItem } from 'src/app/shared/models';
import { Constants } from 'src/app/shared/constants/constants';
import { RoleName } from 'src/app/shared/enums';

@Component({
    selector: 'app-details',
    templateUrl: './details.component.html',
    styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
    private printingEditionModel: PrintingEditionModelItem;
    private types: Array<string>;
    private cartItem: CartModelItem;
    private isClient: boolean;

    constructor(
        private route: ActivatedRoute,
        private printingEditionService: PrintingEditionService,
        private cartService: CartService,
        private accountService: AccountService
    ) {
        this.printingEditionModel = new PrintingEditionModelItem();
        this.types = Constants.enumsKeys.printingEditionTypes;

        this.cartItem = new CartModelItem();
        this.cartItem.quantity = 1;

    }

    public ngOnInit() {

        this.accountService.getProfile().subscribe((resultModel: UserModelItem) => {
            this.isClient = RoleName[resultModel.role] === RoleName.Client;
        });

        const id = this.route.snapshot.paramMap.get('id');

        this.printingEditionService.getById(parseInt(id, 10))
            .subscribe((resultModel: PrintingEditionModelItem) => {
                this.printingEditionModel = resultModel;
                this.cartItem.productId = resultModel.id;
                this.cartItem.productTitle = resultModel.title;
                this.cartItem.price = resultModel.price;
            });
    }

    public addProductToCart(): void {
        console.log("ta");
        if (this.cartItem.quantity > 0) {
            this.cartService.add(this.cartItem);
        }
    }
}
