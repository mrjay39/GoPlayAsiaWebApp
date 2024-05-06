
using GoplayasiaBlazor.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.DTOs.Bingo
{
    public class BingoRoundResultDTO : BaseResultDTO
    {
        public BingoGameRoundDTO GameRound { get; set; }
    }
}
