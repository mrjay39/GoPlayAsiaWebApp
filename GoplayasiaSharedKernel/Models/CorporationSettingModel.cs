﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Models
{
    public class CorporationSettingModel
    {
        public int Id { get; set; }
        public string AppVersion { get; set; }
        public string OperatorVerision { get; set; }
        public string TermsAndConditionsUrl { get; set; }
        public string PrivacyPolicyUrl { get; set; }
        public decimal? FirstMinimumCreditRequest { get; set; }
        public decimal? MinimumCreditRequest { get; set; }
        public decimal? MaximumCreditRequest { get; set; }
        public decimal? MinimumWithdrawRequest { get; set; }
        public decimal? MaximumWithdrawRequest { get; set; }
        public decimal? GoplayWithdrawalFee { get; set; }
        public decimal? UBInstapayFee { get; set; }

        public int? IdleTimer { get; set; }
    }
}
