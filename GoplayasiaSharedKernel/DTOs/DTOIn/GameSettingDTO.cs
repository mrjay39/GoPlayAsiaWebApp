using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class GameSettingDTO
    {
        public int Id { get; set; }
        public int GameTypeId { get; set; }
        public string GameStreamUrl { get; set; }
        public decimal? FixedPriceMultiplier { get; set; }
        public decimal? PayoutMultiplier { get; set; }
        public decimal? RunningOddsPercentage { get; set; }
        public decimal? OddsPercentageReturn { get; set; }
        public bool FixedPriceEnabled { get; set; }
        public bool RunningOddsEnabled { get; set; }
        public bool BetOnDrawEnabled { get; set; }
        public bool DisplayTotalBetEnabled { get; set; }
        public decimal? NumbersTotalBetsCap { get; set; }
        public decimal? TotalBetsCap { get; set; }
        public int? BettingDuration { get; set; }
        public decimal? NumbersMinimumBet { get; set; }
        public decimal? NumbersMaximumBet { get; set; }
        public decimal? MinimumBet { get; set; }
        public decimal? MaximumBet { get; set; }
        public decimal? FixValue { get; set; }
        public decimal? BaseRatioValue { get; set; }
        public decimal? F2maximumBet { get; set; }
        public decimal? F2ratio { get; set; }
        public decimal? F3maximumBet { get; set; }
        public decimal? F3ratio { get; set; }
        public decimal? A4maximumBet { get; set; }
        public decimal? A4ratio { get; set; }
        public decimal? DifferentialMinimum { get; set; }
        public decimal? DifferentialMaximum { get; set; }
        public decimal? F2soldOutMinimum { get; set; }
        public decimal? F2soldOutMaximum { get; set; }
        public decimal? F3soldOutMinimum { get; set; }
        public decimal? F3soldOutMaximum { get; set; }
        public decimal? A4soldOutMinimum { get; set; }
        public decimal? A4soldOutMaximum { get; set; }
        public decimal? NumbersBaseChecker { get; set; }
        public decimal? NumbersAdditional { get; set; }
        public decimal? Jackpot { get; set; }
        public decimal? MiniJackpot { get; set; }
        public decimal? ProgressivePercentage { get; set; }
        public decimal? MProgressivePercentage { get; set; }
        public decimal? Consolation { get; set; }
        public decimal? TaxableMinimum { get; set; }
        public decimal? TaxPercentage { get; set; }
        public long? ModifiedById { get; set; }

        public string F2RatioBaseQuotient { get; set; }
        public string F3RatioBaseQuotient { get; set; }
        public string A4RatioBaseQuotient { get; set; }
    }
}
