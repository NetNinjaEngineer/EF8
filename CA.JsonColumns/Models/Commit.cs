namespace CA.JsonColumns.Models;

public class Commit
{
    public DateTime CommittedOn { get; private set; }
    public string Comment { get; set; } = null!;

    private Commit() { }

    private Commit(DateTime committedOn, string comment)
    {
        CommittedOn = committedOn;
        Comment = comment;
    }

    public static Commit Create(DateTime committedOn, string comment)
    {
        return new Commit(committedOn, comment);
    }


}