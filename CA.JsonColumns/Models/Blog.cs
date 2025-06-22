namespace CA.JsonColumns.Models;

public class Blog
{
    private readonly List<Post> _posts = [];
    private Blog() { }

    private Blog(string name)
    {
        Name = name;
    }

    public int Id { get; private set; }
    public string Name { get; set; } = null!;
    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    public static Blog Create(string name)
    {
        return new Blog(name);
    }

    public void AddPost(Post post)
    {
        if (post == null) throw new ArgumentNullException(nameof(post));
        _posts.Add(post);
    }

    public Blog WithPosts(List<Post> posts)
    {
        if (posts == null) throw new ArgumentNullException(nameof(posts));
        _posts.Clear();
        _posts.AddRange(posts);
        return this;
    }
}