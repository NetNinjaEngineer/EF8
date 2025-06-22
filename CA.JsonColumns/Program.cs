using CA.JsonColumns.Data;

namespace CA.JsonColumns;

internal class Program
{
    private static async Task Main()
    {
        await using (var context = new BlogsDbContext())
        {
            await context.Database.EnsureCreatedAsync();
            await context.Seed();

        }

        Console.ReadKey();
    }
}
