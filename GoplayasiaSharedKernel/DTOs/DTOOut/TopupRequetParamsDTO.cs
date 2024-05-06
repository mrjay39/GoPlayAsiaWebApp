using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs.DTOOut
{
    public class TopupRequestParamsDTO
    {
        public long UserId { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public int SelectionId { get; set; }
        public List<voucherActiveModel>? Voucher { get; set; } // added by: cjpvaquilar
    }
}
