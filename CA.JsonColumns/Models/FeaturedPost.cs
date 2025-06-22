namespace CA.JsonColumns.Models;

public class FeaturedPost : Post
{
    private FeaturedPost() { }
    private FeaturedPost(string title, string content, DateTime publishedOn, string promoText)
        : base(title, content, publishedOn)
    {
        PromoText = promoText;
    }

    public string PromoText { get; private set; } = null!;

    public static FeaturedPost Create(string title, string content, DateTime publishedOn, string promoText)
    {
        return new FeaturedPost(title, content, publishedOn, promoText);
    }
}