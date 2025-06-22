using CA.JsonColumns.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CA.JsonColumns.Data;

public class BlogsDbContext : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Author> Authors => Set<Author>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = "Server=.\\SQLEXPRESS;Database=CA.JsonColumns;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=true";

        optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.EnableRetryOnFailure();
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            })
            .LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Blog>()
             .HasMany(b => b.Posts)
             .WithOne(p => p.Blog)
             .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Author)
            .WithMany(a => a.Posts)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Post>()
            .OwnsOne(p => p.Metadata, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.OwnsMany(m => m.TopGeographies);
                ownedNavigationBuilder.OwnsMany(m => m.TopSearches);
                ownedNavigationBuilder.OwnsMany(m => m.Updates, b =>
                {
                    b.OwnsMany(u => u.Commits);
                });

            });

        modelBuilder.Entity<Author>()
            .OwnsOne(a => a.Contact, ownedNavigationBuilder =>
            {
                ownedNavigationBuilder.OwnsOne(contactDetails => contactDetails.Address);
            });

        modelBuilder.Entity<Tag>()
            .HasIndex(t => t.Text)
            .IsUnique();
    }

    public async Task Seed()
    {
        var tagEntityFramework = Tag.Create("TagEF", "Entity Framework");
        var tagDotNet = Tag.Create("TagNet", ".NET");
        var tagDotNetMaui = Tag.Create("TagMaui", ".NET MAUI");
        var tagAspDotNet = Tag.Create("TagAsp", "ASP.NET");
        var tagAspDotNetCore = Tag.Create("TagAspC", "ASP.NET Core");
        var tagDotNetCore = Tag.Create("TagC", ".NET Core");
        var tagHacking = Tag.Create("TagHx", "Hacking");
        var tagLinux = Tag.Create("TagLin", "Linux");
        var tagSqlite = Tag.Create("TagLite", "SQLite");
        var tagVisualStudio = Tag.Create("TagVS", "Visual Studio");
        var tagGraphQl = Tag.Create("TagQL", "GraphQL");
        var tagCosmosDb = Tag.Create("TagCos", "CosmosDB");
        var tagBlazor = Tag.Create("TagBl", "Blazor");

        var maddy = Author.Create("Maddy Montaquila",
            new ContactDetails() { Address = Address.Create("1 Main St", "Camberwick Green", "CW1 5ZH", "UK"), Phone = "01632 12345" });

        var jeremy = Author.Create("Jeremy Likness",
            new ContactDetails()
            { Address = Address.Create("2 Main St", "Chigley", "CW1 5ZH", "UK"), Phone = "01632 12346" });

        var dan = Author.Create("Daniel Roth",
            new ContactDetails()
            { Address = Address.Create("3 Main St", "Camberwick Green", "CW1 5ZH", "UK"), Phone = "01632 12347" });

        var arthur = Author.Create("Arthur Vickers",
            new ContactDetails
            { Address = Address.Create("15a Main St", "Chigley", "CW1 5ZH", "UK"), Phone = "01632 12348" });

        var brice = Author.Create("Brice Lambson",
            new ContactDetails() { Address = Address.Create("4 Main St", "Chigley", "CW1 5ZH", "UK"), Phone = "01632 12349" });

        var blogs = new List<Blog>
        {
                Blog.Create(".NET Blog")
                .WithPosts([
                    Post.Create(
                            "Productivity comes to .NET MAUI in Visual Studio 2022",
                            "Visual Studio 2022 17.3 is now available and...",
                            new DateTime(2022, 8, 9))
                        .WithTags([tagDotNetMaui, tagDotNet])
                        .WithAuthor(maddy)
                        .WithMetadata(BuildPostMetadata()),
                    Post.Create(
                            "Announcing .NET 7 Preview 7",
                            ".NET 7 Preview 7 is now available with improvements to System.LINQ, Unix...",
                            new DateTime(2022, 8, 9))
                        .WithTags([tagDotNet])
                        .WithAuthor(jeremy)
                        .WithMetadata(BuildPostMetadata()),
                    Post.Create(
                            "ASP.NET Core updates in .NET 7 Preview 7",
                            ".NET 7 Preview 7 is now available! Check out what's new in...",
                            new DateTime(2022, 8, 9))
                        .WithTags([tagDotNet, tagAspDotNet, tagAspDotNetCore])
                        .WithAuthor(dan)
                        .WithMetadata(BuildPostMetadata()),
                    FeaturedPost.Create(
                            "Announcing Entity Framework 7 Preview 7: Interceptors!",
                            "Announcing EF7 Preview 7 with new and improved interceptors, and...",
                            new DateTime(2022, 8, 9),
                            "Loads of runnable code!")
                        .WithTags([tagEntityFramework, tagDotNet, tagDotNetCore])
                        .WithAuthor(arthur)
                        .WithMetadata(BuildPostMetadata())
            ]),

        Blog.Create("1unicorn2")
            .WithPosts([
                Post.Create(
                        "Hacking my Sixth Form College network in 1991",
                        "Back in 1991 I was a student at Franklin Sixth Form College...",
                        new DateTime(2020, 4, 10))
                    .WithTags([tagHacking])
                    .WithAuthor(arthur)
                    .WithMetadata(BuildPostMetadata()),
                FeaturedPost.Create(
                        "All your versions are belong to us",
                        "Totally made up conversations about choosing Entity Framework version numbers...",
                        new DateTime(2020, 3, 26),
                        "Way funny!")
                    .WithTags([tagEntityFramework])
                    .WithAuthor(arthur)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "Moving to Linux",
                        "A few weeks ago, I decided to move from Windows to Linux as...",
                        new DateTime(2020, 3, 7))
                    .WithTags([tagLinux])
                    .WithAuthor(arthur)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "Welcome to One Unicorn 2.0!",
                        "I created my first blog back in 2011..",
                        new DateTime(2020, 2, 29))
                    .WithTags([tagEntityFramework])
                    .WithAuthor(arthur)
                    .WithMetadata(BuildPostMetadata())
            ]),

        Blog.Create("Brice's Blog")
            .WithPosts([
                FeaturedPost.Create(
                        "SQLite in Visual Studio 2022",
                        "A couple of years ago, I was thinking of ways...",
                        new DateTime(2022, 7, 26),
                        "Love for VS!")
                    .WithTags([tagSqlite, tagVisualStudio])
                    .WithAuthor(brice)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "On .NET - Entity Framework Migrations Explained",
                        "This week, @JamesMontemagno invited me onto the On .NET show...",
                        new DateTime(2022, 5, 4))
                    .WithTags([tagEntityFramework, tagDotNet])
                    .WithAuthor(brice)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "Dear DBA: A silly idea",
                        "We have fun on the Entity Framework team...",
                        new DateTime(2022, 3, 31))
                    .WithTags([tagEntityFramework])
                    .WithAuthor(brice)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "Microsoft.Data.Sqlite 6",
                        "It’s that time of year again. Microsoft.Data.Sqlite version...",
                        new DateTime(2021, 11, 8))
                    .WithTags([tagSqlite, tagDotNet])
                    .WithAuthor(brice)
                    .WithMetadata(BuildPostMetadata())
            ]),


        Blog.Create("Developer for Life")
            .WithPosts([
                Post.Create(
                        "GraphQL for .NET Developers",
                        "A comprehensive overview of GraphQL as...",
                        new DateTime(2021, 7, 1))
                    .WithTags([tagDotNet, tagGraphQl, tagAspDotNetCore])
                    .WithAuthor(jeremy)
                    .WithMetadata(BuildPostMetadata()),
                FeaturedPost.Create(
                        "Azure Cosmos DB With EF Core on Blazor Server",
                        "Learn how to build Azure Cosmos DB apps using Entity Framework Core...",
                        new DateTime(2021, 5, 16),
                        "Blazor FTW!")
                    .WithTags([ tagDotNet,
                        tagEntityFramework,
                        tagAspDotNetCore,
                        tagCosmosDb,
                        tagBlazor])
                    .WithAuthor(jeremy)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "Multi-tenancy with EF Core in Blazor Server Apps",
                        "Learn several ways to implement multi-tenant databases in Blazor Server apps...",
                        new DateTime(2021, 4, 29))
                    .WithTags([tagDotNet, tagEntityFramework, tagAspDotNetCore, tagBlazor])
                    .WithAuthor(jeremy)
                    .WithMetadata(BuildPostMetadata()),
                Post.Create(
                        "An Easier Blazor Debounce", "Where I propose a simple method to debounce input without...",
                        new DateTime(2021, 4, 12))
                    .WithTags([tagDotNet, tagAspDotNetCore, tagBlazor])
                    .WithAuthor(jeremy)
                    .WithMetadata(BuildPostMetadata())
            ])

        };

        await AddRangeAsync(blogs);
        await SaveChangesAsync();
        return;

        PostMetadata BuildPostMetadata()
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            var metadata = PostMetadata.Create(random.Next(10000));

            for (var i = 0; i < random.Next(5); i++)
            {
                var update = PostUpdate.Create(
                    DateTime.UtcNow - TimeSpan.FromDays(random.Next(1, 10000)),
                    "Admin",
                    IPAddress.Loopback);

                for (var j = 0; j < random.Next(3); j++)
                {
                    update.Commits.Add(Commit.Create(DateTime.Today, $"Commit #{j + 1}"));
                }

                metadata.Updates.Add(update);
            }

            for (var i = 0; i < random.Next(5); i++)
            {
                metadata.TopSearches.Add(SearchTerm.Create($"Search #{i + 1}", 10000 - random.Next(i * 1000, i * 1000 + 900)));
            }

            for (var i = 0; i < random.Next(5); i++)
            {
                metadata.TopGeographies.Add(
                    Visits.Create(
                        115.7930 + 20 - random.Next(40),
                        37.2431 + 10 - random.Next(20),
                        1000 - random.Next(i * 100, i * 100 + 90))
                        .WithBrowsers(["Firefox", "Netscape"]));
            }

            return metadata;
        }
    }
}