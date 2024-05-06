using System;
using System.Collections.Generic;

#nullable disable

namespace GoplayasiaBlazor.Models.Bingo
{
    public partial class GameRoundBingo
    {
        public GameRoundBingo()
        {
            Bets = new HashSet<BingoBet>();
        }

        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int GameVariantID { get; set; }
        public int RoundNumber { get; set; }
        public int RoundStatus { get; set; }
        public decimal? RegularCorporationShareAsOf { get; set; }
        public decimal? RegularAgentShareAsOf { get; set; }
        public decimal? RegularMasterAgentShareAsOf { get; set; }
        public decimal? OddsCorporationShareAsOf { get; set; }
        public decimal? OddsAgentShareAsOf { get; set; }
        public decimal? OddsMasterAgentShareAsOf { get; set; }
        public decimal? ConsolationPrizeAsOf { get; set; }
        public decimal? TaxDelimiterAsOf { get; set; }
        public decimal? TaxPercentageAsOf { get; set; }
        public decimal? F2ratioAsOf { get; set; }
        public decimal? F3ratioAsOf { get; set; }
        public decimal? A4ratioAsOf { get; set; }
        public decimal? FixedMultiplierAsOf { get; set; }
        public decimal? DrawMultiplierAsOf { get; set; }
        public decimal? OddsPercentageAsOf { get; set; }
        public decimal? Less { get; set; }
        public decimal? LeftOdds { get; set; }
        public decimal? RightOdds { get; set; }
        public bool FixedCancelled { get; set; }
        public bool RunningOddsCancelled { get; set; }
        public int CurrentCardDraw { get; set; } = 0;
        public int Card1 { get; set; } = 0;
        public int Card2 { get; set; } = 0;
        public int Card3 { get; set; } = 0;
        public int Card4 { get; set; } = 0;
        public int Card5 { get; set; } = 0;
        public int Card6 { get; set; } = 0;
        public int Card7 { get; set; } = 0;
        public int Card8 { get; set; } = 0;
        public int Card9 { get; set; } = 0;
        public int Card10 { get; set; } = 0;
        public int Card11 { get; set; } = 0;
        public int Card12 { get; set; } = 0;
        public int Card13 { get; set; } = 0;
        public int Card14 { get; set; } = 0;
        public int Card15 { get; set; } = 0;
        public int Card16 { get; set; } = 0;
        public int Card17 { get; set; } = 0;
        public int Card18 { get; set; } = 0;
        public int Card19 { get; set; } = 0;
        public int Card20 { get; set; } = 0;
        public int Card21 { get; set; } = 0;
        public int Card22 { get; set; } = 0;
        public int Card23 { get; set; } = 0;
        public int Card24 { get; set; } = 0;
        public int Card25 { get; set; } = 0;
        public int Card26 { get; set; } = 0;
        public int Card27 { get; set; } = 0;
        public int Card28 { get; set; } = 0;
        public int Card29 { get; set; } = 0;
        public int Card30 { get; set; } = 0;
        public int Card31 { get; set; } = 0;
        public int Card32 { get; set; } = 0;
        public int Card33 { get; set; } = 0;
        public int Card34 { get; set; } = 0;
        public int Card35 { get; set; } = 0;
        public int Card36 { get; set; } = 0;
        public int Card37 { get; set; } = 0;
        public int Card38 { get; set; } = 0;
        public int Card39 { get; set; } = 0;
        public int Card40 { get; set; } = 0;
        public int Card41 { get; set; } = 0;
        public int Card42 { get; set; } = 0;
        public int Card43 { get; set; } = 0;
        public int Card44 { get; set; } = 0;
        public int Card45 { get; set; } = 0;
        public int Card46 { get; set; } = 0;
        public int Card47 { get; set; } = 0;
        public int Card48 { get; set; } = 0;
        public int Card49 { get; set; } = 0;
        public int Card50 { get; set; } = 0;
        public int Card51 { get; set; } = 0;
        public int Card52 { get; set; } = 0;
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
        public long? OperatorId { get; set; }
        public string WinningResultOperator { get; set; }
        public DateTime? OperatorInputDate { get; set; }
        public long? ValidatorId { get; set; }
        public string WinningResultValidator { get; set; }
        public DateTime? ValidatorInputDate { get; set; }
        public bool JackpotWinner { get; set; }
        public decimal? JackpotPrize { get; set; }
        public bool MiniJackpotWinner { get; set; }
        public decimal? MiniJackpotPrize { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DrawDate { get; set; }

        public virtual ICollection<BingoBet> Bets { get; set; }
        public int DrawNumber { get; set; }
        public int? MaxDraws { get; set; }
    }
}
