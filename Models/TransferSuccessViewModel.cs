namespace ABCMoneyTransfer.Models
{
    public class TransferSuccessViewModel
    {
        public decimal TransferAmount { get; set; }
        public string? ReceiverName { get; set; }
        public decimal? PayoutAmount { get; set; }
    }
}
