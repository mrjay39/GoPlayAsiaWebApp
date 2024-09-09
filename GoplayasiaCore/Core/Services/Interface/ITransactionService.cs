
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services.Interface
{
    public interface ITransactionService
    {
        Task<TransactionResultDTO> Topup(TopupRequestParamsDTO paramsModel);
        Task<TransactionResultDTO> Withdraw(WithdrawRequestParamsDTO paramsModel);
        Task<TransactionModel> Transaction(long tranid);
    }
}
