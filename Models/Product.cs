namespace GroceryStore.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        // inne właściwości

        // Relacja wiele-do-wielu z zamówieniem
        public List<Order> Orders { get; set; }
    }

}
