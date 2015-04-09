namespace MusicSystemConsoleClient.Data
{
    public class ArtistResourse : BaseResourse
    {
        public ArtistResourse()
        {
            this.Resourse = "artists";
        }

        public IRequestContentType AddSongs(object data)
        {
            return this.requester.Post(this.Resourse + "/addSongs", null, data);
        }

        public IRequestContentType GetSongs(object data)
        {
            return this.requester.Post(this.Resourse + "/songs", null, data);
        }
    }
}
