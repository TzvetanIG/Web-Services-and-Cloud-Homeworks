namespace MusicSystemWebServices.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Models;

    public class SongsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult GetAllSongs()
        {
            var song = this.Data.Songs.GetAll().ToList();
            return this.Ok(song);
        }

        [HttpPost]
        [ActionName("add")]
        public IHttpActionResult AddSong([FromBody]SongBindingModel song)
        {
            this.Data.Songs.AddSong(song.Title, song.Genre, song.Year, song.ArtistName, song.AlbumTitle);
            this.Data.SaveChanges();
            return this.Ok();
        }
    }
}