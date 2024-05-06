
using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaBlazor.Core.Services
{
    public class DiceGameRoundService : IDiceGameRoundService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _iCurrentUser;
        private readonly IMapper _mapper;

        public DiceGameRoundService(IHTTPClientHelper httpClientHelper, ICurrentUser iCurrentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _iCurrentUser = iCurrentUser;
            _mapper = mapper;
        }
       
        public async Task<DiceGameRoundModel> DiceGetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<DiceGameRoundDTO>($"Round/DiceGetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<DiceGameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<DiceGameRoundModel>> DiceGetTrends()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<DiceGameRoundModel>>($"Round/DiceTrends", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<DiceGameRoundModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<DiceBetResultModel> DiceBetOnRound(DiceBetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<DiceBetResultModel>("Round/DiceBet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<DiceBetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<DiceBetModel>> DiceGetBets(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<DiceBetDTO>>($"Round/DiceGetBet/{userId}/{gameTypeId}?gameRoundId={gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<DiceBetModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<DiceBetResultModel> DiceGetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<DiceBetResultModel>($"Round/DiceGetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<DiceBetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
    }
}
