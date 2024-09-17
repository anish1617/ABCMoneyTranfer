using System.ComponentModel.DataAnnotations;

namespace ABCMoneyTransfer.Models
{
    public class TransferViewModel
    {
        // Sender Information
        [Required]
        [Display(Name = "First Name")]
        public string SenderFirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? SenderMiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string SenderLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string SenderAddress { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string SenderCountry { get; set; }

        // Receiver Information
        [Required]
        [Display(Name = "First Name")]
        public string ReceiverFirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string? ReceiverMiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string ReceiverLastName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string ReceiverAddress { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string ReceiverCountry { get; set; }

        // Payment Details
        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [Display(Name = "Transfer Amount (MYR)")]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0")]
        public decimal TransferAmount { get; set; }

        [Display(Name = "Exchange Rate (MYR to NPR)")]
        [DataType(DataType.Currency)]
        public decimal ExchangeRate { get; set; }

        [Display(Name = "Payout Amount (NPR)")]
        [DataType(DataType.Currency)]
        public decimal PayoutAmount { get; set; }
    }
}
