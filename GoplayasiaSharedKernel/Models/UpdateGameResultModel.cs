using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class UpdateGameResultModel
    {
        public int GameTypeId { get; set; }
        public long GameRoundId { get; set; }
        public string GameName { get; set; }
        public int RoundNo { get; set; }
        public string RoundNumber { get; set; }
        public string WinningCombination { get; set; }
        public string OptACard1 { get; set; }
        public string OptACard2 { get; set; }
        public string OptACard3 { get; set; }
        public string OptBCard1 { get; set; }
        public string OptBCard2 { get; set; }
        public string OptBCard3 { get; set; }
        public string ValACard1 { get; set; }
        public string ValACard2 { get; set; }
        public string ValACard3 { get; set; }
        public string ValBCard1 { get; set; }
        public string ValBCard2 { get; set; }
        public string ValBCard3 { get; set; }
        public string LeftWinningTarget { get; set; }
        public string RightWinningTarget { get; set; }
        public string LeftWinningSuits { get; set; }
        public string RightWinningSuits { get; set; }
        public string LeftWinningColor { get; set; }
        public string RightWinningColor { get; set; }
        public string LeftWinningPair { get; set; }
        public string RightWinningPair { get; set; }
        public string WinningResult { get; set; }
        public string TrioResult { get; set; }
    }
}
