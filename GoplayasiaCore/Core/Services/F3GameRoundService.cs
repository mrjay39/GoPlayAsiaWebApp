using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;
using GoplayasiaCore.Core.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaCore.Core.Services
{
    public class F3GameRoundService : IF3GameRoundService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _iCurrentUser;
        private readonly IMapper _mapper;

        public F3GameRoundService(IHTTPClientHelper httpClientHelper, ICurrentUser iCurrentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _iCurrentUser = iCurrentUser;
            _mapper = mapper;

        }

        public async Task<F3GameRoundModel> GetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<F3GameRoundDTO>($"F3Round/GetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<F3GameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<F3BetResultModel> GetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<F3BetResultDTO>($"F3Round/GetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<F3BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<F3BetModel>> GetBets(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<F3BetDTO>>($"F3Round/GetBet/{userId}/{gameTypeId}?gameRoundId={gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<F3BetModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<F3GameRoundModel>> GetTrends()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<F3GameRoundDTO>>($"F3Round/GetTrends", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<F3GameRoundModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<F3BetResultModel> BetOnRound(F3BetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<F3BetResultDTO>("F3Round/Bet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<F3BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<F3BetModel>> GetPrevGameBets(long userId, int count)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<F3BetDTO>>($"F3Round/GetPrevGameBets/{userId}/{count}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<F3BetModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
