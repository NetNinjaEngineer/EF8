namespace CA.JsonColumns.Models;

public class PostMetadata
{
    public int Views { get; set; }
    public List<SearchTerm> TopSearches { get; } = [];
    public List<Visits> TopGeographies { get; } = [];
    public List<PostUpdate> Updates { get; } = [];

    private PostMetadata() { }
    private PostMetadata(int views)
    {
        Views = views;
    }

    public static PostMetadata Create(int views)
    {
        return new PostMetadata(views);
    }
}