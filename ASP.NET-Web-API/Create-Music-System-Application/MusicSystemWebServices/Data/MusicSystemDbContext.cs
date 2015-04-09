using System.Data.Entity;
using MusicSystemWebServices.Models;

namespace MusicSystemWebServices.Data
{
    public class MusicSystemDbContext : DbContext
    {
        public MusicSystemDbContext() 
            : base("MusicSystem")
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
    }
}