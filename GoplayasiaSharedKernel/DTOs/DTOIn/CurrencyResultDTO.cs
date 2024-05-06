
using GoplayasiaBlazor.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoplayasiaBlazor.Dtos.DTOIn
{
    public class CurrencyResultDTO : BaseResultDTO
    {
        public decimal Credits { get; set; }
        public decimal Commission { get; set; }
    }
}
