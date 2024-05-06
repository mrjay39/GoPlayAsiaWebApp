using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs.DTOOut
{
    public class WithdrawRequestParamsDTO
    {
        public long UserId { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public int SelectionId { get; set; }
        public int SubSelectionId { get; set; }
        public string ChannelCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
    }
}
