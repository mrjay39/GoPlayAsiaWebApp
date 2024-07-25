using GoplayasiaBlazor.Dtos.DTOIn;
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
        Task<List<EvolutionGamesDTO>> GetGames(string gameProvider, string gameType);
    }
}
