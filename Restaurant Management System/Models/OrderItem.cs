namespace Restaurant_Management_System.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? SpecialNotes { get; set; }
        public List<int> SelectedAddOnIds { get; set; } = new();
    }
}