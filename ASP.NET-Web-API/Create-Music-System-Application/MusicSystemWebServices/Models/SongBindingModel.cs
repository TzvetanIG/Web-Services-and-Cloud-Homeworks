using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicSystemWebServices.Models
{
    public class SongBindingModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string ArtistName { get; set; }
        public string AlbumTitle { get; set; }
    }
}