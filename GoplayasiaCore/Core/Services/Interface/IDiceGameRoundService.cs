using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Base;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaBlazor.Core.Services.Interface;
public interface IDiceGameRoundService
{
    Task<DiceGameRoundModel> DiceGetRound(int gameTypeId);
    Task<List<DiceGameRoundModel>> DiceGetTrends();
    Task<DiceBetResultModel> DiceBetOnRound(DiceBetDTO paramsModel);
    Task<List<DiceBetModel>> DiceGetBets(long userId, int gameTypeId, long? gameRoundId);
    Task<DiceBetResultModel> DiceGetBetSummaryOnRound(long userId, int gameTypeId);
}