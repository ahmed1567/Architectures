namespace BookStore.Domain;

public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public byte[] RowVersion { get; private set; } = Array.Empty<byte>(); // For concurrency

    public Customer(string name, string email)
    {
        Id = new Guid();
        Name = name;
        Email = email;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Name cannot be empty.");
        if (string.IsNullOrWhiteSpace(Email))
            throw new ArgumentException("Email cannot be empty.");
        if (!Email.Contains("@"))
            throw new ArgumentException("Invalid email format.");
    }
}