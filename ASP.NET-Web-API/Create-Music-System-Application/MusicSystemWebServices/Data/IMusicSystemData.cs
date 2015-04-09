namespace MusicSystemWebServices.Data
{
    using Repositories;
    using Models;

    public interface IMusicSystemData
    {
        ArtistRepository Artists { get; }

        SongRepository Songs { get; }

        IRepository<Album> Albums { get; }

        int SaveChanges();
    }
}
