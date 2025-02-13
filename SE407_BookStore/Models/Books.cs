namespace SE407_BookStore.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public int GenreID { get; set; }
        public int AuthorID { get; set; }
        public short YearOfRelease { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
