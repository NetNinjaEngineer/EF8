namespace CA.JsonColumns.Models;

public class Tag
{
    private Tag() { }
    private Tag(string id, string text)
    {
        Id = id;
        Text = text;
    }

    public string Id { get; private set; } = null!;
    public string Text { get; private set; } = null!;
    public List<Post> Posts { get; } = [];

    public static Tag Create(string id, string text)
    {
        return new Tag(id, text);
    }
}