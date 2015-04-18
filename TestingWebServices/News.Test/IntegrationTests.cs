namespace News.Test
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Transactions;
    using System.Web.Http;
    using Data;
    using EntityFramework.Extensions;
    using Microsoft.Owin.Testing;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Owin;
    using Servicies;

    [TestClass]
    public class IntegrationTests
    {

        private TestServer httpTestServer;
        private HttpClient httpClient;
        private TransactionScope tran;
        private NewsData data;

        [TestInitialize]
        public void TestInit()
        {
            // Start OWIN testing HTTP server with Web API support
            this.httpTestServer = TestServer.Create(appBuilder =>
            {
                var config = new HttpConfiguration();
                WebApiConfig.Register(config);
                appBuilder.UseWebApi(config);
            });

            this.httpClient = httpTestServer.HttpClient;
            // Start a new temporary transaction
            var context = new NewsDbContext();
            this.data = new NewsData(context);
            this.DeleteDatabase();
       }

        [TestCleanup]
        public void TestCleanup()
        {
            this.httpTestServer.Dispose();
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

            // Act
            var httpResponse = httpClient.GetAsync("/api/news").Result;
            var newsFromService = httpResponse.Content.ReadAsAsync<List<NewsItem>>().Result;

            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode, "Incorect status code");
            Assert.AreEqual(2, newsFromService.Count, "Incorect newsItems count");
        }

        [TestMethod]
        public void AddNewsItem_WhenDataIsCorect_ReturnAddedNewsItemAndStatusCode200Ok()
        {
            // Arrange
            var postContent = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("Title", "news 1"),
                 new KeyValuePair<string, string>("Content", "News content"),
            });

            // Act
            var httpResponse = httpClient.PostAsync("/api/news", postContent).Result;
            var newsItem = httpResponse.Content.ReadAsAsync<NewsItem>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, httpResponse.StatusCode);
        }

        [TestMethod]
        public void AddNewsItem_WhenDataIsIncorect_ReturnAddedNewsItemAndStatusCode401BadRequest()
        {
            // Arrange
            var postContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Content", "News content"), 
            });

            // Act
            var httpResponse = this.httpClient.PostAsync("api/news", postContent).Result;
 
            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, httpResponse.StatusCode);
        }

        [TestMethod]
        public void AddNewsItem_WhenDataIsNull_ReturnAddedNewsItemAndStatusCode401BadRequest()
        {
            // Arrange
            var postContent = new FormUrlEncodedContent( new List<KeyValuePair<string, string>>());

            // Act
            var httpResponse = this.httpClient.PostAsync("api/news", postContent).Result;
            var message = httpResponse.Content.ReadAsAsync<Dictionary<string, string>>().Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, httpResponse.StatusCode);
            Assert.AreEqual("Enter news!", message["Message"]);
        }

        private void DeleteDatabase()
        {
            var context = new NewsDbContext();
            context.NewsItems.Delete();
        }
    }
}
