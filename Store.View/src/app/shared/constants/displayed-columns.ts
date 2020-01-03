export class DisplayedColumns {
    public authors = [
        'name',
        'printingEdition',
        'control'
    ];

    public cart = [
        'title',
        'price',
        'quantity',
        'sum',
        'control'
    ];
    
    public orders = [
        'id',
        'date',
        'userName',
        'email',
        'productType',
        'title',
        'quantity',
        'orderPrice',
        'status'
    ];

    public ordersByUser = [
        'id',
        'date',
        'productType',
        'title',
        'quantity',
        'amount',
        'status'
    ];

    public printingEditions = [
        'title',
        'description',
        'type',
        'authors',
        'price',
        'control'
    ];

    public users = [
        'userName',
        'email',
        'status',
        'control'
    ];
}
