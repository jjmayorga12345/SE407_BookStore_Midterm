using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SE407_BookStore.Models;
using System.Collections.Generic;
using SE407_BookStore.Console;


class Program
{
    static void Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<BookStoreContext>()
            .UseSqlServer("Server=sql.neit.edu,4500;Database=SE407_BookStore;User Id=SE407_BookStore;Password=B00k$t0r3;TrustServerCertificate=True;")
            .Options;

        using (var context = new BookStoreContext(options))
        {
            Console.WriteLine("Database connection successful!");

            if (args.Length == 0)
            {
                Console.WriteLine("\nUsage: [command] [parameter] [output=csv/console]");
                Console.WriteLine("Commands:");
                Console.WriteLine("getbytitle [book title] - Search for a book by title.");
                Console.WriteLine("getbyauthor [author last name] - Search for books by an author.");
                Console.WriteLine("getallbooks - Retrieve all books in the database.");
                return;
            }

            string command = args[0].ToLower();
            string parameter = args.Length > 1 ? args[1] : "";
            string outputFormat = args.Length > 1 && args.Last().Trim().ToLower() == "csv" ? "csv" : "console";

            Console.WriteLine($"DEBUG: Output format received = '{outputFormat}'");

            List<Book> books = new List<Book>();

            if (command == "getbytitle")
            {
                books = GetBookByTitle(context, parameter);
            }
            else if (command == "getbyauthor")
            {
                books = GetBooksByAuthorLastName(context, parameter);
            }
            else if (command == "getallbooks")
            {
                books = GetAllBooks(context);
            }
            else
            {
                Console.WriteLine("\nInvalid command. Use 'getbytitle', 'getbyauthor', or 'getallbooks'.");
                return;
            }

            if (books.Any())
            {
                if (outputFormat == "csv")
                {
                    Console.WriteLine("DEBUG: Exporting books to CSV...");
                    ConsoleUtils.WriteBooksToCsv(books);
                }
                else
                {
                    Console.WriteLine("\nBooks Found:");
                    foreach (var book in books)
                    {
                        Console.WriteLine($"{book.BookID}: {book.BookTitle} ({book.YearOfRelease})");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNo books found.");
            }
        }
    }

    static List<Book> GetBookByTitle(BookStoreContext context, string title)
    {
        return context.Books
            .Where(b => b.BookTitle == title)
            .ToList();
    }

    static List<Book> GetBooksByAuthorLastName(BookStoreContext context, string lastName)
    {
        return context.Books
            .Include(b => b.Author)
            .Where(b => b.Author.AuthorLast == lastName)
            .ToList();
    }

    static List<Book> GetAllBooks(BookStoreContext context)
    {
        return context.Books.ToList();
    }
}
