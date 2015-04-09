namespace MusicSystemWebServices.Controllers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web.Http;
    using Models;
    using WebGrease.Css.Extensions;

    public class ArtistsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllArtists()
        {
            var artists = this.Data.Artists.GetAll().ToList();
            return this.Ok(artists);
        }

        [HttpPost]
        [ActionName("Songs")]
        public IHttpActionResult GetSongs([FromBody] ArtistBindingModel artist)
        {
            var artistObj = this.Data.Artists
                .All()
                .FirstOrDefault(a => a.Name == artist.Name);

            
            if (artistObj != null)
            {
                var songs = artistObj.Songs
                    .Select(s => new SongBindingModel
                    {
                        Title = s.Title,
                        Genre = s.Genre,
                        Year = s.Year
                    });

                return this.Ok(songs);
            }

            return this.NotFound();
        }

        [HttpGet]
        //[Route("api/artists/{id}")]
        public IHttpActionResult ArtistsById(int id)
        {
            var artist = this.Data.Artists.Find(id);

            if (artist == null)
            {
                return this.NotFound();
            }

            var artistResult = new ArtistBindingModel
            {
                Name = artist.Name,
                Country = artist.Country,
                DateOfBirth = artist.DateOfBirth
            };

            return this.Ok(artistResult);
        }


        [HttpPost]
        [ActionName("add")]
        public IHttpActionResult AddArtist([FromBody]ArtistBindingModel artist)
        {
            this.Data.Artists.AddArtist(artist.Name, artist.Country, (DateTime)artist.DateOfBirth);
            this.Data.SaveChanges();

            return this.Ok();
        }


        [HttpDelete]
        [ActionName("delete")]
        public IHttpActionResult DeleteArtist(int id)
        {
            var artist = this.Data.Artists.Delete(id);

            if (artist == null)
            {
                return this.NotFound();
            }

            this.Data.SaveChanges();

            return this.Ok("Deleted sucssefully");
        }

        [HttpPut]
        [ActionName("update")]
        public IHttpActionResult UpdateArtist(int id, [FromBody]ArtistBindingModel artist)
        {
            var updateArtist = this.Data.Artists.Find(id);

            try
            {
                if (artist.Name != null)
                {
                    updateArtist.Name = artist.Name;
                }

                if (artist.Country != null)
                {
                    updateArtist.Country = artist.Country;
                }

                if (artist.DateOfBirth != null)
                {
                    updateArtist.DateOfBirth = (DateTime)artist.DateOfBirth;
                }

                this.Data.Artists.Update(updateArtist);
                this.Data.SaveChanges();
            }
            catch (NullReferenceException)
            {
                return this.NotFound();
            }

            return this.Ok();
        }

        [HttpPost]
        [ActionName("AddSongs")]
        public IHttpActionResult AddSongs([FromBody]ArtistBindingModel artist)
        {
            var artistObj = this.Data.Artists.All().FirstOrDefault(a => a.Name == artist.Name);
            if (artistObj == null)
            {
                return this.NotFound();
            }

            artist.Songs.ForEach(s =>
            {
                var song = this.Data.Songs.Find(s);
                if (song != null)
                {
                    artistObj.Songs.Add(song);
                }
            });

            this.Data.Artists.SaveChanges();
            return this.Ok();
        }

    }
}
