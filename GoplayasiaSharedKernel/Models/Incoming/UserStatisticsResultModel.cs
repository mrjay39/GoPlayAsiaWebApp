using GoplayasiaBlazor.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models.Incoming
{
    public class UserStatisticsResultModel : BaseResultModel
    {
        public decimal CurrentToken { get; set; }
        public string CurrentTokenString { get; set; }
        public decimal TotalDeposit { get; set; }
        public string TotalDepositString { get; set; }
        public decimal TotalWin { get; set; }
        public string TotalWinString { get; set; }
        public decimal TotalLoss { get; set; }
        public string TotalLossString { get; set; }
        public decimal TotalWithdrawal { get; set; }
        public string TotalWithdrawalString { get; set; }
        public decimal EndingOverallTotal { get; set; }
        public string TotalEndingOverallTotalString { get; set; }
        public decimal Difference { get; set; }
        public string DifferenceString { get; set; }
        public string DifferenceLabelColor => SetDifferenceLabelColor();
        private string SetDifferenceLabelColor()
        {
            return TotalWin > TotalLoss ? "Green" : "Red";
        }
    }
}
