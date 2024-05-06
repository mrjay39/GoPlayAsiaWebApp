using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.DTOs.Bingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.DTOs.Bingo.Outgoing
{
    public class BingoBetResultDTO : BaseResultDTO
    {
        public BingoBetDTO Bet { get; set; }
        public List<BingoBetDTO> Bets { get; set; }
        public string FixedBet { get; set; } = string.Empty;
        public string FixedWinnings { get; set; } = "0.00";

    }
}
