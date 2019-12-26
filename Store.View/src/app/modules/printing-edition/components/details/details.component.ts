import { Component, OnInit } from '@angular/core';
import { PrintingEditionService, AccountService, CartService } from '../../../../shared/services';
import { ActivatedRoute } from '@angular/router';
import { PrintingEditionModelItem, CartModelItem, UserModelItem } from '../../../../shared/models';
import { Constants } from '../../../../shared/constants/constants';
import { RoleName } from '../../../../shared/enums';

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
        this.types = Constants.enumsAttributes.printingEditionTypes;

        this.accountService.getProfile().subscribe((resultModel: UserModelItem) => {
            this.isClient = RoleName[resultModel.role] === RoleName.Client;
        });
    }

    public ngOnInit() {
        const id = this.route.snapshot.paramMap.get('id');
        this.cartItem = new CartModelItem();
        this.cartItem.quantity = 1;

        this.printingEditionService.getById(parseInt(id, 10))
            .subscribe((resultModel: PrintingEditionModelItem) => {
                this.printingEditionModel = resultModel;
                this.cartItem.productId = resultModel.id;
                this.cartItem.productTitle = resultModel.title;
                this.cartItem.price = resultModel.price;
            });
    }

    public addProductToCart() {
        if (this.cartItem.quantity > 0) {
            this.cartService.add(this.cartItem);
        }
    }
}
