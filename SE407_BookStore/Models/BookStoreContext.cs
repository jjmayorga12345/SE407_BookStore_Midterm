using Microsoft.EntityFrameworkCore;

namespace SE407_BookStore.Models
{
    public class BookStoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public List<Book> GetBooksByTitle(string title)
        {
            return Books.Where(b => b.BookTitle.Contains(title)).ToList();
        }

        public List<Book> GetAllBooks()
        {
            return Books.ToList();
        }

        public List<Book> GetBooksByAuthorLastName(string lastName)
        {
            return Books.Include(b => b.Author)
                .Where(b => b.Author.AuthorLast.Contains(lastName))
                .ToList();
        }
    }
}
