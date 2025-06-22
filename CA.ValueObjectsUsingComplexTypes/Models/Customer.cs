namespace CA.ValueObjectsUsingComplexTypes.Models;

public sealed class Customer
{
    public Guid Id { get; set; }
    public string Name { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private Customer() { }

    private Customer(string name, Address address)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
    }

    public static Customer Create(string name, Address address)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty.", nameof(name));
        if (address == null) throw new ArgumentNullException(nameof(address), "Address cannot be null.");
        return new Customer(name, address);
    }

    public override string ToString()
        => $"{{ Id: {Id}, Name: {Name}, Address: {Address.ToString()} }}";
}