using AutoMapper;
using Blazored.Modal.Services;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Dtos.DTOIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoplayasiaSharedKernel.DTOs.DTOIn;
using GoplayasiaCore.Core.Services.Interface;
using GoplayasiaBlazor.Core.Global;
using GoplayasiaBlazor.DTOs.Bingo;
using GoplayasiaBlazor.Dtos.DTOIn.Profile;
using GoplayasiaBlazor.DTOs.eGames;

namespace GoplayasiaCore.Core.Services
{
	public class EGamesService : IEGamesService
	{
		private readonly IHTTPClientHelper _httpClientHelper;
		private readonly ICurrentUser _currentUser;
		private readonly IMapper _mapper;
		public IModalService _modal { get; set; } = default!;

		private readonly IToastService _toastService;

		public IModalReference _globalPopup { get; set; }
		public EGamesService(IHTTPClientHelper httpClientHelper, ICurrentUser currentUser, IModalService Modal, IToastService toastService)
		{
			_httpClientHelper = httpClientHelper;
			_currentUser = currentUser;
			_modal = Modal;
			_toastService = toastService;
		}
		public async Task<List<eGamesListDTO>> GetGames(string gameProvider, string gameType)
		{
			try
			{
				var result = await _httpClientHelper.GetAsync<List<eGamesListDTO>>($"eGames/listgames/{gameProvider}/{gameType}", _currentUser.Token);
                if (result == null)
					throw new Exception();
				return result;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
