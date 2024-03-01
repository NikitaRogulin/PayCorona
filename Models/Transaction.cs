namespace PayCorona.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ClientID { get; set; }
        public int RecipientId { get; set; }
        public DateTime DateTime { get; set; } 
        public decimal Sum { get; set; }
    }
}
