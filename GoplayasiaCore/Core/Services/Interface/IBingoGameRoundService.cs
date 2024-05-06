using GoplayasiaBlazor.DTOs.Bingo;
using GoplayasiaBlazor.DTOs.Bingo.Outgoing;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services.Interface
{
    public interface IBingoGameRoundService
    {
        Task<BingoBetResultDTO> BingoBetOnRound(BingoBetDTO paramsModel);
        Task<List<BingoBetDTO>> BingoGetBet(long userId, int gameTypeId, long? gameRoundId);
        Task<BingoBetResultDTO> BingoGetBetSummaryOnRound(long userId, int gameTypeId);
        Task<BetUpdatesModel> GetGameBets(int gameTypeId);
        Task<List<BingoBetDTO>> GetPrevBet(long userId, int gameTypeId);
        Task<BingoBetResultDTO> GetPrevBetSummaryOnRound(long userId, int gameTypeId);
        Task<List<BingoBetDTO>> GetPrevGameBets(long userId, int gameTypeId, int count);
        Task<BingoGameRoundDTO> GetRound(int gameTypeId);
        Task<List<BingoGameRoundDTO>> GetRoundById(long gameroundid, int? gameTypeId = 0);
        Task<List<BingoGameRoundDTO>> GetTrends();
        Task<List<BingoPrizePerDrawDTO>> GetPrizeByChip(int chip);
    }
}