import {
    OrderStatus,
    PrintingEditionType,
    PrintingEditionCurrency,
    UserLockStatus
} from 'src/app/shared/enums';

export class EnumsValues {
    public orderStatuses = [
        OrderStatus.Paid,
        OrderStatus.Unpaid
    ];

    public printingEditionTypes = [
        PrintingEditionType.Books,
        PrintingEditionType.Magazines,
        PrintingEditionType.Newspapers
    ];

    public printingEditionCurrencies = [
        PrintingEditionCurrency.USD,
        PrintingEditionCurrency.EUR,
        PrintingEditionCurrency.GBP,
        PrintingEditionCurrency.CHF,
        PrintingEditionCurrency.JPY,
        PrintingEditionCurrency.UAH
    ];

    public userLockStatuses = [
        UserLockStatus.Active,
        UserLockStatus.Blocked
    ];
}