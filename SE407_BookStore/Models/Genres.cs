namespace SE407_BookStore.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string GenreType { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
