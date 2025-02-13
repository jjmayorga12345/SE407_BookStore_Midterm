namespace SE407_BookStore.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int BookID { get; set; }
        public int CustomerID { get; set; }
        public DateTime CheckedOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public string CheckedIn { get; set; }

        public Book Book { get; set; }
        public Customer Customer { get; set; }
    }
}
