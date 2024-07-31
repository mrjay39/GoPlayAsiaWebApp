using GoplayasiaBlazor.Dtos.DTOIn;
using GoplayasiaBlazor.DTOs.eGames;
using GoplayasiaSharedKernel.DTOs.DTOIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoplayasiaCore.Core.Services.Interface
{
	public interface IEGamesService
	{
        Task<List<eGamesListDTO>> GetGames(string gameProvider, string gameType);
        Task<List<eGamesListDTO>> ListPlayerGames(string gameProvider, string gameType);
    }
}
