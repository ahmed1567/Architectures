namespace BookStore.Domain;

public class Order
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>(); // For concurrency
    public Guid CustomerId { get; private set; } // Added for customer association



    public Order(Guid bookId, int quantity,Guid customerId)
    {
        CustomerId = customerId;
        Id = new Guid();
        BookId = bookId;
        Quantity = quantity;
        PurchaseDate = DateTime.UtcNow;
        Validate();
    }

    private void Validate()
    {
        if (Quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");
    }
}