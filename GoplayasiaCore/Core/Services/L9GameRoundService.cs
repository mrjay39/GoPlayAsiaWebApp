using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;
using GoplayasiaCore.Core.Services.Interface;

namespace GoplayasiaCore.Core.Services
{
    public class L9GameRoundService : IL9GameRoundService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _iCurrentUser;
        private readonly IMapper _mapper;

        public L9GameRoundService(IHTTPClientHelper httpClientHelper, ICurrentUser iCurrentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _iCurrentUser = iCurrentUser;
            _mapper = mapper;
        }

        public async Task<L9GameRoundModel> GetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<L9GameRoundDTO>($"L9Round/GetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<L9GameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<L9BetResultModel> GetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<L9BetResultDTO>($"L9Round/GetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<L9BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<L9BetModel>> GetBets(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<L9BetDTO>>($"L9Round/GetBet/{userId}/{gameTypeId}?gameRoundId={gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<L9BetModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<L9GameRoundModel>> GetTrends()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<L9GameRoundDTO>>($"L9Round/GetTrends", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<L9GameRoundModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<L9BetResultModel> BetOnRound(L9BetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<L9BetResultDTO>("L9Round/Bet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<L9BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<L9BetModel>> GetPrevGameBets(long userId, int count)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<L9BetDTO>>($"L9Round/GetPrevGameBets/{userId}/{count}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<L9BetModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
