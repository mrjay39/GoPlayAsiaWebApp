
using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs.Bingo;
using GoplayasiaBlazor.DTOs.Bingo.Outgoing;
using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaBlazor.Core.Services
{
    public class BingoGameRoundService : IBingoGameRoundService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _iCurrentUser;
        private readonly IMapper _mapper;

        public BingoGameRoundService(IHTTPClientHelper httpClientHelper, ICurrentUser iCurrentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _iCurrentUser = iCurrentUser;
            _mapper = mapper;
        }

        public async Task<BingoGameRoundDTO> GetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<BingoGameRoundDTO>($"BingoRound/GetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BingoGameRoundDTO>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BingoGameRoundDTO>> GetTrends()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BingoGameRoundDTO>>($"BingoRound/Trends", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BingoGameRoundDTO>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<BingoBetResultDTO> BingoBetOnRound(BingoBetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<BingoBetResultDTO>("BingoRound/Bet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BingoBetResultDTO>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BingoBetDTO>> BingoGetBet(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<DiceBetDTO>>($"BingoRound/GetBet/{userId}/{gameTypeId}?gameRoundId={gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BingoBetDTO>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BingoBetDTO>> GetPrevBet(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BingoBetDTO>>($"BingoRound/GetPrevBet/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BingoBetDTO>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BingoBetDTO>> GetPrevGameBets(long userId, int gameTypeId, int count)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BingoBetDTO>>($"BingoRound/GetPrevGameBets/{userId}/{gameTypeId}/{count}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BingoBetDTO>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BingoGameRoundDTO>> GetRoundById(long gameroundid, int? gameTypeId = 0)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BingoGameRoundDTO>>($"BingoRound/GetRoundById/{gameroundid}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BingoGameRoundDTO>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<BingoBetResultDTO> BingoGetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<BingoBetResultDTO>($"BingoRound/GetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BingoBetResultDTO>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<BingoBetResultDTO> GetPrevBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<BingoBetResultDTO>($"BingoRound/GetPrevBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BingoBetResultDTO>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<BetUpdatesModel> GetGameBets(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<BetUpdatesModel>($"BingoRound/GetGameBets/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BetUpdatesModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BingoPrizePerDrawDTO>> GetPrizeByChip(int chip)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BingoPrizePerDrawDTO>>($"BingoRound/GetPrizeByChip/{chip}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

    }
}
