using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs.DTOOut
{
    public class WithdrawRequestParamsDtlDTO
    {
        public long? Id { get; set; }
        public int PaymentType { get; set; }
        public string AccountNumber { get; set; }
        public long? TransactionId { get; set; }
        public string? Remarks { get; set; }
    }
}
