using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;

namespace GoplayasiaBlazor.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IHTTPReportClientHelper _client;
        private readonly string _api;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public ReportService(IHTTPReportClientHelper httpClientHelper, IMapper mapper, ICurrentUser currentUser)
        {
            _client = httpClientHelper;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        #region REPORTING METHODS
        public async Task<List<TransactionModel>> GetTransactionsByUserId()
        {
            try
            {
                var result = await _client.GetAsync<List<TransactionDTO>>($"Report/GetTransactionsByUserId/{_currentUser.Id}", _currentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<TransactionModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<GameRoundHistoryResultDTO> GetGameHistoryWithUserBets(ReportParamsDTO paramsModel, string authenticationToken)
        {
            try
            {
                return await _client.PostAsync<GameRoundHistoryResultDTO>("Report/GameHistoryWithUserBets", authenticationToken, paramsModel);
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<UserStatisticsResultModel> GetUserStatistics(ReportParamsDTO paramsModel, string authenticationToken)
        {
            try
            {
                var result = await _client.PostAsync<UserStatisticsResultDTO>("Report/Statistics", authenticationToken, paramsModel);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<UserStatisticsResultModel>(result);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
