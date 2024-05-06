
using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.Dtos.DTOOut;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class BetResultDTO : BaseResultDTO
    {
        public BetDTO Bet { get; set; }
        public string LeftFixedBet { get; set; }
        public string LeftFixedWinnings { get; set; }
        public string LeftOddsBet { get; set; }
        public string LeftOddsWinnings { get; set; }
        public string DrawBet { get; set; }
        public string DrawWinnings { get; set; }
        public string RightFixedBet { get; set; }
        public string RightFixedWinnings { get; set; }
        public string RightOddsBet { get; set; }
        public string RightOddsWinnings { get; set; }
        public List<Go12BetsDTO> Go12Bets { get; set; }
        public List<BetDTO> Bets { get; set; }
        public List<BetDTO> F2Bets { get; set; }
        public List<BetDTO> F3Bets { get; set; }
        public List<BetDTO> A4Bets { get; set; }
        public string F2Sum { get; set; }
        public string F3Sum { get; set; }
        public string A4Sum { get; set; }
    }

    public class Go12BetsDTO
    {
        public int Number { get; set; }
        public string Bet { get; set; }
        public string Winnings { get; set; }
    }
}
