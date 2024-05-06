
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaBlazor.Core.Services.Interface
{
    public interface ITransactionService
    {
        Task<TransactionResultDTO> Topup(TopupRequestParamsDTO paramsModel);
        Task<TransactionResultDTO> Withdraw(WithdrawRequestParamsDTO paramsModel);
        Task<TransactionModel> Transaction(long tranid);
    }
}
