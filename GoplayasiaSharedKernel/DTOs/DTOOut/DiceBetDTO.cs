using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOOut
{
    public class DiceBetDTO
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int GameVariantID { get; set; }
        public long GameRoundId { get; set; }
        public long UserId { get; set; }
        public int? BetStatus { get; set; }
        public decimal? BetAmount { get; set; }
        public string BetValue { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal? Winnings { get; set; }


        public DiceGameRoundDTO GameRound { get; set; }
    }
}
