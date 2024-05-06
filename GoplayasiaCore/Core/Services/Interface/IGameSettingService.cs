using GoplayasiaBlazor.DTOs.DTOIn;
using GoplayasiaBlazor.Models;

namespace GoplayasiaBlazor.Core.Services.Interface;
public interface IGameSettingService
{
    Task<GameChipModel> GetGameChips(int gameTypeId);
    Task<GameSettingModel> GetGameSettings(int gameTypeId);
    Task<GameTypeModel> GetGameType(int gameTypeId);
    Task<List<GameVariantModel>> GetGameSettingsVariant(int gameTypeId);
    Task<List<GameVariantChipsModel>> GetGameVariantChips(int gameTypeId);
    Task<StreamKey> GenerateStreamKey(string streamID, string type);
    Task<List<PlayerCategoryModel>> GetPlayerCategory();
    Task<GameChipModel> GetGameChipsByCategory(int gameTypeId, int categoryId);
    Task<List<GameVariantChipsModel>> GetGameVariantChipsByCategory(int gameTypeId, int categoryId);
}