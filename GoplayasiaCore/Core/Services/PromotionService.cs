using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public PromotionService(IHTTPClientHelper httpClientHelper, ICurrentUser currentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<List<voucherActiveModel>> GetActiveVoucherForUser()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<voucherActiveModel>>($"Promotion/GetActiveVoucherForUser", _currentUser.Token);
                if (result == null)
                    return null;
                return _mapper.Map<List<voucherActiveModel>>(result);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Value cannot be null. (Parameter 'reader')")
                {
                    throw ex;
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<List<voucherCorpModel>> GetMinTopupPromotion()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<voucherCorpModel>>($"Promotion/GetMinTopupPromotion", _currentUser.Token);
                if (result == null)
                    return null;
                return _mapper.Map<List<voucherCorpModel>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
