using System.Collections.Generic;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class UBBanksDTO
    {
        public List<UBBanks> records { get; set; }
        public int totalRecords { get; set; }
    }
    public class UBBanks
    {
        public string code { get; set; }
        public string bank { get; set; }
        public string? brstn { get; set; }
    }
}
