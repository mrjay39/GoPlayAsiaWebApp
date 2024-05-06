using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Dtos.DTOIn.Profile;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services
{
    public class ConstantService : IConstantService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public ConstantService(IHTTPClientHelper httpClientHelper, IMapper mapper, ICurrentUser currentUser)
        {
            _httpClientHelper = httpClientHelper;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<ConstantResultDTO> GetConstants()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<ConstantResultDTO>("Constant", string.Empty);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ProvinceModel>> GetRegionProvinces(int regionId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<ProvinceDTO>>($"Constant/Provinces/{regionId}", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<ProvinceModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CityModel>> GetAllCities()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<CityDTO>>("Constant/AllCities", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<CityModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CityModel> GetParentsOfCity(int cityId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<CityDTO>($"Constant/CityParent/{cityId}", string.Empty);
                if (result == null || result.Id < 1)
                    throw new Exception();
                return _mapper.Map<CityModel>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<NationalityModel>> GetAllNationalities()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<NationalityDTO>>("Constant/Nationalities", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<NationalityModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<NatureOfWorkModel>> GetAllNaturesOfWork()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<NatureOfWorkDTO>>("Constant/NaturesOfWork", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<NatureOfWorkModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<SourceOfIncomeModel>> GetAllSourceOfIncome()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<SourceOfIncomeDTO>>("Constant/SourcesOfIncome", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<SourceOfIncomeModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<CityModel>> GetProvinceCities(int provinceId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<CityDTO>>($"Constant/Cities/{provinceId}", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<CityModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<BarangayModel>> GetCityBarangays(int cityId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<BarangayDTO>>($"Constant/Barangays/{cityId}", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<BarangayModel>>(result);
            }
            catch
            {
                return null;
            }
        }

        public async Task<CorporationSettingModel> GetCorporationSettings()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<CorporationSettingModel>("Constant/CorporationSettings", string.Empty);
                if (result == null || result.Id < 1)
                    throw new Exception();
                return _mapper.Map<CorporationSettingModel>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<CashinCashoutSettings>> GetCashinCashoutSettings()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<CashinCashoutSettingsDTO>>("Constant/GetCashinCashoutSettings", _currentUser.Token);
                return _mapper.Map<List<CashinCashoutSettings>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<UBBanksDTO> GetInstpayBanks()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<UBBanksDTO>("Constant/GetListOfBanks", _currentUser.Token);
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<GCashAccountDTO> GetActiveGCashAccount()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<GCashAccountDTO>("Constant/GetActiveGCashAccount", _currentUser.Token);
                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<SurveyQuestionModel>> GetSurveyQuestions()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<SurveyQuestionModel>>("Constant/GetSurveyQuestions", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<SurveyQuestionModel>>(result);
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<SurveyAnswerModel>> GetSurveyAnswers(int QuestionId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<SurveyAnswerModel>>($"Constant/GetSurveyAnswers/{QuestionId}", string.Empty);
                if (result == null || result.Count < 1)
                    throw new Exception();
                return _mapper.Map<List<SurveyAnswerModel>>(result);
            }
            catch
            {
                return null;
            }
        }
    }
}
