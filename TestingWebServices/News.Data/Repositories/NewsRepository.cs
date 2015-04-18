namespace News.Data.Repositories
{
    using System.Data.Entity;
    using Models;

    public class NewsRepository : GenericRepository<NewsItem>
    {
        public NewsRepository(DbContext context) : base(context)
        {
        }
    }
}
