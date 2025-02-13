namespace SE407_BookStore.Models
{
    public class Author
    {
        public int AuthorID { get; set; }
        public string AuthorFirst { get; set; }
        public string AuthorLast { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
