
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
    public class GameRoundService : IGameRoundService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _iCurrentUser;
        private readonly IMapper _mapper;

        public GameRoundService(IHTTPClientHelper httpClientHelper, ICurrentUser iCurrentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _iCurrentUser = iCurrentUser;
            _mapper = mapper;
        }
        public async Task<BetResultModel> BetOnRound(BetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<BetResultDTO>("Round/Bet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }

        //public async Task<GameRoundModel> CancelRound(long gameRoundId)
        //{
        //    try
        //    {
        //        var result = await _httpClientHelper.GetAsync<GameRoundDTO>($"Round/CancelRound/{gameRoundId}", _systemSettingsHelper.Token);
        //        if (result == null)
        //            throw new Exception();
        //        return _mapper.Map<GameRoundModel>(result);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<GameRoundModel> CloseRound(long gameRoundId)
        //{
        //    try
        //    {
        //        var result = await _httpClientHelper.GetAsync<GameRoundDTO>($"Round/CloseRound/{gameRoundId}", _systemSettingsHelper.Token);
        //        if (result == null)
        //            throw new Exception();
        //        return _mapper.Map<GameRoundModel>(result);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<GameRoundModel> CreateRound(GameRoundDTO paramsModel)
        //{
        //    try
        //    {
        //        var result = await _httpClientHelper.PostAsync<GameRoundDTO>("Round/CreateRound", _systemSettingsHelper.Token, paramsModel);
        //        if (result == null)
        //            throw new Exception();
        //        return _mapper.Map<GameRoundModel>(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return null;
        //    }
        //}

        public async Task<List<BetModel>> GetBets(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BetDTO>>($"Round/GetBet/{userId}/{gameTypeId}/{gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BetModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BetModel>> GetPrevBets(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BetDTO>>($"Round/GetPrevBet/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BetModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<GameRoundModel> GetRoundById(long gameroundid)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<GameRoundDTO>>($"Round/GetRoundById/{gameroundid}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<GameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<GameRoundModel> GetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<GameRoundDTO>($"Round/GetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<GameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<GameRoundModel>> GetTrends(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<GameRoundDTO>>($"Round/Trends/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<GameRoundModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        //public async Task<GameRoundModel> HoldRound(long gameRoundId)
        //{
        //    try
        //    {
        //        var result = await _httpClientHelper.GetAsync<GameRoundDTO>($"Round/HoldRound/{gameRoundId}", _systemSettingsHelper.Token);
        //        if (result == null)
        //            throw new Exception();
        //        return _mapper.Map<GameRoundModel>(result);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<GameRoundModel> InputOperatorResult(long gameRoundId, long operatorId, string winningResult)
        //{
        //    try
        //    {
        //        var result = await _httpClientHelper.GetAsync<GameRoundDTO>($"Round/InputOperatorResult/{gameRoundId}/{operatorId}/{winningResult}", _systemSettingsHelper.Token);
        //        if (result == null)
        //            throw new Exception();
        //        return _mapper.Map<GameRoundModel>(result);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        //public async Task<GameRoundModel> InputValidatorResult(long gameRoundId, long validatorId, string winningResult)
        //{
        //    try
        //    {
        //        var result = await _httpClientHelper.GetAsync<GameRoundDTO>($"Round/InputOperatorResult/{gameRoundId}/{validatorId}/{winningResult}", _systemSettingsHelper.Token);
        //        if (result == null)
        //            throw new Exception();
        //        return _mapper.Map<GameRoundModel>(result);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        public async Task<BetResultModel> GetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<BetResultDTO>($"Round/GetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<BetResultModel> GetPrevBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<BetResultDTO>($"Round/GetPrevBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<BetModel>> GetPrevGameBets(long userId, int gameTypeId, int count)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BetDTO>>($"Round/GetPrevGameBets/{userId}/{gameTypeId}/{count}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<BetModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<L9GameRoundModel> L9GetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<L9GameRoundDTO>($"Round/L9GetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<L9GameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<L9BetResultModel> L9GetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<L9BetResultDTO>($"Round/L9GetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<L9BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<L9BetModel>> L9GetBets(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<L9BetDTO>>($"Round/L9GetBet/{userId}/{gameTypeId}?gameRoundId={gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<L9BetModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<L9GameRoundModel>> L9GetTrends()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<L9GameRoundDTO>>($"Round/L9Trends", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<L9GameRoundModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<L9BetResultModel> L9BetOnRound(L9BetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<L9BetResultDTO>("Round/L9Bet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<L9BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<L9BetModel>> L9GetPrevGameBets(long userId, int count)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<L9BetDTO>>($"Round/L9GetPrevGameBets/{userId}/{count}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<L9BetModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<F3GameRoundModel> F3GetRound(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<F3GameRoundDTO>($"Round/F3GetRound/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<F3GameRoundModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<F3BetResultModel> F3GetBetSummaryOnRound(long userId, int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<F3BetResultDTO>($"Round/F3GetBetSummaryOnRound/{userId}/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<F3BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<F3BetModel>> F3GetBets(long userId, int gameTypeId, long? gameRoundId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<F3BetDTO>>($"Round/F3GetBet/{userId}/{gameTypeId}?gameRoundId={gameRoundId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<F3BetModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<F3GameRoundModel>> F3GetTrends()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<F3GameRoundDTO>>($"Round/F3Trends", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<F3GameRoundModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<F3BetResultModel> F3BetOnRound(F3BetDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<F3BetResultDTO>("Round/F3Bet", _iCurrentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<F3BetResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<F3BetModel>> F3GetPrevGameBets(long userId, int count)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<F3BetDTO>>($"Round/F3GetPrevGameBets/{userId}/{count}", _iCurrentUser.Token);
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
