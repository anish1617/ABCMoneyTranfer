using System.ComponentModel.DataAnnotations.Schema;

namespace ABCMoneyTransfer.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        [Column(TypeName = "money")]
        public decimal TransferAmount { get; set; }
        [Column(TypeName = "money")]
        public decimal ExchangeRate { get; set; }
        [Column(TypeName = "money")]
        public decimal PayoutAmount { get; set; }
        public DateTime TransactionDate { get; set; }

        public Person Sender { get; set; }
        public Person Receiver { get; set; }
    }
}
