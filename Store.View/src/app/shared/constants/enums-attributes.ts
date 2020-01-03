import {
    SortProperty,
    OrderStatus,
    PrintingEditionType,
    PrintingEditionCurrency,
    UserLockStatus,
    CRUDOperations,
    PrintingEditionCurrencySymbol
} from 'src/app/shared/enums';

export class EnumsAttributes {
    public sortProperties: Array<string>;
    public orderStatuses: Array<string>;
    public printingEditionTypes: Array<string>;
    public printingEditionCurrencies: Array<string>;
    public userLockStatuses: Array<string>;
    public crudOperations: Array<string>;
    public printingEditionCurrenciesSymbols: Array<string>;

    constructor() {
        this.sortProperties = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in SortProperty) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.sortProperties.push(SortProperty[enumMember]);
            }
        }
        this.orderStatuses = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in OrderStatus) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.orderStatuses.push(OrderStatus[enumMember]);
            }
        }

        this.printingEditionTypes = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in PrintingEditionType) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.printingEditionTypes.push(PrintingEditionType[enumMember]);
            }
        }

        this.printingEditionCurrencies = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in PrintingEditionCurrency) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.printingEditionCurrencies.push(PrintingEditionCurrency[enumMember]);
            }
        }

        this.printingEditionCurrenciesSymbols = new Array<string>();
        // tslint:disable-next-line: forin
        // tslint:disable-next-line: forin
        for (const enumMember in PrintingEditionCurrencySymbol) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.printingEditionCurrenciesSymbols.push(PrintingEditionCurrencySymbol[enumMember]);
            }
        }

        this.userLockStatuses = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in UserLockStatus) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.userLockStatuses.push(UserLockStatus[enumMember]);
            }
        }

        this.crudOperations = new Array<string>();
        // tslint:disable-next-line: forin
        for (const enumMember in CRUDOperations) {
            const currencyIndex = parseInt(enumMember, 10);
            if (currencyIndex >= 0) {
                this.crudOperations.push(CRUDOperations[enumMember]);
            }
        }
    }
}
