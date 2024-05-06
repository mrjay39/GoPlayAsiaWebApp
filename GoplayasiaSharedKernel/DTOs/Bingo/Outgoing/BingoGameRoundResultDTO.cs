using GoplayasiaBlazor.Dtos.Base;
using GoplayasiaBlazor.DTOs.Bingo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.DTOs.Bingo.Outgoing
{
    public class BingoGameRoundResultDTO : BaseResultDTO
    {
        public BingoGameRoundDTO GameRound { get; set; }
    }
}
