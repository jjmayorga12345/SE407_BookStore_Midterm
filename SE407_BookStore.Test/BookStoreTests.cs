using SE407_BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace SE407_BookStore.Test
{
    public class BookStoreTests
    {
        private DbContextOptions<BookStoreContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<BookStoreContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDB")
                .Options;
        }

        [Fact]
        public void Test_GetBooksByTitle()
        {
            var options = CreateNewContextOptions();

            using (var context = new BookStoreContext(options))
            {
                context.Books.Add(new Book { BookTitle = "The Travels of Marco Polo", YearOfRelease = 1305 });
                context.Books.Add(new Book { BookTitle = "Canterbury Tales", YearOfRelease = 1410 });
                context.SaveChanges();
            }

            using (var context = new BookStoreContext(options))
            {
                var books = context.Books
                    .Where(b => b.BookTitle == "The Travels of Marco Polo")
                    .ToList();

                Assert.Single(books);
                Assert.Equal("The Travels of Marco Polo", books[0].BookTitle);
            }
        }

        [Fact]
        public void Test_GetBooksByAuthorLastName()
        {
            var options = CreateNewContextOptions();

            using (var context = new BookStoreContext(options))
            {
                var author = new Author { AuthorFirst = "Geoffrey", AuthorLast = "Chaucer" };
                context.Authors.Add(author);
                context.SaveChanges();

                context.Books.Add(new Book { BookTitle = "Canterbury Tales", YearOfRelease = 1410, AuthorID = author.AuthorID });
                context.SaveChanges();
            }

            using (var context = new BookStoreContext(options))
            {
                var books = context.Books
                    .Include(b => b.Author)
                    .Where(b => b.Author.AuthorLast == "Chaucer")
                    .ToList();

                Assert.Single(books);
                Assert.Equal("Canterbury Tales", books[0].BookTitle);
            }
        }

        [Fact]
        public void Test_GetAllBooks()
        {
            var options = CreateNewContextOptions();

            using (var context = new BookStoreContext(options))
            {
                context.Books.RemoveRange(context.Books);
                context.SaveChanges();

                context.Books.Add(new Book { BookTitle = "The Travels of Marco Polo", YearOfRelease = 1305 });
                context.Books.Add(new Book { BookTitle = "Canterbury Tales", YearOfRelease = 1410 });
                context.Books.Add(new Book { BookTitle = "Farid", YearOfRelease = 2000 });
                context.SaveChanges();
            }

            using (var context = new BookStoreContext(options))
            {
                var books = context.Books.ToList();
                Assert.Equal(3, books.Count);
            }
        }

    }
}
