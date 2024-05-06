
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.DTOs.Bingo
{
    public class BingoOperatorParamDTO
    {
        public long GameRoundId { get; set; }
        public long OperatorId { get; set; }
        public string CardValue { get; set; }

    }
}
