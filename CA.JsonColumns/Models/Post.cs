namespace CA.JsonColumns.Models;

public class Post
{
    protected Post()
    {
    }

    protected Post(string title, string content, DateTime publishedOn)
    {
        Title = title;
        Content = content;
        PublishedOn = publishedOn;
    }

    public int Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;
    public DateTime PublishedOn { get; private set; }
    public Blog Blog { get; set; } = null!;
    public List<Tag> Tags { get; } = [];
    public Author? Author { get; private set; }
    public PostMetadata? Metadata { get; private set; }

    public static Post Create(string title, string content, DateTime publishedOn)
    {
        return new Post(title, content, publishedOn);
    }

    public Post WithAuthor(Author author)
    {
        Author = author ?? throw new ArgumentNullException(nameof(author));
        return this;
    }

    public Post WithMetadata(PostMetadata? metadata)
    {
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        return this;
    }


    public Post WithTags(List<Tag> tags)
    {
        if (tags == null) throw new ArgumentNullException(nameof(tags));
        Tags.Clear();
        Tags.AddRange(tags);
        return this;
    }

}