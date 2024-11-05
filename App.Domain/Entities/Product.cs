namespace App.Domain.Entities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Photo { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}