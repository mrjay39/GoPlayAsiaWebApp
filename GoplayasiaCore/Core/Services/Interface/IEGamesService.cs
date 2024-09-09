using GoplayasiaBlazor.DTOs.eGames;
using GoplayasiaSharedKernel.DTOs.eGames;

namespace GoplayasiaCore.Core.Services.Interface
{
    public interface IEGamesService
    {
        Task<List<eGamesListDTO>> GetGames(string gameProvider, string gameType);
        Task<List<eGamesListDTO>> ListPlayerGames(string gameProvider, string gameType);
        Task<GameLaunchRespDTO> LaunchGame(eGamesListDTO game);
        Task<GameLaunchRespDTO> LaunchGameMode(eGamesListDTO game, string mode);
    }
}
