namespace CA.JsonColumns.Models;

public class Author
{
    private Author() { }
    private Author(string name)
    {
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = null!;
    public ContactDetails Contact { get; private set; } = null!;
    public List<Post> Posts { get; } = [];

    public static Author Create(string name, ContactDetails contact)
    {
        var author = new Author(name)
        {
            Contact = contact
        };

        return author;
    }


}