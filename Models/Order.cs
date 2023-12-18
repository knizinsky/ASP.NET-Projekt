namespace GroceryStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        // inne właściwości

        // Relacja wiele-do-wielu z produktem
        public List<Product> Products { get; set; }
    }
}
