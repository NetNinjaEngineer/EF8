using CA.JsonColumns.Data;
using Microsoft.EntityFrameworkCore;

namespace CA.JsonColumns;

internal class Program
{
    private static async Task Main()
    {
        await using (var context = new BlogsDbContext())
        {
            await context.Database.EnsureCreatedAsync();
            //await context.Seed();

            var posts = await context.Posts
                .AsNoTracking()
                .AsSplitQuery()
                .Select(p => new
                {
                    p.Id,
                    p.Title,
                    p.Content,
                    p.PublishedOn,
                    p.Author,
                    Tags = p.Tags.Select(t => t.Text).ToList(),
                    p.Metadata,
                    Blog = p.Blog.Name
                }).ToListAsync();


        }

        Console.ReadKey();
    }
}
