using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOOut
{
    public class L9BetDTO
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int GameVariantID { get; set; } //new
        public long GameRoundId { get; set; }
        public long UserId { get; set; }
        public int? BetStatus { get; set; }
        public decimal? BetAmount { get; set; }
        public string BetValue { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool LuckyPick { get; set; }
        public bool LuckyPickX3 { get; set; }
        public bool LuckyPickX5 { get; set; }
        public bool LuckyPickX10 { get; set; }
        public int LuckyPickCharacters { get; set; }
        public L9GameRoundDTO GameRound { get; set; }
        public GameTypeDTO GameType { get; set; }
        public UserDTO User { get; set; }

        public DateTime? ConvertedDateCreated { get; set; }
        public string GameTypeName { get; set; }
        public string DrawDateString { get; set; }
        public string DrawDateTimeString { get; set; }
        public string RoundNumberDisplay { get; set; }
        public string Result { get; set; }
        public string GameResult { get; set; }
        public string GigaCombination { get; set; }
        public string DropAndWinWinnings { get; set; }
        public decimal? Winnings { get; set; }
    }
}
