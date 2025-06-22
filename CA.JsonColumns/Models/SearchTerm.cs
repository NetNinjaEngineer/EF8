namespace CA.JsonColumns.Models;

public class SearchTerm
{
    public string Term { get; private set; } = null!;
    public int Count { get; private set; }

    private SearchTerm() { }

    private SearchTerm(string term, int count)
    {
        Term = term;
        Count = count;
    }

    public static SearchTerm Create(string term, int count)
    {
        if (string.IsNullOrWhiteSpace(term))
            throw new ArgumentException("Term cannot be null or whitespace.", nameof(term));
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Count cannot be negative.");
        return new SearchTerm(term, count);
    }
}