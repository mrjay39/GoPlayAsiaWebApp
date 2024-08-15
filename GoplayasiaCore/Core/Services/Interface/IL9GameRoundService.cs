using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaCore.Core.Services.Interface;

public interface IL9GameRoundService
{
    Task<L9GameRoundModel> GetRound(int gameTypeId);
    Task<L9BetResultModel> GetBetSummaryOnRound(long userId, int gameTypeId);
    Task<List<L9BetModel>> GetBets(long userId, int gameTypeId, long? gameRoundId);
    Task<List<L9GameRoundModel>> GetTrends();
    Task<L9BetResultModel> BetOnRound(L9BetDTO paramsModel);
    Task<List<L9BetModel>> L9GetPrevGameBets(long userId, int count);
}
