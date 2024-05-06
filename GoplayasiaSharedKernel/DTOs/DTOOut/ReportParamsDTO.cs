using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs.DTOOut
{
    public class ReportParamsDTO
    {
        public long? UserId { get; set; }
        public int? ApprovalStatus { get; set; }
        public int? TransactionType { get; set; }
        public string ReferenceNo { get; set; }
        public int? PaymentType { get; set; }
        public long? GameRoundId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public DateTime? Date { get; set; }
        public int? GameTypeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
    }
}
