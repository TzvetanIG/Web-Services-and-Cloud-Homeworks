namespace MusicSystemWebServices.Controllers
{
    using System.Data.Entity;
    using System.Web.Http;
    using Data;
    using Migrations;

    public abstract class BaseApiController : ApiController
    {
        private MusicSystemData data;


        protected BaseApiController()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MusicSystemDbContext, Configuration>());
            var contex = new MusicSystemDbContext();
            this.data = new MusicSystemData(contex);
        }

        public MusicSystemData Data
        {
            get
            {
                return this.data;
            }
        }
    }
}
