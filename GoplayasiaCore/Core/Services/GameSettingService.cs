using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs;
using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services
{
    public class GameSettingService : IGameSettingService
    {
        private readonly IHTTPClientHelper _httpClientHelper;
        private readonly ICurrentUser _iCurrentUser;
        private readonly IMapper _mapper;

        public GameSettingService(IHTTPClientHelper httpClientHelper, ICurrentUser iCurrentUser, IMapper mapper)
        {
            _httpClientHelper = httpClientHelper;
            _iCurrentUser = iCurrentUser;
            _mapper = mapper;
        }
        
        public async Task<GameChipModel> GetGameChips(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<GameChipDTO>($"GameSettings/GetGameChips/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<GameChipModel>(result);
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<GameSettingModel> GetGameSettings(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<GameSettingDTO>($"GameSettings/GetGameSettings/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<GameSettingModel>(result);
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<List<GameVariantModel>> GetGameSettingsVariant(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<GameVariantModel>>($"GameSettings/GetGameSettingsVariant/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<GameVariantModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<GameTypeModel> GetGameType(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<GameTypeDTO>($"GameSettings/GetGameType/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<GameTypeModel>(result);
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<List<GameVariantChipsModel>> GetGameVariantChips(int gameTypeId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<GameVariantChipsModel>>($"GameSettings/GetGameVariantChips/{gameTypeId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<GameVariantChipsModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<StreamKey> GenerateStreamKey(string streamID, string type)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<StreamKey>($"GameSettings/GenerateStreamKey/{streamID}/{type}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return result;
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<List<PlayerCategoryModel>> GetPlayerCategory()
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<PlayerCategoryModel>>($"GameSettings/GetPlayerCategory", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<PlayerCategoryModel>>(result);
            }
            catch
            {
                throw new Exception();
            }
        }
        
        public async Task<GameChipModel> GetGameChipsByCategory(int gameTypeId, int categoryId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<GameChipDTO>($"GameSettings/GetGameChipsByCategoryId/{gameTypeId}/{categoryId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<GameChipModel>(result);
            }
            catch
            {
                return null;
            }
        }
        
        public async Task<List<GameVariantChipsModel>> GetGameVariantChipsByCategory(int gameTypeId, int categoryId)
        {
            try
            {
                var result = await _httpClientHelper.GetAsync<List<GameVariantChipsModel>>($"GameSettings/GetGameVariantChipsByCategoryId/{gameTypeId}/{categoryId}", _iCurrentUser.Token);
                if (result == null)
                    throw new Exception();
                return _mapper.Map<List<GameVariantChipsModel>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
