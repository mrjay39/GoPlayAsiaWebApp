using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Core.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.Design;
using AutoMapper;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Global;
using GoplayasiaBlazor.Core.Helpers.Interface;
using GoplayasiaBlazor.Core.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.SessionStorage;
using Blazored.Toast;
using Blazored.Modal;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoplayasiaCore.Core.Services;

namespace GoPlayAsiaWebApp
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection AddBlazorServices(this IServiceCollection services)
        {


            services.AddBlazoredSessionStorage();
            services.AddBlazoredToast();
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            services.AddScoped<Lucky9ViewModel>();
            services.AddScoped<DropwinViewModel>();
            services.AddScoped<DropwinS5ViewModel>();
            services.AddScoped<First3ViewModel>();
            services.AddScoped<Go12ViewModel>();
            services.AddScoped<GigadrawModel>();
            services.AddScoped<NotificationViewModel>();
            services.AddScoped<CompBetHistoryViewModel>();
            services.AddScoped<StatisticsViewModel>();
            services.AddScoped<SettingsViewModel>();
            services.AddScoped<LobbyViewModel>();
            services.AddScoped<MainCreditViewModel>();
            services.AddScoped<GameHeaderViewModel>();
            services.AddScoped<ForgotPasswordViewModel>();
            services.AddScoped<BigWinViewModel>();
            services.AddScoped<MainPageHistoryViewModel>();
            services.AddScoped<VerifyRegistrationViewModel>();
            services.AddScoped<Lucky4V2ViewModel>();
            services.AddScoped<HeadsAndTailsViewModel>();
            services.AddScoped<DiceViewModel>();
            services.AddScoped<BingoViewModel>();
           

            services.AddBlazoredModal();
            services.AddSingleton(new MapperConfiguration(mapper =>
            {
                mapper.AddProfile(new MappingProfile());
            }).CreateMapper());

            services.AddScoped<IGameRoundService, GameRoundService>();
            services.AddScoped<IConstantService, ConstantService>();
            services.AddScoped<IGameSettingService, GameSettingService>();
            services.AddScoped<IDiceGameRoundService, DiceGameRoundService>();
            services.AddScoped<IBingoGameRoundService, BingoGameRoundService>();
            services.AddAuthorizationCore();


            //Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddTransient<ReCaptchaClass>();
            services.AddSingleton<GlobalStateService>();


            //Helpders
            services.AddScoped<IHTTPClientHelper, HTTPClientHelper>();
            services.AddSingleton<IHTTPReportClientHelper, HTTPReportClientHelper>();
            services.AddSingleton<ValidatorHelper>();

            //Globals
            services.AddScoped<ICurrentUser, CurrentUser>();
            services.AddScoped<SignUpViewModel>();

          

            return services;
        }
    }
}
