namespace BookStore.Domain;

public class Book
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public decimal Price { get; private set; }

    public Book(string title, string author, decimal price)
    {
        Id = new Guid();
        Title = title;
        Author = author;
        Price = price;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Title))
            throw new ArgumentException("Book title cannot be empty.");
        if (string.IsNullOrWhiteSpace(Author))
            throw new ArgumentException("Book author cannot be empty.");
        if (Price < 0)
            throw new ArgumentException("Book price cannot be negative.");
    }
}