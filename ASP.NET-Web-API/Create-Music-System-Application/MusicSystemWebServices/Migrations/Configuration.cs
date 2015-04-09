using System;
using System.Linq;
using MusicSystemWebServices.Data;
using MusicSystemWebServices.Models;

namespace MusicSystemWebServices.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MusicSystemWebServices.Data.MusicSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MusicSystemWebServices.Data.MusicSystemDbContext context)
        {
            var db = new MusicSystemDbContext();
            if (!db.Artists.Any())
            {
                var mimi = new Artist
                {
                    Name = "Mimi Mimova",
                    Country = "Absurdia",
                    DateOfBirth = new DateTime(1988, 12, 1)
                };

                var song1 = new Song
                {
                    Title = "Song1",
                    Year = 2000,
                    Genre = "pop"
                };

                var song2 = new Song
                {
                    Title = "Song2",
                    Year = 2001,
                    Genre = "pop"
                };

                mimi.Songs.Add(song1);
                mimi.Songs.Add(song2);
                db.Artists.Add(mimi);

                db.SaveChanges();
            }   
        }
    }
}
