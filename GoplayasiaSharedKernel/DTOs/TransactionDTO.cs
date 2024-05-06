using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int PaymentType { get; set; }
        public int TransactionType { get; set; }
        public string ReferenceId { get; set; }
        public string TransactionId { get; set; } // added by: cjpvaquilar
        public decimal? Amount { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ConvertedDateCreated { get; set; }
        public string DateCreatedString { get; set; }
        public string TimeCreatedString { get; set; }
        public string DateCreatedFullString { get; set; }
        public string AmountDisplay { get; set; }
        public string DateApprovedFullString { get; set; }
        public int ApprovalStatus { get; set; }
        public string Status { get; set; }
        public string Payment { get; set; }
        public string Transaction { get; set; }

        public string TransactionUrl { get; set; }
        public string CallbackUrl { get; set; }

        public List<ApprovalDTO> Approvals { get; set; }
    }
}
