namespace MusicSystemWebServices.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Controllers;
    using Models;

    public class SongRepository : GenericRepository<Song>
    {
        public SongRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<SongBindingModel> GetAll()
        {
            return this
                .All()
                .Select(s => new SongBindingModel
                {
                    Title = s.Title,
                    Genre = s.Genre,
                    Year = s.Year,
                    ArtistName = s.Artist.Name,
                    AlbumTitle = s.Album.Title
                });
        }

        public void AddSong(string title, string genre, int year, string artistName, string albumName)
        {
            var song = new Song
            {
                Title = title,
                Genre = genre,
                Year = year,
            };

            this.Add(song);
        }

        public Song Find(string songTitle)
        {
            return this.All().FirstOrDefault(s => s.Title == songTitle);
        }
    }
}