using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Incoming;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Core.Services.Interface
{
    public interface IReportService
    {
        Task<List<TransactionModel>> GetTransactionsByUserId();
        Task<GameRoundHistoryResultDTO> GetGameHistoryWithUserBets(ReportParamsDTO paramsModel, string authenticationToken);
        Task<UserStatisticsResultModel> GetUserStatistics(ReportParamsDTO paramsModel, string authenticationToken);
    }
}
