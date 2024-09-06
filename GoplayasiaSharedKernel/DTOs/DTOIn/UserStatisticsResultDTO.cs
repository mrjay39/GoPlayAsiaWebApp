using GoplayasiaBlazor.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.DTOs.DTOIn
{
    public class UserStatisticsResultDTO : BaseResultDTO
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
        public decimal TotalBetAmount { get; set; }
        public string TotalBetAmountString { get; set; }
        public decimal TotalWinnings { get; set; }
        public string TotalWinningsString { get; set; }
    }
}
