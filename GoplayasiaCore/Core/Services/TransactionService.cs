using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public TransactionService(IHTTPClientHelper httpClientHelper, ICurrentUser currentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<TransactionResultDTO> Topup(TopupRequestParamsDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<TransactionResultDTO>("Transaction/Topup", _currentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<TransactionResultDTO> Withdraw(WithdrawRequestParamsDTO paramsModel)
        {
            try
            {
                var result = await _httpClientHelper.PostAsync<TransactionResultDTO>($"Transaction/Withdraw", _currentUser.Token, paramsModel);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<TransactionModel> Transaction(long tranid)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<TransactionDTO>($"Transaction/Transaction/{tranid}", _currentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<TransactionModel>(result);
            }
            catch
            {
                return null;
            }
        }


    }
}
