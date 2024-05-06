using System;
using System.Collections.Generic;

#nullable disable

namespace GoplayasiaBlazor.Models.Bingo
{
    public partial class BingoBet
    {
        public long Id { get; set; }
        public int GameTypeId { get; set; }
        public int GameVariantID { get; set; }
        public long GameRoundId { get; set; }
        public long UserId { get; set; }
        public int? BetStatus { get; set; }
        public decimal? BetAmount { get; set; }
        public string BetValue { get; set; }
        public string BetValue1 { get; set; }
        public string BetValue2 { get; set; }
        public string BetValue3 { get; set; }
        public string BetValue4 { get; set; }
        public string BetValue5 { get; set; }
        public string BetValue6 { get; set; }
        public string BetValue7 { get; set; }
        public string BetValue8 { get; set; }
        public int CardHits { get; set; } = 0;
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
        public decimal? PercentageLess { get; set; }
        public decimal? TaxPercentageAsOf { get; set; }
        public decimal? Taxations { get; set; }
        public decimal? Winnings { get; set; }
        public decimal? ActualWinnings { get; set; }
        public decimal? CorporationCommissionPercentageAsOf { get; set; }
        public decimal? CorporationCommission { get; set; }
        public long? AgentId { get; set; }
        public decimal? AgentCommissionPercentageAsOf { get; set; }
        public decimal? AgentCommission { get; set; }
        public long? MasterAgentId { get; set; }
        public decimal? MasterAgentCommissionPercentageAsOf { get; set; }
        public decimal? MasterAgentCommission { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual GameRoundBingo GameRound { get; set; }
    }
}
