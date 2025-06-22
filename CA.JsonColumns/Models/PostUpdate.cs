using System.Net;

namespace CA.JsonColumns.Models;

public class PostUpdate
{
    private PostUpdate() { }
    private PostUpdate(DateTime updatedOn, string? updatedBy, IPAddress postedFrom)
    {
        UpdatedOn = updatedOn;
        UpdatedBy = updatedBy;
        PostedFrom = postedFrom;
    }

    public static PostUpdate Create(DateTime updatedOn, string? updatedBy, IPAddress postedFrom)
    {
        return new PostUpdate(updatedOn, updatedBy, postedFrom);
    }

    public IPAddress PostedFrom { get; private set; } = null!;
    public string? UpdatedBy { get; init; }
    public DateTime UpdatedOn { get; private set; }
    public List<Commit> Commits { get; } = [];
}