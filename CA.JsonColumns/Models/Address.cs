namespace CA.JsonColumns.Models;

public class Address
{
    private Address() { }

    private Address(string street, string city, string postcode, string country)
    {
        Street = street;
        City = city;
        Postcode = postcode;
        Country = country;
    }

    public static Address Create(string street, string city, string postcode, string country)
    {
        return new Address(street, city, postcode, country);
    }

    public string Street { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Postcode { get; private set; } = null!;
    public string Country { get; private set; } = null!;
}