namespace News.Test
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Transactions;
    using System.Web.Http;
    using System.Web.Http.Routing;
    using Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Servicies.Controllers;
    using Servicies.Models;

    [TestClass]
    public class WebApiControllersTests
    {
        private TransactionScope tran;
        private NewsData data;
        private NewsController controller;

        [TestInitialize]
        public void TestInit()
        {
            var context = new NewsDbContext();
            data = new NewsData(context); 
            this.controller = new NewsController();
            // Start a new temporary transaction
            tran = new TransactionScope();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            // Rollback the temporary transaction
            tran.Dispose();
        }

        [TestMethod]
        public void ListAllNews_returnAllNewsAndCode200Ok()
        {
            // Arrange
            var news1 = new NewsItem
            {
                Title = "news 1",
                Content = "News content",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };

            var news2 = new NewsItem
            {
                Title = "news 2",
                Content = "News content2",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };
            data.News.Add(news1);
            data.News.Add(news2);
            data.SaveChanges();
            this.SetupController(controller, "news");
            
            // Act
            var result = controller.Get();
            var httpResponse = result.ExecuteAsync(new CancellationToken()).Result;
            var newsList = httpResponse.Content.ReadAsAsync<List<NewsItem>>().Result;

            // Assesr
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode, "Incorect status code");
            Assert.AreEqual(2, newsList.Count, "Incorect newsItems count");
        }

        [TestMethod]
        public void AddNewsItem_WhenDataIsCorect_ReturnAddedNewsItemAndStatusCode200Ok()
        {
            // Arrange
            var news = new BindingNewsModel
            {
                Title = "news 1",
                Content = "News content",
            };      

            this.SetupController(controller, "news");

            // Act
            var httpResponse = this.controller.Post(news).ExecuteAsync(new CancellationToken()).Result;
            var newsItem = httpResponse.Content.ReadAsAsync<NewsItem>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddNewsItem_WhenDataIsIncorect_ReturnAddedNewsItemAndStatusCode401BadRequest()
        {
            // Arrange
            var news = new BindingNewsModel
            {
                Content = "News content",
            };

            this.SetupController(controller, "news");

            // Act
            var httpResponse = this.controller.Post(news).ExecuteAsync(new CancellationToken()).Result;
            var newsItem = httpResponse.Content.ReadAsAsync<NewsItem>().Result;
        }

        [TestMethod]
        public void AddNewsItem_WhenDataIsNull_ReturnAddedNewsItemAndStatusCode401BadRequest()
        {
            // Arrange
            BindingNewsModel news = null;

            this.SetupController(controller, "news");

            // Act
            var httpResponse = this.controller.Post(news).ExecuteAsync(new CancellationToken()).Result;
            var message = httpResponse.Content.ReadAsAsync<Dictionary<string, string>>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.AreEqual("Enter news!", message["Message"]);
        }

        private void SetupController(ApiController controller, string controllerName)
        {
            string serverUrl = "http://sample-url.com";

            // Setup the Request object of the controller
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(serverUrl)
            };
            controller.Request = request;

            // Setup the configuration of the controller
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            controller.Configuration = config;

            // Apply the routes to the controller
            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary
                {
                    { "controller", controllerName }
                });
        }
    }
}
