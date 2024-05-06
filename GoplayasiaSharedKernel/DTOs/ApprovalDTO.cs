using GoplayasiaBlazor.Dtos.DTOIn;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs
{
    public class ApprovalDTO
    {
        public long Id { get; set; }
        public long RequestorId { get; set; }
        public long? ApproverId { get; set; }
        public long? TransactionId { get; set; }
        public decimal? RequestValue { get; set; }
        public int ApprovalStatus { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateApproved { get; set; }

        public UserDTO Requestor { get; set; }
        public TransactionDTO Transaction { get; set; }

        public DateTime? ConvertedDateCreated { get; set; }
        public DateTime? ConvertedDateApproved { get; set; }
        public string DateCreatedString { get; set; }
        public string DateCreatedTime { get; set; }
        public string DateCreatedFullString { get; set; }
        public string DateApprovedString { get; set; }
        public string DateApprovedTime { get; set; }
        public string DateApprovedFullString { get; set; }
        public string RequestorName { get; set; }
        public string ReferenceId { get; set; }

    }
}
