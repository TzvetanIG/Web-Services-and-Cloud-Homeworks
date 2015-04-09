namespace MusicSystemConsoleClient.Data
{

    public class Resourses : BaseResourse
    {
        public ArtistResourse Artists
        {
            get
            {
                return new ArtistResourse();
             }
        }

        public IResourse Songs
        {
            get
            {
                this.Resourse = "songs";
                return this;               
            }
        }
    }
}
