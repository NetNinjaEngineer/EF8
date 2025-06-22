namespace CA.JsonColumns.Models;

public class Blog
{
    private Blog() { }

    private Blog(string name)
    {
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; set; } = null!;
    public List<Post> Posts { get; } = [];

    public static Blog Create(string name)
    {
        return new Blog(name);
    }
}