using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace News.Test
{
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Transactions;
    using Data;
    using Models;

    [TestClass]
    public class RepositoriesTests
    {
        private TransactionScope tran;

        [TestInitialize]
        public void TestInit()
        {
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
        public void AddNews_WhenNewsItemIsValid_ShouldAddtoDB()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
            var news = new NewsItem
            {
                Title = "Title1",
                Content = "News content",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };

            //Act
            data.News.Add(news);
            data.SaveChanges();

            //Assert
            var newsFromDb = data.News.Find(news.Id);
            Assert.IsNotNull(newsFromDb, "News is not added to database");
            Assert.AreNotEqual(newsFromDb.Id, 0, "News has 0 for Id");
            Assert.AreEqual(news.Title, newsFromDb.Title, "Title is not corect");
            Assert.AreEqual(news.Content, newsFromDb.Content, "Content is not corect");
            Assert.AreEqual(news.PublishDate.ToString(), newsFromDb.PublishDate.ToString(), "Date is not corect");
        }

        [TestMethod]
        public void ListAllNews_WhenAddedTwoNews_ShouldTwoNewsInDB()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
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

            //Act
            var newsItems = data.News.All().ToList();

            Assert.AreEqual(2, newsItems.Count);
            Assert.AreEqual(news1.Title, newsItems[0].Title, "Title is not corect");
            Assert.AreEqual(news1.Content, newsItems[0].Content, "Content is not corect");
            Assert.AreEqual(news1.PublishDate.ToString(), newsItems[0].PublishDate.ToString(), "Date is not corect");
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddNews_WhenTitleIsNull_ShouldThrowExeption()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
            var news = new NewsItem
            {
                Content = "News content",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };

            //Act
            data.News.Add(news);
            data.SaveChanges();
        }


        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddNews_WhenTitleIsEmptyString_ShouldThrowExeption()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
            var news = new NewsItem
            {
                Title = "",
                Content = "News content",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };

            //Act
            data.News.Add(news);
            data.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddNews_WhenContentIsNull_ShouldThrowExeption()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
            var news = new NewsItem
            {
                Title = "Title",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };

            //Act
            data.News.Add(news);
            data.SaveChanges();
        }

        [TestMethod]
        public void DeleteNews_WhenNewsExist_ShouldDeletedFromDB()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
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

            var nembersBeforeDeleting = data.News.All().Count();

            //Act
            data.News.Delete(news1);
            data.SaveChanges();

            var news = data.News.Find(news1.Id);
            Assert.IsNull(news, "News is not deleted");
            var nembersAfterDeleting = data.News.All().Count();
            Assert.AreEqual(nembersBeforeDeleting - 1, nembersAfterDeleting, "Count of news is not changed");
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void DeleteNews_WhenNewsNotExist_ShouldDeletedFromDB()
        {
            //Arrange
            var context = new NewsDbContext();
            var data = new NewsData(context);
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

            var news3 = new NewsItem
            {
                Title = "news 1",
                Content = "News content",
                PublishDate = new DateTime(2000, 12, 1, 14, 01, 55)
            };

            data.News.Add(news1);
            data.News.Add(news2);
            data.SaveChanges();

            var nembersBeforeDeleting = data.News.All().Count();

            //Act
            data.News.Delete(news3);
            data.SaveChanges();
        }
    }
}
