
using GoplayasiaBlazor.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models.Incoming
{
    public class TransactionResultModel : BaseResultModel
    {
        public TransactionModel Transaction { get; set; }
    }
}
