namespace News.Servicies.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Models;
    using News.Models;

    [RoutePrefix("api/News")]
    public class NewsController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var news = this.Data.News
                .All().OrderBy(n => n.PublishDate)
                .ToList();

            return this.Ok(news);
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] BindingNewsModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Enter news!");
            }

            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var newsItem = new NewsItem
            {
                Title = model.Title,
                Content = model.Content,
                PublishDate = DateTime.Now
            };

            this.Data.News.Add(newsItem);
            this.Data.SaveChanges();

            return this.Ok(newsItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody] BindingNewsModel model)
        {
            var newsItem = this.Data.News.All()
                    .FirstOrDefault(u => u.Id == id);

            if (newsItem == null)
            {
                return this.NotFound();
            }

            newsItem.Title = model.Title;
            newsItem.Content = model.Content;

            this.Data.News.Update(newsItem);
            this.Data.SaveChanges();

            return this.Ok(newsItem);
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult Put(int id)
        {
            var newsItem = this.Data.News.All()
                    .FirstOrDefault(u => u.Id == id);

            if (newsItem == null)
            {
                return this.NotFound();
            }

            this.Data.News.Delete(newsItem);
            this.Data.SaveChanges();

            return this.Ok(newsItem);
        }
    }
}
