namespace SE407_BookStore.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerFirst { get; set; }
        public string CustomerLast { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
