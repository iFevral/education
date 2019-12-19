import { CartModelItem } from './cart.model.item';

export class CartModel {
    constructor(
        public items?: Array<CartModelItem>
    ) { }
}
