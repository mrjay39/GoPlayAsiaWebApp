using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.DTOs
{
    public class voucherActiveModel
    {
		public int Id { get; set; }
		public string Code { get; set; }
		public string Description { get; set; }
		public decimal Amount { get; set; }
		public decimal MinTopup { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsFirstTopup { get; set; }
		public int Count { get; set; }
		public int Used { get; set; }
	}
}
