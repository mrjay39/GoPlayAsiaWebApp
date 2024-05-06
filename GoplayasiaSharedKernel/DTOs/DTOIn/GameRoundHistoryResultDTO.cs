using GoplayasiaBlazor.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class GameRoundHistoryResultDTO : BaseResultDTO
    {
        public List<UserBetHistoryResultDTO> GameRounds { get; set; }
    }

    public class UserBetHistoryResultDTO
    {
        public int GameTypeId { get; set; }
        public string GameTypeName { get; set; }
        public string DateCreatedString { get; set; }
        public string DateCreatedTime { get; set; }
        public string DateCreatedFullString { get; set; }
        public int RoundStatus { get; set; }
        public string GameRound { get; set; }
        public string BetValue { get; set; }
        public string BetAmount { get; set; }
        public string BetValueAndAmount { get; set; }
        public string WinningResult { get; set; }
        public string GameResult { get; set; }
        public bool BetOnRound { get; set; }
        public bool Cancelled { get; set; }
        public bool Won { get; set; }
        public bool Lost { get; set; }
        public bool Consoled { get; set; }
        public bool Odds { get; set; }
        public bool BetReturned { get; set; }
        public string WonAmount { get; set; }
        public string LostAmount { get; set; }
        public string ConsoledAmount { get; set; }
        public string OddsPercentage { get; set; }
        public string JackpotPrize { get; set; }
    }
}
