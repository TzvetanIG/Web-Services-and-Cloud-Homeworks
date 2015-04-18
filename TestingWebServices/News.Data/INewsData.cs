namespace News.Data
{
    using Repositories;

    public interface INewsData
    {
       UsersRepository Users { get; }

       NewsRepository News { get; }

        int SaveChanges();
    }
}
