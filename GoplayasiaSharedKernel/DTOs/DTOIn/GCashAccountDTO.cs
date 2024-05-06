using System.Collections.Generic;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class GCashAccountDTO
    {
        public int id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public bool IsActive { get; set; }
    }
}
