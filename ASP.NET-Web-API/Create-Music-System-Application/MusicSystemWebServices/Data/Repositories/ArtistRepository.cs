using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MusicSystemWebServices.Models;

namespace MusicSystemWebServices.Data.Repositories
{
    public class ArtistRepository : GenericRepository<Artist>
    {
        public ArtistRepository(DbContext context) 
            : base(context)
        {

        }

        public IQueryable<ArtistBindingModel> GetAll()
        {
            return this
                .All()
                .Select(a => new ArtistBindingModel
                {
                    Name = a.Name,
                    DateOfBirth = a.DateOfBirth,
                    Country = a.Country
                });
        }

        public void AddArtist(string name, string country, DateTime dateOfBirth)
        {
            var artist = new Artist
            {
                Name = name,
                Country = country,
                DateOfBirth = dateOfBirth
            };

            this.Add(artist);
        }
    }
}