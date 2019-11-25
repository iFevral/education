namespace Store.BusinessLogic.Models.Orders
{
    public class OrderItemInputData
    {
        public int Id { get; set; }
        public int Amount { get; set; } = 1;

        public int? OrderId { get; set; }
        public int? PrintingEditionId { get; set; }
    }
}