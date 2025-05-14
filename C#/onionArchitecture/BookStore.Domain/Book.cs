namespace BookStore.Domain;

  public class Book
  {
      public Guid Id { get; private set; }
      public string Title { get; private set; }
      public string Author { get; private set; }
      public decimal Price { get; private set; }
      public byte[] RowVersion { get; private set; } = Array.Empty<byte>(); // For concurrency

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
              throw new ArgumentException("Title cannot be empty.");
          if (string.IsNullOrWhiteSpace(Author))
              throw new ArgumentException("Author cannot be empty.");
          if (Price < 0)
              throw new ArgumentException("Price cannot be negative.");
      }

      public void ApplyDiscount(decimal discountPercentage)
      {
          if (discountPercentage is < 0 or > 100)
              throw new ArgumentException("Discount percentage must be between 0 and 100.");
          Price -= Price * (discountPercentage / 100);
      }
  }