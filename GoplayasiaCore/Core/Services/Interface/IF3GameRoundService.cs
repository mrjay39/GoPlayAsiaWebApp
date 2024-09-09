using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaCore.Core.Services.Interface;

public interface IF3GameRoundService
{
    Task<F3GameRoundModel> GetRound(int gameTypeId);
    Task<F3BetResultModel> GetBetSummaryOnRound(long userId, int gameTypeId);
    Task<List<F3BetModel>> GetBets(long userId, int gameTypeId, long? gameRoundId);
    Task<List<F3GameRoundModel>> GetTrends();
    Task<F3BetResultModel> BetOnRound(F3BetDTO paramsModel);
    Task<List<F3BetModel>> GetPrevGameBets(long userId, int count);
}
