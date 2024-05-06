using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class TransactionRequestsModel
    {
        public int SelectionId { get; set; }
        public int SubSelectionId { get; set; }
        public string Password { get; set; }
        public bool CreditRequest { get; set; }
        public bool WithdrawRequest { get; set; }
        public decimal Amount { get; set; }
    }
}
