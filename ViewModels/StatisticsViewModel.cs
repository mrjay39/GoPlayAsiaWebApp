using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.DTOs.DTOOut;
using GoplayasiaBlazor.Models.Incoming;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        #region Local Variable & Properties
        ICurrentUser _currentUser;
        IReportService _reportService;

        private UserStatisticsResultModel _statistics;
        public UserStatisticsResultModel Statistic
        {
            get => _statistics;
            set
            {
                _statistics = value;
            }
        }
        #endregion

        #region Life cycle methods
        public StatisticsViewModel(ICurrentUser currentUser, IReportService reportService, NavigationManager navigationManager, IAccountService iaccountService, IToastService toastService, 
            AuthenticationStateProvider AuthenticationStateProvider)
        {
            _currentUser = currentUser;
            _reportService = reportService;
            _navigationManager = navigationManager;
            _iaccountService = iaccountService;
            _toastService = toastService;
            _AuthenticationStateProvider = AuthenticationStateProvider;
        }
        public async Task GetStatistics()
        {
            ReportParamsDTO paramsModel = new ReportParamsDTO()
            {
                UserId = _currentUser.Id
            };
            var stat = await _reportService.GetUserStatistics(paramsModel, _currentUser.Token);
            if (stat != null)
            {
                Statistic = stat;
            }
        }
        #endregion
    }
}
