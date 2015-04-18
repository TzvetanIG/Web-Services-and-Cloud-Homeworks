namespace News.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class NewsDbContext : IdentityDbContext<User>
    {
        public NewsDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsDbContext, Configuration>());
        }

        public static NewsDbContext Create()
        {
            return new NewsDbContext();
        }

        public DbSet<NewsItem> NewsItems { get; set; }
    }
}
