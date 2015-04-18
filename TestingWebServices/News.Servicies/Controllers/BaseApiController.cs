namespace News.Servicies.Controllers
{
    using System.Web.Http;
    using Data;

    public abstract class BaseApiController : ApiController
    {
        private INewsData data;

        protected BaseApiController()
        {
            var context = new NewsDbContext();
            this.data = new NewsData(context);
        }

        public INewsData Data
        {
            get { return this.data; }
        }
    }
}