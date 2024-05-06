using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Models
{
    public class voucherCorpModel
    {
		public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int Months { get; set; }
        public int IsFirstTopup { get; set; }
    }
}
