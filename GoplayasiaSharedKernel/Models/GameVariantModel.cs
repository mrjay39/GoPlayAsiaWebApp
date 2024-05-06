using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class GameVariantModel
    {
		public int GameVariantID { get; set; }
		public int GameTypeId { get; set; }
		public string GameVariantDesc { get; set; }
		public string GameStreamUrl { get; set; }
		public decimal FixedPriceMultiplier { get; set; }
		public decimal PayoutMultiplier { get; set; }
		public decimal RunningOddsPercentage { get; set; }
		public decimal OddsPercentageReturn { get; set; }
		public decimal RegularCorporationShare { get; set; }
		public decimal RegularAgentShare { get; set; }
		public decimal RegularMasterAgentShare { get; set; }
		public decimal OddsCorporationShare { get; set; }
		public decimal OddsAgentShare { get; set; }
		public decimal OddsMasterAgentShare { get; set; }
		public bool FixedPriceEnabled { get; set; }
		public bool RunningOddsEnabled { get; set; }
		public bool BetOnDrawEnabled { get; set; }
		public bool DisplayTotalBetEnabled { get; set; }
		public decimal NumbersTotalBetsCap { get; set; }
		public decimal TotalBetsCap { get; set; }
		public int BettingDuration { get; set; }
		public decimal NumbersMinimumBet { get; set; }
		public decimal NumbersMaximumBet { get; set; }
		public decimal MinimumBet { get; set; }
		public decimal MaximumBet { get; set; }
		public decimal FixValue { get; set; }
		public decimal BaseRatioValue { get; set; }
		public decimal F2MaximumBet { get; set; }
		public decimal F2Ratio { get; set; }
		public decimal F3MaximumBet { get; set; }
		public decimal F3Ratio { get; set; }
		public decimal A4MaximumBet { get; set; }
		public decimal A4Ratio { get; set; }
		public decimal DifferentialMinimum { get; set; }
		public decimal DifferentialMaximum { get; set; }
		public decimal F2SoldOutMinimum { get; set; }
		public decimal F2SoldOutMaximum { get; set; }
		public decimal F3SoldOutMinimum { get; set; }
		public decimal F3SoldOutMaximum { get; set; }
		public decimal A4SoldOutMinimum { get; set; }
		public decimal A4SoldOutMaximum { get; set; }
		public decimal NumbersBaseChecker { get; set; }
		public decimal NumbersAdditional { get; set; }
		public decimal Jackpot { get; set; }
		public decimal MiniJackpot { get; set; }
		public decimal ProgressivePercentage { get; set; }
		public decimal MProgressivePercentage { get; set; }
		public decimal Consolation { get; set; }
		public decimal TaxableMinimum { get; set; }
		public decimal TaxPercentage { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public long CreatedById { get; set; }
		public long ModifiedById { get; set; }

        public string TaxPercentageDisplay => SetTaxPercentageDisplay();
        private string SetTaxPercentageDisplay()
        {
            string result = string.Empty;
            if (TaxPercentage != null)
            {
                var remainder = (decimal)TaxPercentage % 1;
                result = (remainder > 0 ? Convert.ToDecimal(TaxPercentage).ToString("N2") : Convert.ToDecimal(TaxPercentage).ToString("N0")) + "%";
            }
            return result;
        }
    }
}
