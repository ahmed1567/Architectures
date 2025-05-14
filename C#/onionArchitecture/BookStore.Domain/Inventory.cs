namespace BookStore.Domain;

public class Inventory
{
    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public int Quantity { get; private set; }
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>(); // For concurrency

    public Inventory(Guid bookId, int quantity)
    {
        Id = new Guid();
        BookId = bookId;
        Quantity = quantity;
        Validate();
    }

    private void Validate()
    {

        if (Quantity < 0)
            throw new ArgumentException("Quantity cannot be negative.");
    }
}