using System;

namespace MusicSystemWebServices.Models
{
    public class ArtistBindingModel
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string[] Songs { get; set; }
    }
}