using System;
using System.Collections.Generic;

#nullable disable

namespace GoplayasiaBlazor.Models
{
    public partial class CashinCashoutSettings
    {
        public int Id { get; set; }
        public int? RoleType { get; set; }
        public long? UserId { get; set; }
        public byte TransactionType { get; set; }
        public bool OTC { get; set; }
        public bool GoplayAsia { get; set; }
        public bool PaymentGateway { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
