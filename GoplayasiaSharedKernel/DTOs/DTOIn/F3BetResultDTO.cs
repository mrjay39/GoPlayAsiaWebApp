using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;

namespace GoplayasiaBlazor.DTOs.DTOIn
{
    public class F3BetResultDTO : BaseResultDTO
    {
        public BetDTO Bet { get; set; }
        public string LeftFixedBet { get; set; } = string.Empty;
        public string LeftFixedWinnings { get; set; } = "0.00";
        public string LeftOddsBet { get; set; } = string.Empty;
        public string LeftOddsWinnings { get; set; } = "0.00";
        public string DrawBet { get; set; } = string.Empty;
        public string DrawWinnings { get; set; } = "0.00";
        public string RightFixedBet { get; set; } = string.Empty;
        public string RightFixedWinnings { get; set; } = "0.00";
        public string RightOddsBet { get; set; } = string.Empty;
        public string RightOddsWinnings { get; set; } = "0.00";


        public string TargetLeftBet { get; set; } = string.Empty;
        public string TargetLeftBetWinnings { get; set; } = "0.00";
        public string TargetRightBet { get; set; } = string.Empty;
        public string TargetRightBetWinnings { get; set; } = "0.00";
        public string SuitsLeftBet { get; set; } = string.Empty;
        public string SuitsLeftBetWinnings { get; set; } = "0.00";
        public string SuitsRightBet { get; set; } = string.Empty;
        public string SuitsRightBetWinnings { get; set; } = "0.00";
        public string ColorRedLeftBet { get; set; } = string.Empty;
        public string ColorRedLeftBetWinnings { get; set; } = "0.00";
        public string ColorBlackLeftBet { get; set; } = string.Empty;
        public string ColorBlackLeftBetWinnings { get; set; } = "0.00";
        public string ColorRedRightBet { get; set; } = string.Empty;
        public string ColorRedRightBetWinnings { get; set; } = "0.00";
        public string ColorBlackRightBet { get; set; } = string.Empty;
        public string ColorBlackRightBetWinnings { get; set; } = "0.00";
        public string PairLeftBet { get; set; } = string.Empty;
        public string PairLeftBetWinnings { get; set; } = "0.00";
        public string PairRightBet { get; set; } = string.Empty;
        public string PairRightBetWinnings { get; set; } = "0.00";
        public string TrioBet { get; set; } = string.Empty;
        public string TrioBetWinnings { get; set; } = "0.00";


        public List<Go12BetsDTO> Go12Bets { get; set; }
    }
}
