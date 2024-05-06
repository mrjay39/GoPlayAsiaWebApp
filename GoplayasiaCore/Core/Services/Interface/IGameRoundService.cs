using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Base;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaBlazor.Core.Services.Interface;
public interface IGameRoundService
{
    Task<GameRoundModel> GetRound(int gameTypeId);

    Task<BetResultModel> GetBetSummaryOnRound(long userId, int gameTypeId);
    Task<BetResultModel> GetPrevBetSummaryOnRound(long userId, int gameTypeId);

    Task<List<BetModel>> GetBets(long userId, int gameTypeId, long? gameRoundId);
    Task<List<BetModel>> GetPrevBets(long userId, int gameTypeId);
    Task<BetResultModel> BetOnRound(BetDTO paramsModel);
    Task<List<GameRoundModel>> GetTrends(int gameTypeId);
    Task<GameRoundModel> GetRoundById(long gameroundid);
    Task<List<BetModel>> GetPrevGameBets(long userId, int gameTypeId, int count);


    Task<L9GameRoundModel> L9GetRound(int gameTypeId);
    Task<L9BetResultModel> L9GetBetSummaryOnRound(long userId, int gameTypeId);
    Task<List<L9BetModel>> L9GetBets(long userId, int gameTypeId, long? gameRoundId);
    Task<List<L9GameRoundModel>> L9GetTrends();
    Task<L9BetResultModel> L9BetOnRound(L9BetDTO paramsModel);
    Task<List<L9BetModel>> L9GetPrevGameBets(long userId, int count);

    Task<F3GameRoundModel> F3GetRound(int gameTypeId);
    Task<F3BetResultModel> F3GetBetSummaryOnRound(long userId, int gameTypeId);
    Task<List<F3BetModel>> F3GetBets(long userId, int gameTypeId, long? gameRoundId);
    Task<List<F3GameRoundModel>> F3GetTrends();
    Task<F3BetResultModel> F3BetOnRound(F3BetDTO paramsModel);
    Task<List<F3BetModel>> F3GetPrevGameBets(long userId, int count);
}