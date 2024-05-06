using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services.Interface;
public interface IConstantService
{
    Task<List<CityModel>> GetAllCities();
    Task<List<NationalityModel>> GetAllNationalities();
    Task<List<NatureOfWorkModel>> GetAllNaturesOfWork();
    Task<List<SourceOfIncomeModel>> GetAllSourceOfIncome();
    Task<List<BarangayModel>> GetCityBarangays(int cityId);
    Task<ConstantResultDTO> GetConstants();
    Task<CorporationSettingModel> GetCorporationSettings();
    Task<CityModel> GetParentsOfCity(int cityId);
    Task<List<CityModel>> GetProvinceCities(int provinceId);
    Task<List<ProvinceModel>> GetRegionProvinces(int regionId);
    Task<List<CashinCashoutSettings>> GetCashinCashoutSettings();
    Task<UBBanksDTO> GetInstpayBanks();
    Task<GCashAccountDTO> GetActiveGCashAccount();
    Task<List<SurveyQuestionModel>> GetSurveyQuestions();
    Task<List<SurveyAnswerModel>> GetSurveyAnswers(int QuestionId);
}