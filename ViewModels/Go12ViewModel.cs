using AutoMapper;
using Blazored.Modal;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Core.Services.Interface;
using GoplayasiaBlazor.Dtos.DTOOut;
using GoPlayAsiaWebApp.Main.Login; 
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Diagnostics;
using static GoplayasiaBlazor.Models.Constants.Settings;
using Microsoft.AspNetCore.Components.Authorization;

namespace GoPlayAsiaWebApp.ViewModels;

public class Go12ViewModel : BaseViewModel
{
    #region Local Variables

    [Inject] protected IJSRuntime JsRuntime { get; set; }

    #region GAME CONFIGURATION
    private bool _isFixedPrizeCancelled;
    private bool _isRunningOddsCancelled;
    private bool _isOptionEnabled_1;
    private bool _isOptionEnabled_2;
    private bool _isOptionEnabled_3;
    private bool _isOptionEnabled_4;
    private bool _isOptionEnabled_5;
    private bool _isOptionEnabled_6;
    private bool _isOptionEnabled_7;
    private bool _isOptionEnabled_8;
    private bool _isOptionEnabled_9;
    private bool _isOptionEnabled_10;
    private bool _isOptionEnabled_11;
    private bool _isOptionEnabled_12;
    private bool _isOptionEnabled_Odds_Black;
    private bool _isOptionEnabled_Odds_Red;
    private bool _isWon_1;
    private bool _isWon_2;
    private bool _isWon_3;
    private bool _isWon_4;
    private bool _isWon_5;
    private bool _isWon_6;
    private bool _isWon_7;
    private bool _isWon_8;
    private bool _isWon_9;
    private bool _isWon_10;
    private bool _isWon_11;
    private bool _isWon_12;
    private bool _isOddsWon_Black;
    private bool _isOddsWon_Red;
    private TimeSpan _previousTime;
    private TimeSpan _currentTime;
    private bool _showTotalBets;
    private string _runningOdds_Black_Percentage = "0%";
    private string _runningOdds_Red_Percentage = "0%";
    private string _duplicateBetvalue = "";
    private string _tokenDiv = "tokenhide";
    private string _go12gif = "";


    public bool IsFixedPrizeCancelled
    {
        get => _isFixedPrizeCancelled;
        set
        {
            _isFixedPrizeCancelled = value;
            RaisePropertyChanged(() => IsFixedPrizeCancelled);
        }
    }
    public bool IsRunningOddsCancelled
    {
        get => _isRunningOddsCancelled;
        set
        {
            _isRunningOddsCancelled = value;
            RaisePropertyChanged(() => IsRunningOddsCancelled);
        }
    }
    public bool IsOptionEnabled_1
    {
        get => _isOptionEnabled_1;
        set
        {
            _isOptionEnabled_1 = value;
            RaisePropertyChanged(() => IsOptionEnabled_1);
        }
    }
    public bool IsOptionEnabled_2
    {
        get => _isOptionEnabled_2;
        set
        {
            _isOptionEnabled_2 = value;
            RaisePropertyChanged(() => IsOptionEnabled_2);
        }
    }
    public bool IsOptionEnabled_3
    {
        get => _isOptionEnabled_3;
        set
        {
            _isOptionEnabled_3 = value;
            RaisePropertyChanged(() => IsOptionEnabled_3);
        }
    }
    public bool IsOptionEnabled_4
    {
        get => _isOptionEnabled_4;
        set
        {
            _isOptionEnabled_4 = value;
            RaisePropertyChanged(() => IsOptionEnabled_4);
        }
    }
    public bool IsOptionEnabled_5
    {
        get => _isOptionEnabled_5;
        set
        {
            _isOptionEnabled_5 = value;
            RaisePropertyChanged(() => IsOptionEnabled_5);
        }
    }
    public bool IsOptionEnabled_6
    {
        get => _isOptionEnabled_6;
        set
        {
            _isOptionEnabled_6 = value;
            RaisePropertyChanged(() => IsOptionEnabled_6);
        }
    }
    public bool IsOptionEnabled_7
    {
        get => _isOptionEnabled_7;
        set
        {
            _isOptionEnabled_7 = value;
            RaisePropertyChanged(() => IsOptionEnabled_7);
        }
    }
    public bool IsOptionEnabled_8
    {
        get => _isOptionEnabled_8;
        set
        {
            _isOptionEnabled_8 = value;
            RaisePropertyChanged(() => IsOptionEnabled_8);
        }
    }
    public bool IsOptionEnabled_9
    {
        get => _isOptionEnabled_9;
        set
        {
            _isOptionEnabled_9 = value;
            RaisePropertyChanged(() => IsOptionEnabled_9);
        }
    }
    public bool IsOptionEnabled_10
    {
        get => _isOptionEnabled_10;
        set
        {
            _isOptionEnabled_10 = value;
            RaisePropertyChanged(() => IsOptionEnabled_10);
        }
    }
    public bool IsOptionEnabled_11
    {
        get => _isOptionEnabled_11;
        set
        {
            _isOptionEnabled_11 = value;
            RaisePropertyChanged(() => IsOptionEnabled_11);
        }
    }
    public bool IsOptionEnabled_12
    {
        get => _isOptionEnabled_12;
        set
        {
            _isOptionEnabled_12 = value;
            RaisePropertyChanged(() => IsOptionEnabled_12);
        }
    }
    public bool IsOptionEnabled_Odds_Black
    {
        get => _isOptionEnabled_Odds_Black;
        set
        {
            _isOptionEnabled_Odds_Black = value;
            RaisePropertyChanged(() => IsOptionEnabled_Odds_Black);
        }
    }
    public bool IsOptionEnabled_Odds_Red
    {
        get => _isOptionEnabled_Odds_Red;
        set
        {
            _isOptionEnabled_Odds_Red = value;
            RaisePropertyChanged(() => IsOptionEnabled_Odds_Red);
        }
    }
    public bool IsWon_1
    {
        get => _isWon_1;
        set
        {
            _isWon_1 = value;
            RaisePropertyChanged(() => IsWon_1);
        }
    }
    public bool IsWon_2
    {
        get => _isWon_2;
        set
        {
            _isWon_2 = value;
            RaisePropertyChanged(() => IsWon_2);
        }
    }
    public bool IsWon_3
    {
        get => _isWon_3;
        set
        {
            _isWon_3 = value;
            RaisePropertyChanged(() => IsWon_3);
        }
    }
    public bool IsWon_4
    {
        get => _isWon_4;
        set
        {
            _isWon_4 = value;
            RaisePropertyChanged(() => IsWon_4);
        }
    }
    public bool IsWon_5
    {
        get => _isWon_5;
        set
        {
            _isWon_5 = value;
            RaisePropertyChanged(() => IsWon_5);
        }
    }
    public bool IsWon_6
    {
        get => _isWon_6;
        set
        {
            _isWon_6 = value;
            RaisePropertyChanged(() => IsWon_6);
        }
    }
    public bool IsWon_7
    {
        get => _isWon_7;
        set
        {
            _isWon_7 = value;
            RaisePropertyChanged(() => IsWon_7);
        }
    }
    public bool IsWon_8
    {
        get => _isWon_8;
        set
        {
            _isWon_8 = value;
            RaisePropertyChanged(() => IsWon_8);
        }
    }
    public bool IsWon_9
    {
        get => _isWon_9;
        set
        {
            _isWon_9 = value;
            RaisePropertyChanged(() => IsWon_9);
        }
    }
    public bool IsWon_10
    {
        get => _isWon_10;
        set
        {
            _isWon_10 = value;
            RaisePropertyChanged(() => IsWon_10);
        }
    }
    public bool IsWon_11
    {
        get => _isWon_11;
        set
        {
            _isWon_11 = value;
            RaisePropertyChanged(() => IsWon_11);
        }
    }
    public bool IsWon_12
    {
        get => _isWon_12;
        set
        {
            _isWon_12 = value;
            RaisePropertyChanged(() => IsWon_12);
        }
    }
    public bool IsOddsWon_Black
    {
        get => _isOddsWon_Black;
        set
        {
            _isOddsWon_Black = value;
            RaisePropertyChanged(() => IsOddsWon_Black);
        }
    }
    public bool IsOddsWon_Red
    {
        get => _isOddsWon_Red;
        set
        {
            _isOddsWon_Red = value;
            RaisePropertyChanged(() => IsOddsWon_Red);
        }
    }
    public bool ShowTotalBets
    {
        get => _showTotalBets;
        set
        {
            _showTotalBets = value;
            RaisePropertyChanged(() => ShowTotalBets);
        }
    }
    public string RunningOdds_Black_Percentage
    {
        get => _runningOdds_Black_Percentage;
        set
        {
            _runningOdds_Black_Percentage = value;
            RaisePropertyChanged(() => RunningOdds_Black_Percentage);
        }
    }
    public string RunningOdds_Red_Percentage
    {
        get => _runningOdds_Red_Percentage;
        set
        {
            _runningOdds_Red_Percentage = value;
            RaisePropertyChanged(() => RunningOdds_Red_Percentage);
        }
    }
    public string DuplicateBetvalue
    {
        get => _duplicateBetvalue;
        set
        {
            _duplicateBetvalue = value;
            RaisePropertyChanged(() => DuplicateBetvalue);
        }
    }
    public string TokenDiv
    {
        get => _tokenDiv;
        set
        {
            _tokenDiv = value;
            RaisePropertyChanged(() => TokenDiv);
        }
    }
    public string Go12gif
    {
        get => _go12gif;
        set
        {
            _go12gif = value;
            RaisePropertyChanged(() => Go12gif);
        }
    }
    #endregion

    #region GAME SETTINGS
    public GameSettingModel _gameSetting;


    public GameSettingModel GameSetting
    {
        get => _gameSetting;
        set
        {
            _gameSetting = value;
            RaisePropertyChanged(() => GameSetting);
        }
    }
    #endregion

    #region USER BETS
    private string _number_01_UserBet = "0";
    private string _number_02_UserBet = "0";
    private string _number_03_UserBet = "0";
    private string _number_04_UserBet = "0";
    private string _number_05_UserBet = "0";
    private string _number_06_UserBet = "0";
    private string _number_07_UserBet = "0";
    private string _number_08_UserBet = "0";
    private string _number_09_UserBet = "0";
    private string _number_10_UserBet = "0";
    private string _number_11_UserBet = "0";
    private string _number_12_UserBet = "0";
    private string _runningOdds_Black_Userbet = "0";
    private string _runningOdds_Red_Userbet = "0";


    public string Number_01_UserBet
    {
        get => _number_01_UserBet;
        set
        {
            _number_01_UserBet = value;
            RaisePropertyChanged(() => Number_01_UserBet);
        }
    }
    public string Number_02_UserBet
    {
        get => _number_02_UserBet;
        set
        {
            _number_02_UserBet = value;
            RaisePropertyChanged(() => Number_02_UserBet);
        }
    }
    public string Number_03_UserBet
    {
        get => _number_03_UserBet;
        set
        {
            _number_03_UserBet = value;
            RaisePropertyChanged(() => Number_03_UserBet);
        }
    }
    public string Number_04_UserBet
    {
        get => _number_04_UserBet;
        set
        {
            _number_04_UserBet = value;
            RaisePropertyChanged(() => Number_04_UserBet);
        }
    }
    public string Number_05_UserBet
    {
        get => _number_05_UserBet;
        set
        {
            _number_05_UserBet = value;
            RaisePropertyChanged(() => Number_05_UserBet);
        }
    }
    public string Number_06_UserBet
    {
        get => _number_06_UserBet;
        set
        {
            _number_06_UserBet = value;
            RaisePropertyChanged(() => Number_06_UserBet);
        }
    }
    public string Number_07_UserBet
    {
        get => _number_07_UserBet;
        set
        {
            _number_07_UserBet = value;
            RaisePropertyChanged(() => Number_07_UserBet);
        }
    }
    public string Number_08_UserBet
    {
        get => _number_08_UserBet;
        set
        {
            _number_08_UserBet = value;
            RaisePropertyChanged(() => Number_08_UserBet);
        }
    }
    public string Number_09_UserBet
    {
        get => _number_09_UserBet;
        set
        {
            _number_09_UserBet = value;
            RaisePropertyChanged(() => Number_09_UserBet);
        }
    }
    public string Number_10_UserBet
    {
        get => _number_10_UserBet;
        set
        {
            _number_10_UserBet = value;
            RaisePropertyChanged(() => Number_10_UserBet);
        }
    }
    public string Number_11_UserBet
    {
        get => _number_11_UserBet;
        set
        {
            _number_11_UserBet = value;
            RaisePropertyChanged(() => Number_11_UserBet);
        }
    }
    public string Number_12_UserBet
    {
        get => _number_12_UserBet;
        set
        {
            _number_12_UserBet = value;
            RaisePropertyChanged(() => Number_12_UserBet);
        }
    }
    public string RunningOdds_Black_Userbet
    {
        get => _runningOdds_Black_Userbet;
        set
        {
            _runningOdds_Black_Userbet = value;
            RaisePropertyChanged(() => RunningOdds_Black_Userbet);
        }
    }
    public string RunningOdds_Red_Userbet
    {
        get => _runningOdds_Red_Userbet;
        set
        {
            _runningOdds_Red_Userbet = value;
            RaisePropertyChanged(() => RunningOdds_Red_Userbet);
        }
    }
    #endregion

    #region TOTAL BETS
    private string _number_01_TotalBets = "0";
    private string _number_02_TotalBets = "0";
    private string _number_03_TotalBets = "0";
    private string _number_04_TotalBets = "0";
    private string _number_05_TotalBets = "0";
    private string _number_06_TotalBets = "0";
    private string _number_07_TotalBets = "0";
    private string _number_08_TotalBets = "0";
    private string _number_09_TotalBets = "0";
    private string _number_10_TotalBets = "0";
    private string _number_11_TotalBets = "0";
    private string _number_12_TotalBets = "0";
    private string _runningOdds_Black_TotalBet = "0";
    private string _runningOdds_Red_TotalBet = "0";


    public string Number_01_TotalBets
    {
        get => _number_01_TotalBets;
        set
        {
            _number_01_TotalBets = value;
            RaisePropertyChanged(() => Number_01_TotalBets);
        }
    }
    public string Number_02_TotalBets
    {
        get => _number_02_TotalBets;
        set
        {
            _number_02_TotalBets = value;
            RaisePropertyChanged(() => Number_02_TotalBets);
        }
    }
    public string Number_03_TotalBets
    {
        get => _number_03_TotalBets;
        set
        {
            _number_03_TotalBets = value;
            RaisePropertyChanged(() => Number_03_TotalBets);
        }
    }
    public string Number_04_TotalBets
    {
        get => _number_04_TotalBets;
        set
        {
            _number_04_TotalBets = value;
            RaisePropertyChanged(() => Number_04_TotalBets);
        }
    }
    public string Number_05_TotalBets
    {
        get => _number_05_TotalBets;
        set
        {
            _number_05_TotalBets = value;
            RaisePropertyChanged(() => Number_05_TotalBets);
        }
    }
    public string Number_06_TotalBets
    {
        get => _number_06_TotalBets;
        set
        {
            _number_06_TotalBets = value;
            RaisePropertyChanged(() => Number_06_TotalBets);
        }
    }
    public string Number_07_TotalBets
    {
        get => _number_07_TotalBets;
        set
        {
            _number_07_TotalBets = value;
            RaisePropertyChanged(() => Number_07_TotalBets);
        }
    }
    public string Number_08_TotalBets
    {
        get => _number_08_TotalBets;
        set
        {
            _number_08_TotalBets = value;
            RaisePropertyChanged(() => Number_08_TotalBets);
        }
    }
    public string Number_09_TotalBets
    {
        get => _number_09_TotalBets;
        set
        {
            _number_09_TotalBets = value;
            RaisePropertyChanged(() => Number_09_TotalBets);
        }
    }
    public string Number_10_TotalBets
    {
        get => _number_10_TotalBets;
        set
        {
            _number_10_TotalBets = value;
            RaisePropertyChanged(() => Number_10_TotalBets);
        }
    }
    public string Number_11_TotalBets
    {
        get => _number_11_TotalBets;
        set
        {
            _number_11_TotalBets = value;
            RaisePropertyChanged(() => Number_11_TotalBets);
        }
    }
    public string Number_12_TotalBets
    {
        get => _number_12_TotalBets;
        set
        {
            _number_12_TotalBets = value;
            RaisePropertyChanged(() => Number_12_TotalBets);
        }
    }
    public string RunningOdds_Black_TotalBet
    {
        get => _runningOdds_Black_TotalBet;
        set
        {
            _runningOdds_Black_TotalBet = value;
            RaisePropertyChanged(() => RunningOdds_Black_TotalBet);
        }
    }
    public string RunningOdds_Red_TotalBet
    {
        get => _runningOdds_Red_TotalBet;
        set
        {
            _runningOdds_Red_TotalBet = value;
            RaisePropertyChanged(() => RunningOdds_Red_TotalBet);
        }
    }
    #endregion

    #region PAYOUT
    private string _number_Payout_1 = "0";
    private string _number_Payout_2 = "0";
    private string _number_Payout_3 = "0";
    private string _number_Payout_4 = "0";
    private string _number_Payout_5 = "0";
    private string _number_Payout_6 = "0";
    private string _number_Payout_7 = "0";
    private string _number_Payout_8 = "0";
    private string _number_Payout_9 = "0";
    private string _number_Payout_10 = "0";
    private string _number_Payout_11 = "0";
    private string _number_Payout_12 = "0";


    public string Number_Payout_1
    {
        get => _number_Payout_1;
        set
        {
            _number_Payout_1 = value;
            RaisePropertyChanged(() => Number_Payout_1);
        }
    }
    public string Number_Payout_2
    {
        get => _number_Payout_2;
        set
        {
            _number_Payout_2 = value;
            RaisePropertyChanged(() => Number_Payout_2);
        }
    }
    public string Number_Payout_3
    {
        get => _number_Payout_3;
        set
        {
            _number_Payout_3 = value;
            RaisePropertyChanged(() => Number_Payout_3);
        }
    }
    public string Number_Payout_4
    {
        get => _number_Payout_4;
        set
        {
            _number_Payout_4 = value;
            RaisePropertyChanged(() => Number_Payout_4);
        }
    }
    public string Number_Payout_5
    {
        get => _number_Payout_5;
        set
        {
            _number_Payout_5 = value;
            RaisePropertyChanged(() => Number_Payout_5);
        }
    }
    public string Number_Payout_6
    {
        get => _number_Payout_6;
        set
        {
            _number_Payout_6 = value;
            RaisePropertyChanged(() => Number_Payout_6);
        }
    }
    public string Number_Payout_7
    {
        get => _number_Payout_7;
        set
        {
            _number_Payout_7 = value;
            RaisePropertyChanged(() => Number_Payout_7);
        }
    }
    public string Number_Payout_8
    {
        get => _number_Payout_8;
        set
        {
            _number_Payout_8 = value;
            RaisePropertyChanged(() => Number_Payout_8);
        }
    }
    public string Number_Payout_9
    {
        get => _number_Payout_9;
        set
        {
            _number_Payout_9 = value;
            RaisePropertyChanged(() => Number_Payout_9);
        }
    }
    public string Number_Payout_10
    {
        get => _number_Payout_10;
        set
        {
            _number_Payout_10 = value;
            RaisePropertyChanged(() => Number_Payout_10);
        }
    }
    public string Number_Payout_11
    {
        get => _number_Payout_11;
        set
        {
            _number_Payout_11 = value;
            RaisePropertyChanged(() => Number_Payout_11);
        }
    }
    public string Number_Payout_12
    {
        get => _number_Payout_12;
        set
        {
            _number_Payout_12 = value;
            RaisePropertyChanged(() => Number_Payout_12);
        }
    }
    #endregion

    #region WIN AMOUNT
    private string _black_Odds_WinAmount = "0";
    private string _red_Odds_WinAmount = "0";


    public string Black_Odds_WinAmount
    {
        get => _black_Odds_WinAmount;
        set
        {
            _black_Odds_WinAmount = value;
            RaisePropertyChanged(() => Black_Odds_WinAmount);
        }
    }
    public string Red_Odds_WinAmount
    {
        get => _red_Odds_WinAmount;
        set
        {
            _red_Odds_WinAmount = value;
            RaisePropertyChanged(() => Red_Odds_WinAmount);
        }
    }
    #endregion

    #region MULTIPLIER
    private decimal _fixedPriceMultiplier = 0;
    private decimal _g12FixedPriceMultiplier = 0;


    public decimal FixedPriceMultiplier
    {
        get => _fixedPriceMultiplier;
        set
        {
            _fixedPriceMultiplier = value;
            RaisePropertyChanged(() => FixedPriceMultiplier);
        }
    }
    public decimal G12VarFixedPriceMultiplier
    {
        get => _g12FixedPriceMultiplier;
        set
        {
            _g12FixedPriceMultiplier = value;
            RaisePropertyChanged(() => G12VarFixedPriceMultiplier);
        }
    }
    #endregion

    #endregion

    #region Properties

    public bool IsFixed_Black_Enabled
    {
        get; set;
    }
    public bool IsFixed_Red_Enabled
    {
        get; set;
    }

    public bool IsPayoutWon_Black
    {
        get; set;
    }
    public bool IsPayoutWon_Red
    {
        get; set;
    }

    public string FixedPrice_Black_TotalBets
    {
        get; set;
    }
    public string FixedPrice_Red_TotalBets
    {
        get; set;
    }

    public string SampleWinFixedPrize_Black
    {
        get; set;
    }
    public string SampleWinFixedPrize_Red
    {
        get; set;
    }

    public string UserTotalBet_FixedPrize_Black
    {
        get; set;
    }
    public string UserTotalBet_FixedPrize_Red
    {
        get; set;
    }

    public string FixedPrice_Black_Multiplier
    {
        get; set;
    }
    public string FixedPrice_Red_Multiplier
    {
        get; set;
    }

    public string G12RegFixedPrice_Black_Multiplier
    {
        get; set;
    }
    public string G12RegFixedPrice_Red_Multiplier
    {
        get; set;
    }

    private string _G12VarMinBet;
    private string _G12VarMaxBet;

    public string G12VarMinBet
    {
        get => _G12VarMinBet;
        set
        {
            _G12VarMinBet = value;
            RaisePropertyChanged(() => G12VarMinBet);
        }
    }
    public string G12VarMaxBet
    {
        get => _G12VarMaxBet;
        set
        {
            _G12VarMaxBet = value;
            RaisePropertyChanged(() => G12VarMaxBet);
        }
    }

    private GameVariantChipsModel _G12VargameChips;
    public GameVariantChipsModel G12VarGameChips
    {
        get => _G12VargameChips;
        set
        {
            _G12VargameChips = value;
            RaisePropertyChanged(() => G12VarGameChips);
        }
    }

    private GameVariantModel _G12RegSettings;
    public GameVariantModel G12RegSettings
    {
        get => _G12RegSettings;
        set
        {
            _G12RegSettings = value;
            RaisePropertyChanged(() => G12RegSettings);
        }
    }

    #endregion

    #region Trends
    private static int trendsRowLimit = 5;
    private ObservableCollection<TrendsDisplayModel> _payoutTrendsDisplay;
    private ObservableCollection<GameRoundModel> _trends;
    private ObservableCollection<TrendsDisplayModel> _oddsTrendsDisplay;
    public ObservableCollection<GameRoundModel> Trends
    {
        get => _trends;
        set
        {
            _trends = value;
            RaisePropertyChanged(() => Trends);
        }
    }
    public ObservableCollection<TrendsDisplayModel> PayoutTrendsDisplay
    {
        get => _payoutTrendsDisplay;
        set
        {
            _payoutTrendsDisplay = value;
            RaisePropertyChanged(() => PayoutTrendsDisplay);
        }
    }
    public class TrendsDisplayModel
    {
        public int ColumnIndex { get; set; }
        public int NextCount { get; set; }
        public bool IsLessThan10 => NextCount == 10 ? false : true;
        public ObservableCollection<GameRoundModel> CurrentList { get; set; }
    }
    public ObservableCollection<TrendsDisplayModel> OddsTrendsDisplay
    {
        get => _oddsTrendsDisplay;
        set
        {
            _oddsTrendsDisplay = value;
            RaisePropertyChanged(() => OddsTrendsDisplay);
        }
    }
    public static int payoutRowLimit = 10;
    public static int oddsRowLimit = 5;
    #endregion

    #region Lifecycle Methods
    public Go12ViewModel(ICurrentUser icurrentUser, IConfiguration iconfig, IGameRoundService igameRoundService, IToastService toastService, IAccountService iaccountService, IGameSettingService igameSettingService, NavigationManager navigationManager, AuthenticationStateProvider AuthenticationStateProvider, IMapper mapper)
    {
        _config = iconfig;
        _icurrentUser = icurrentUser;
        _igameRoundService = igameRoundService;
        _toastService = toastService;
        _iaccountService = iaccountService;
        _igameSettingService = igameSettingService;
        _navigationManager = navigationManager;
        _AuthenticationStateProvider = AuthenticationStateProvider;
        StreamId = Constants.StreamIDGo12;
        GametypeId = (int)GameTypes.Go_12;
        _mapper = mapper;

        ValidateUser();

    }
    #endregion

    #region Security
    public async Task ValidateUser()
    {

        var user = await _iaccountService.GetUser();
        if (user != null)
        {
            if (user.User.Id == 6216) return; //chamcy player

            if (user.User.DeviceToken != _icurrentUser.DeviceToken)
            {
                Logout();
                _toastService.ShowInfo("You have logged in on another device.");
            }
            _icurrentUser.Credits = user.User.Credits;
            _icurrentUser.CreditsDisp = string.Format("{0:0,0.00}", user.User.Credits);
        }
    }
    #endregion

    #region SignalR Methods
    public async Task AssignSignalRMethods()
    {
        if (HubConnection != null)
        {
            if (HubConnection.State == HubConnectionState.Connected)
            {
                HubConnection.Remove(Constants.UpdateGameTimer);
                HubConnection.Remove(Constants.UpdateGameStatus);
                HubConnection.Remove(Constants.NotifyGameRoundResult);
                HubConnection.Remove(Constants.UpdateTrends);
                HubConnection.Remove(Constants.UpdateBetValues);
                HubConnection.Remove(Constants.NotifyFixedCancelled);
                HubConnection.Remove(Constants.NotifyOddsCancelled);
                HubConnection.Remove(Constants.NotifyNumberBets);
                HubConnection.Remove(Constants.NotifyNumberOptions);
                HubConnection.Remove(Constants.NotifyEnableAllNumbers);

                HubConnection.On<int, string>(Constants.UpdateGameTimer, UpdateGameTimer);
                HubConnection.On<int, int, long>(Constants.UpdateGameStatus, UpdateGameStatus);
                HubConnection.On<UpdateGameResultModel>(Constants.NotifyGameRoundResult, NotifyGameRoundResult);
                HubConnection.On<int>(Constants.UpdateTrends, UpdateTrends);
                HubConnection.On<BetUpdatesModel>(Constants.UpdateBetValues, UpdateBetValues);


                HubConnection.On<long, int>(Constants.NotifyFixedCancelled, NotifyFixedCancelled);
                HubConnection.On<long, int>(Constants.NotifyOddsCancelled, NotifyOddsCancelled);

                HubConnection.On<int, int, decimal>(Constants.NotifyNumberBets, NotifyNumberBets);
                HubConnection.On<int, int, bool>(Constants.NotifyNumberOptions, NotifyNumberOptions);
                HubConnection.On<int>(Constants.NotifyEnableAllNumbers, NotifyEnableAllNumbers);
            }
        }
    }
    public override async void UpdateGameTimer(int gametypeId, string value)
    {
        try
        {
            if (GametypeId == gametypeId && GameRound is not null)
            {
                RoundTimer = value != null ? value : ""; // 00:00
                int timer;
                int.TryParse(RoundTimer.Replace(":", "").TrimStart(new char[] { '0' }), out timer);
                if (GameRound.RoundStatus == (int)RoundStatus.Open && timer < 10)
                {
                    ShowFlashing = "timerFlasher";
                }
                else
                {
                    ShowFlashing = "timerNotFlashing";
                }
                if (GameRound.RoundStatus == (int)RoundStatus.Closed && AwaitingGameRound == false)
                {
                    AwaitingGameRound = true;
                    await GetGameRound();
                    AwaitingGameRound = false;
                }
                CallInvoke();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex.ToString());
        }
    }
    protected async Task UpdateGameStatus(int gametypeId, int value, long gameRoundId)
    {
        if (gametypeId == GametypeId)
        {
            await GetGameRound();
            //if (gameRoundId != GameRound.Id && value == (int)RoundStatus.Open)
            //{
            //    await GetGameRound();
            //}
            //else if (gameRoundId == GameRound.Id && value != GameRound.RoundStatus && value != (int)RoundStatus.Open)
            //{
            //    await GetGameRound();
            //}
            //else
            //{
            //    return;
            //}

            if (value == (int)RoundStatus.Open)
            {
                TickerMessage = "";
                await GetCurrentRoundBets();
                await LoadDefaultValues();
                IsOptionEnabled_1 = true;
                IsOptionEnabled_2 = true;
                IsOptionEnabled_3 = true;
                IsOptionEnabled_4 = true;
                IsOptionEnabled_5 = true;
                IsOptionEnabled_6 = true;
                IsOptionEnabled_7 = true;
                IsOptionEnabled_8 = true;
                IsOptionEnabled_9 = true;
                IsOptionEnabled_10 = true;
                IsOptionEnabled_11 = true;
                IsOptionEnabled_12 = true;
                IsOptionEnabled_Odds_Black = true;
                IsOptionEnabled_Odds_Red = true;
                IsFixedPrizeCancelled = false;
                IsRunningOddsCancelled = false;
                IsFixed_Black_Enabled = true;
                IsFixed_Red_Enabled = true;

                UserBets = new ObservableCollection<BetModel>();

                Go12gif = "/img/animation/test-openbet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            else if (value == (int)RoundStatus.PendingResult)
            {
                Go12gif = "/img/animation/test-closebet.gif";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
            else
            {
                IsFixed_Black_Enabled = false;
                IsFixed_Red_Enabled = false;
                ShowFlashing = "timerNotFlashing";

                Go12gif = "";
                JsRuntime.InvokeVoidAsync("funcAnimation");
            }
        }
        await CallInvoke();
    }
    public async Task NotifyGameRoundResult(UpdateGameResultModel result)
    {
        if (result != null && result.GameTypeId == GametypeId)
        {
            GameResultString = result.WinningCombination;
            await CallInvoke();
        }
    }
    public async Task UpdateTrends(int _gameTypeId)
    {
        if (_gameTypeId == GametypeId)
        {
            await GetTrends();
            await CallInvoke();
        }
    }
    public async Task UpdateBetValues(BetUpdatesModel paramsModel)
    {
        if (GameRound == null) return;
        if (paramsModel.GameTypeId == GametypeId)
        {
            if (GameRound.RoundStatus == (int)RoundStatus.Open)
            {
                if (DateTime.Now.TimeOfDay.Subtract(lastBetUpdateReg).TotalSeconds > BetsDisplayDelay)
                {
                    lastBetUpdateReg = DateTime.Now.TimeOfDay;
                    RunningOdds_Black_TotalBet = paramsModel.RunningLeftBetValue.ToString("#,##0");
                    RunningOdds_Red_TotalBet = paramsModel.RunningRightBetValue.ToString("#,##0");
                    RunningOdds_Black_Percentage = paramsModel.LeftPercentage != 0 ? paramsModel.LeftPercentage.ToString("#.#0") + "%" : "0%";
                    RunningOdds_Red_Percentage = paramsModel.RightPercentage != 0 ? paramsModel.RightPercentage.ToString("#.#0") + "%" : "0%";
                    FixedPrice_Black_TotalBets = paramsModel.FixedLeftBetValue.ToString("#,##0");
                    FixedPrice_Red_TotalBets = paramsModel.FixedRightBetValue.ToString("#,##0");

                    //decimal oddBlackWinAmount = arg.LeftQuotient > 0 && arg.RunningLeftBetValue > 0 ? ( arg.LeftQuotient * arg.RunningLeftBetValue) : 0;
                    //decimal oddRedWinAmount = arg.RightQuotient > 0 && arg.RunningRightBetValue > 0 ? (arg.RightQuotient * arg.RunningRightBetValue) : 0;
                    decimal oddsBlackUserBet = !string.IsNullOrEmpty(RunningOdds_Black_Userbet) ? Convert.ToDecimal(RunningOdds_Black_Userbet) : 0;
                    decimal oddsRedUserBet = !string.IsNullOrEmpty(RunningOdds_Red_Userbet) ? Convert.ToDecimal(RunningOdds_Red_Userbet) : 0;
                    decimal oddBlackWinAmount = paramsModel.LeftQuotient > 0 && oddsBlackUserBet > 0 ? paramsModel.LeftQuotient * oddsBlackUserBet : 0;
                    decimal oddRedWinAmount = paramsModel.RightQuotient > 0 && oddsRedUserBet > 0 ? paramsModel.RightQuotient * oddsRedUserBet : 0;
                    Black_Odds_WinAmount = oddBlackWinAmount > 0 ? oddBlackWinAmount.ToString("#,###.#0") : "0";
                    Red_Odds_WinAmount = oddRedWinAmount > 0 ? oddRedWinAmount.ToString("#,###.#0") : "0";

                    await CallInvoke();
                }
            }
        }
    }
    public async Task NotifyFixedCancelled(long userId, int gameTypeId)
    {

        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsFixedPrizeCancelled = true;
            IsFixed_Black_Enabled = false;
            IsFixed_Red_Enabled = false;
            await CallInvoke();
        }

    }
    public async Task NotifyOddsCancelled(long userId, int gameTypeId)
    {

        if (gameTypeId == GametypeId && _icurrentUser.Id == userId)
        {
            IsRunningOddsCancelled = true;
            await CallInvoke();
        }

    }
    public async Task NotifyNumberBets(int gameTypeId, int number, decimal value)
    {
        if (gameTypeId == GametypeId)
        {
            switch (number)
            {
                case 1:
                    Number_01_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 2:
                    Number_02_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 3:
                    Number_03_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 4:
                    Number_04_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 5:
                    Number_05_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 6:
                    Number_06_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 7:
                    Number_07_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 8:
                    Number_08_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 9:
                    Number_09_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 10:
                    Number_10_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 11:
                    Number_11_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
                case 12:
                    Number_12_TotalBets = value > 0 ? value.ToString("#,##0") : "0";
                    break;
            }
            await CallInvoke();
        }
    }
    public async Task NotifyNumberOptions(int gameTypeId, int number, bool disabled)
    {
        if (GametypeId == gameTypeId)
        {
            switch (number)
            {
                case 1:
                    IsOptionEnabled_1 = false;
                    break;
                case 2:
                    IsOptionEnabled_2 = false;
                    break;
                case 3:
                    IsOptionEnabled_3 = false;
                    break;
                case 4:
                    IsOptionEnabled_4 = false;
                    break;
                case 5:
                    IsOptionEnabled_5 = false;
                    break;
                case 6:
                    IsOptionEnabled_6 = false;
                    break;
                case 7:
                    IsOptionEnabled_7 = false;
                    break;
                case 8:
                    IsOptionEnabled_8 = false;
                    break;
                case 9:
                    IsOptionEnabled_9 = false;
                    break;
                case 10:
                    IsOptionEnabled_10 = false;
                    break;
                case 11:
                    IsOptionEnabled_11 = false;
                    break;
                case 12:
                    IsOptionEnabled_12 = false;
                    break;
            }
            await CallInvoke();
        }
    }
    public async Task NotifyEnableAllNumbers(int gameTypeId)
    {
        if (GametypeId == gameTypeId)
        {
            IsOptionEnabled_1 = true;
            IsOptionEnabled_2 = true;
            IsOptionEnabled_3 = true;
            IsOptionEnabled_4 = true;
            IsOptionEnabled_5 = true;
            IsOptionEnabled_6 = true;
            IsOptionEnabled_7 = true;
            IsOptionEnabled_8 = true;
            IsOptionEnabled_9 = true;
            IsOptionEnabled_10 = true;
            IsOptionEnabled_11 = true;
            IsOptionEnabled_12 = true;
            await CallInvoke();
        }
    }
    public async Task NotifyFixedRightOptions(int gameTypeId, bool disabled)
    {
        if (GametypeId == gameTypeId)
        {
            if (GameRound != null)
            {

                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    IsFixed_Red_Enabled = !disabled;
                    IsFixedPrizeCancelled = false;
                    await CallInvoke();
                }
            }
        }
    }
    #endregion

    #region Game Methods
    public async Task ShowDivToken()
    {
        TokenDiv = "block";
        await CallInvoke();
    }
    public async Task HideDivToken()
    {
        TokenDiv = "none";
        await CallInvoke();
    }
    public async Task GetGameSetting()
    {
        try
        {
            if (GametypeId > 0)
            {
                UserTotalBet_FixedPrize_Black = "0";
                UserTotalBet_FixedPrize_Red = "0";

                var tempRoundSettings = await _igameSettingService.GetGameSettings(GametypeId);
                if (tempRoundSettings == null)
                {
                    _toastService.ShowInfo("No game settings found.");
                }
                else if (tempRoundSettings != null)
                {
                    GameSetting = tempRoundSettings;
                    FixedPriceMultiplier = GameSetting.FixedPriceMultiplier != null ? (decimal)GameSetting.FixedPriceMultiplier : 0;

                    FixedPrice_Black_Multiplier = GameSetting.FixedPriceMultiplier != null ? "x" + Convert.ToDecimal(GameSetting.FixedPriceMultiplier).ToString() : "x";
                    FixedPrice_Red_Multiplier = GameSetting.FixedPriceMultiplier != null ? "x" + Convert.ToDecimal(GameSetting.FixedPriceMultiplier).ToString() : "x";


                    MinBet = GameSetting.MinimumBet != null ? GameSetting.MinimumBet.Value.ToString("#,##0") : "0";
                    MaxBet = GameSetting.MaximumBet != null ? GameSetting.MaximumBet.Value.ToString("#,##0") : "0";

                }
            }
        }
        catch (Exception ex)
        {
            // Console.WriteLine(ex);
        }
    }
    public async Task GetGameSettingVariant()
    {
        try
        {
            var tempVariant = await _igameSettingService.GetGameSettingsVariant(GametypeId);
            if (tempVariant == null)
            {
                _toastService.ShowInfo("No game variant found.");
            }
            else if (tempVariant != null)
            {
                foreach (var item in tempVariant)
                {
                    if (item.GameVariantID == (int)GameVariant.G12Reg)
                    {
                        G12RegSettings = item;

                        G12VarFixedPriceMultiplier = G12RegSettings.FixedPriceMultiplier != null ? G12RegSettings.FixedPriceMultiplier : 0;

                        G12RegFixedPrice_Black_Multiplier = G12RegSettings.FixedPriceMultiplier != null ? "x" + Convert.ToDecimal(G12RegSettings.FixedPriceMultiplier).ToString() : "x";
                        G12RegFixedPrice_Red_Multiplier = G12RegSettings.FixedPriceMultiplier != null ? "x" + Convert.ToDecimal(G12RegSettings.FixedPriceMultiplier).ToString() : "x";


                        G12VarMinBet = G12RegSettings.MinimumBet != null ? G12RegSettings.MinimumBet.ToString("#,##0") : "0";
                        G12VarMaxBet = G12RegSettings.MaximumBet != null ? G12RegSettings.MaximumBet.ToString("#,##0") : "0";

                    }

                }
            }
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task GetGameVariantChipsByCategory()
    {
        var tempGameVariantChips = await _igameSettingService.GetGameVariantChipsByCategory(GametypeId, _icurrentUser.CategoryId);
        if (tempGameVariantChips == null)
        {
            _toastService.ShowInfo("No game variant chips found.");
            return;
        }
        foreach (var item in tempGameVariantChips)
        {
            int[] chips = { item.Chip6 != null || item.Chip6 != 0 ? item.Chip6 : 0,
                        item.Chip7 != null || item.Chip7 != 0 ? item.Chip7 : 0,
                        item.Chip8 != null || item.Chip8 != 0 ? item.Chip8 : 0,
                        item.Chip9 != null || item.Chip9 != 0 ? item.Chip9 : 0,
                        item.Chip10 != null || item.Chip10 != 0 ? item.Chip10 : 0
                      };
            if (item.GameVariantId == (int)GameVariant.G12Reg)
            {
                G12VarGameChips = item;
                MaxChip = chips.Max();
            }
        }

        await CallInvoke();
    }
    public async Task LoadDefaultValues()
    {
        Number_01_UserBet = "0";
        Number_02_UserBet = "0";
        Number_03_UserBet = "0";
        Number_04_UserBet = "0";
        Number_05_UserBet = "0";
        Number_06_UserBet = "0";
        Number_07_UserBet = "0";
        Number_08_UserBet = "0";
        Number_09_UserBet = "0";
        Number_10_UserBet = "0";
        Number_11_UserBet = "0";
        Number_12_UserBet = "0";


        Number_Payout_1 = "0";
        Number_Payout_2 = "0";
        Number_Payout_3 = "0";
        Number_Payout_4 = "0";
        Number_Payout_5 = "0";
        Number_Payout_6 = "0";
        Number_Payout_7 = "0";
        Number_Payout_8 = "0";
        Number_Payout_9 = "0";
        Number_Payout_10 = "0";
        Number_Payout_11 = "0";
        Number_Payout_12 = "0";

        IsOptionEnabled_1 = false;
        IsOptionEnabled_2 = false;
        IsOptionEnabled_3 = false;
        IsOptionEnabled_4 = false;
        IsOptionEnabled_5 = false;
        IsOptionEnabled_6 = false;
        IsOptionEnabled_7 = false;
        IsOptionEnabled_8 = false;
        IsOptionEnabled_9 = false;
        IsOptionEnabled_10 = false;
        IsOptionEnabled_11 = false;
        IsOptionEnabled_12 = false;
        IsOptionEnabled_Odds_Black = false;
        IsOptionEnabled_Odds_Red = false;

        RunningOdds_Black_Userbet = "0";
        RunningOdds_Red_Userbet = "0";

        RunningOdds_Black_TotalBet = "0";
        RunningOdds_Red_TotalBet = "0";
        Black_Odds_WinAmount = "0";
        Red_Odds_WinAmount = "0";

        SampleWinFixedPrize_Black = "";
        SampleWinFixedPrize_Red = "";
        G12RegFixedPrice_Black_Multiplier = "";
        G12RegFixedPrice_Red_Multiplier = "";
        UserTotalBet_FixedPrize_Black = "0";
        UserTotalBet_FixedPrize_Red = "0";

        if (GameSetting != null)
        {
            RunningOdds_Black_Percentage = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
            RunningOdds_Red_Percentage = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage).ToString("#.#0") + "%" : "0%";
        }
    }
    public async Task GetGameRound()
    {
        TotalBets = 0;
        if (GametypeId == (int)GameTypes.Go_12)
        {
            if (GameSetting == null)
            {
                await GetGameSetting();
            }
            if (G12RegSettings == null)
            {
                await GetGameSettingVariant();
            }

            //gamechips by player category
            GameChips = Go12GameChips;

            FixedPrice_Black_TotalBets = "0";
            FixedPrice_Red_TotalBets = "0";
            IsPayoutWon_Black = false;
            IsPayoutWon_Red = false;

            var sampleBet = 0;
            if (G12RegSettings != null)
            {
                //decimal FixedPriceMultiplier = Convert.ToDecimal(G12RegSettings.FixedPriceMultiplier);
                //SampleWinFixedPrize_Black = (sampleBet * FixedPriceMultiplier).ToString("0");
                //SampleWinFixedPrize_Red = (sampleBet * FixedPriceMultiplier).ToString("0");
            }

            var tempRound = await _igameRoundService.GetRound(GametypeId);
            if (tempRound == null)
            {
                _toastService.ShowError("No round details found.");
                await GetGameSetting();
                await LoadDefaultValues();
                return;
            }
            else if (tempRound != null)
            {
                GameRound = tempRound;

                GameResultString = !string.IsNullOrEmpty(GameRound.WinningResult) ? GameRound.WinningResult : null;
                FixedPrice_Black_TotalBets = GameRound.FixedLeftBet;
                FixedPrice_Red_TotalBets = GameRound.FixedRightBet;

                if (GameRound.RoundNumber > 0)
                {
                    RoundNumber = GameRound.RoundNumber;
                }
                if (GameRound.RoundStatus == (int)RoundStatus.Open)
                {
                    GameResultString = null;
                    RoundTimer = ""; // 00:00
                    RoundStatusString = Constants.Open;
                    RoundStatusColor = Constants.GameOpenColor;

                    IsWon_1 = false;
                    IsWon_2 = false;
                    IsWon_3 = false;
                    IsWon_4 = false;
                    IsWon_5 = false;
                    IsWon_6 = false;
                    IsWon_7 = false;
                    IsWon_8 = false;
                    IsWon_9 = false;
                    IsWon_10 = false;
                    IsWon_11 = false;
                    IsWon_12 = false;
                    IsOddsWon_Black = false;
                    IsOddsWon_Red = false;

                    IsOptionEnabled_1 = true;
                    IsOptionEnabled_2 = true;
                    IsOptionEnabled_3 = true;
                    IsOptionEnabled_4 = true;
                    IsOptionEnabled_5 = true;
                    IsOptionEnabled_6 = true;
                    IsOptionEnabled_7 = true;
                    IsOptionEnabled_8 = true;
                    IsOptionEnabled_9 = true;
                    IsOptionEnabled_10 = true;
                    IsOptionEnabled_11 = true;
                    IsOptionEnabled_12 = true;
                    IsOptionEnabled_Odds_Black = true;
                    IsOptionEnabled_Odds_Red = true;

                    await GetRoundBetSummary();
                    await SetTotalBetsPerNumber(GameRound.Go12Bets);

                    RunningOdds_Black_TotalBet = !string.IsNullOrEmpty(GameRound.OddsLeftBet) ? GameRound.OddsLeftBet : "0";
                    RunningOdds_Red_TotalBet = !string.IsNullOrEmpty(GameRound.OddsRightBet) ? GameRound.OddsRightBet : "0";
                    RunningOdds_Black_Percentage = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) ? GameRound.OddsLeftPercentage : "0%";
                    RunningOdds_Red_Percentage = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) ? GameRound.OddsRightPercentage : "0%";

                    decimal blackUserBet = !string.IsNullOrEmpty(RunningOdds_Black_Userbet) ? Convert.ToDecimal(RunningOdds_Black_Userbet) : 0;
                    decimal redUserBet = !string.IsNullOrEmpty(RunningOdds_Red_Userbet) ? Convert.ToDecimal(RunningOdds_Red_Userbet) : 0;
                    decimal oddBlackWinAmount = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) && Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) > 0 && blackUserBet > 0 ? Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) / 100 * blackUserBet : 0;
                    decimal oddRedWinAmount = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) && Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) > 0 && redUserBet > 0 ? Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) / 100 * redUserBet : 0;
                    Black_Odds_WinAmount = oddBlackWinAmount > 0 ? oddBlackWinAmount.ToString("#,###.#0") : "0";
                    Red_Odds_WinAmount = oddRedWinAmount > 0 ? oddRedWinAmount.ToString("#,###.#0") : "0";

                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Closed)
                {
                    IsWon_1 = false;
                    IsWon_2 = false;
                    IsWon_3 = false;
                    IsWon_4 = false;
                    IsWon_5 = false;
                    IsWon_6 = false;
                    IsWon_7 = false;
                    IsWon_8 = false;
                    IsWon_9 = false;
                    IsWon_10 = false;
                    IsWon_11 = false;
                    IsWon_12 = false;
                    IsOddsWon_Black = false;
                    IsOddsWon_Red = false;
                    RoundTimer = ""; // 00:00

                    //await GetTrends();
                    if (!string.IsNullOrEmpty(GameRound.WinningResult))
                    {
                        if (GameRound.WinningResult.Contains(Constants.Black))
                        {
                            if (!GameRound.FixedCancelled)
                            {
                                IsPayoutWon_Black = true;
                            }

                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Red))
                        {
                            if (!GameRound.FixedCancelled)
                            {
                                IsPayoutWon_Red = true;
                            }

                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }

                        if (GameRound.WinningResult.Contains(Constants.One))
                        {
                            IsWon_1 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Two))
                        {
                            IsWon_2 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Three))
                        {
                            IsWon_3 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Four))
                        {
                            IsWon_4 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Five))
                        {
                            IsWon_5 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Six))
                        {
                            IsWon_6 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Seven))
                        {
                            IsWon_7 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Eight))
                        {
                            IsWon_8 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Nine))
                        {
                            IsWon_9 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Ten))
                        {
                            IsWon_10 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Eleven))
                        {
                            IsWon_11 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Red = true;
                            }
                        }
                        else if (GameRound.WinningResult.Contains(Constants.Twelve))
                        {
                            IsWon_12 = true;
                            if (!GameRound.RunningOddsCancelled)
                            {
                                IsOddsWon_Black = true;
                            }
                        }
                    }
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = RoundStatusColor = Constants.GameClosedColor;

                    IsOptionEnabled_1 = false;
                    IsOptionEnabled_2 = false;
                    IsOptionEnabled_3 = false;
                    IsOptionEnabled_4 = false;
                    IsOptionEnabled_5 = false;
                    IsOptionEnabled_6 = false;
                    IsOptionEnabled_7 = false;
                    IsOptionEnabled_8 = false;
                    IsOptionEnabled_9 = false;
                    IsOptionEnabled_10 = false;
                    IsOptionEnabled_11 = false;
                    IsOptionEnabled_12 = false;
                    IsOptionEnabled_Odds_Black = false;
                    IsOptionEnabled_Odds_Red = false;

                    await GetRoundBetSummary();
                    await SetTotalBetsPerNumber(GameRound.Go12Bets);

                    RunningOdds_Black_TotalBet = !string.IsNullOrEmpty(GameRound.OddsLeftBet) ? GameRound.OddsLeftBet : "0";
                    RunningOdds_Red_TotalBet = !string.IsNullOrEmpty(GameRound.OddsRightBet) ? GameRound.OddsRightBet : "0";
                    RunningOdds_Black_Percentage = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) ? GameRound.OddsLeftPercentage : "0%";
                    RunningOdds_Red_Percentage = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) ? GameRound.OddsRightPercentage : "0%";

                    decimal blackUserBet = !string.IsNullOrEmpty(RunningOdds_Black_Userbet) ? Convert.ToDecimal(RunningOdds_Black_Userbet) : 0;
                    decimal redUserBet = !string.IsNullOrEmpty(RunningOdds_Red_Userbet) ? Convert.ToDecimal(RunningOdds_Red_Userbet) : 0;
                    decimal oddBlackWinAmount = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) && Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) > 0 && blackUserBet > 0 ? Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) / 100 * blackUserBet : 0;
                    decimal oddRedWinAmount = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) && Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) > 0 && redUserBet > 0 ? Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) / 100 * redUserBet : 0;
                    Black_Odds_WinAmount = oddBlackWinAmount > 0 ? oddBlackWinAmount.ToString("#,###.#0") : "0";
                    Red_Odds_WinAmount = oddRedWinAmount > 0 ? oddRedWinAmount.ToString("#,###.#0") : "0";

                    IsFixedPrizeCancelled = GameRound.FixedCancelled;
                    IsRunningOddsCancelled = GameRound.RunningOddsCancelled;

                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Paused)
                {
                    RoundStatusString = Constants.Paused;
                    RoundStatusColor = Constants.GamePausedColor;

                    IsOptionEnabled_1 = false;
                    IsOptionEnabled_2 = false;
                    IsOptionEnabled_3 = false;
                    IsOptionEnabled_4 = false;
                    IsOptionEnabled_5 = false;
                    IsOptionEnabled_6 = false;
                    IsOptionEnabled_7 = false;
                    IsOptionEnabled_8 = false;
                    IsOptionEnabled_9 = false;
                    IsOptionEnabled_10 = false;
                    IsOptionEnabled_11 = false;
                    IsOptionEnabled_12 = false;
                    IsOptionEnabled_Odds_Black = false;
                    IsOptionEnabled_Odds_Red = false;

                    await GetRoundBetSummary();
                    await SetTotalBetsPerNumber(GameRound.Go12Bets);

                    RunningOdds_Black_TotalBet = !string.IsNullOrEmpty(GameRound.OddsLeftBet) ? GameRound.OddsLeftBet : "0";
                    RunningOdds_Red_TotalBet = !string.IsNullOrEmpty(GameRound.OddsRightBet) ? GameRound.OddsRightBet : "0";
                    RunningOdds_Black_Percentage = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) ? GameRound.OddsLeftPercentage : "0%";
                    RunningOdds_Red_Percentage = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) ? GameRound.OddsRightPercentage : "0%";

                    decimal blackUserBet = !string.IsNullOrEmpty(RunningOdds_Black_Userbet) ? Convert.ToDecimal(RunningOdds_Black_Userbet) : 0;
                    decimal redUserBet = !string.IsNullOrEmpty(RunningOdds_Red_Userbet) ? Convert.ToDecimal(RunningOdds_Red_Userbet) : 0;
                    decimal oddBlackWinAmount = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) && Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) > 0 && blackUserBet > 0 ? Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) / 100 * blackUserBet : 0;
                    decimal oddRedWinAmount = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) && Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) > 0 && redUserBet > 0 ? Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) / 100 * redUserBet : 0;
                    Black_Odds_WinAmount = oddBlackWinAmount > 0 ? oddBlackWinAmount.ToString("#,###.#0") : "0";
                    Red_Odds_WinAmount = oddRedWinAmount > 0 ? oddRedWinAmount.ToString("#,###.#0") : "0";
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.Cancelled)
                {
                    RoundTimer = ""; // 00:00
                    RoundStatusString = Constants.Cancelled;
                    RoundStatusColor = Constants.GameCancelledColor;
                    IsOptionEnabled_1 = false;
                    IsOptionEnabled_2 = false;
                    IsOptionEnabled_3 = false;
                    IsOptionEnabled_4 = false;
                    IsOptionEnabled_5 = false;
                    IsOptionEnabled_6 = false;
                    IsOptionEnabled_7 = false;
                    IsOptionEnabled_8 = false;
                    IsOptionEnabled_9 = false;
                    IsOptionEnabled_10 = false;
                    IsOptionEnabled_11 = false;
                    IsOptionEnabled_12 = false;
                    IsOptionEnabled_Odds_Black = false;
                    IsOptionEnabled_Odds_Red = false;

                    IsFixedPrizeCancelled = true;
                    IsRunningOddsCancelled = true;
                }
                else if (GameRound.RoundStatus == (int)RoundStatus.PendingResult)
                {
                    RoundStatusString = Constants.Closed;
                    RoundStatusColor = Constants.GameClosedColor;
                    RoundTimer = ""; // 00:00
                    IsOptionEnabled_1 = false;
                    IsOptionEnabled_2 = false;
                    IsOptionEnabled_3 = false;
                    IsOptionEnabled_4 = false;
                    IsOptionEnabled_5 = false;
                    IsOptionEnabled_6 = false;
                    IsOptionEnabled_7 = false;
                    IsOptionEnabled_8 = false;
                    IsOptionEnabled_9 = false;
                    IsOptionEnabled_10 = false;
                    IsOptionEnabled_11 = false;
                    IsOptionEnabled_12 = false;
                    IsOptionEnabled_Odds_Black = false;
                    IsOptionEnabled_Odds_Red = false;

                    await GetRoundBetSummary();
                    await SetTotalBetsPerNumber(GameRound.Go12Bets);

                    RunningOdds_Black_TotalBet = !string.IsNullOrEmpty(GameRound.OddsLeftBet) ? GameRound.OddsLeftBet : "0";
                    RunningOdds_Red_TotalBet = !string.IsNullOrEmpty(GameRound.OddsRightBet) ? GameRound.OddsRightBet : "0";
                    RunningOdds_Black_Percentage = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) ? GameRound.OddsLeftPercentage : "0%";
                    RunningOdds_Red_Percentage = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) ? GameRound.OddsRightPercentage : "0%";

                    decimal blackUserBet = !string.IsNullOrEmpty(RunningOdds_Black_Userbet) ? Convert.ToDecimal(RunningOdds_Black_Userbet) : 0;
                    decimal redUserBet = !string.IsNullOrEmpty(RunningOdds_Red_Userbet) ? Convert.ToDecimal(RunningOdds_Red_Userbet) : 0;
                    decimal oddBlackWinAmount = !string.IsNullOrEmpty(GameRound.OddsLeftPercentage) && Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) > 0 && blackUserBet > 0 ? Convert.ToDecimal(GameRound.OddsLeftPercentage.Replace("%", " ")) / 100 * blackUserBet : 0;
                    decimal oddRedWinAmount = !string.IsNullOrEmpty(GameRound.OddsRightPercentage) && Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) > 0 && redUserBet > 0 ? Convert.ToDecimal(GameRound.OddsRightPercentage.Replace("%", " ")) / 100 * redUserBet : 0;
                    Black_Odds_WinAmount = oddBlackWinAmount > 0 ? oddBlackWinAmount.ToString("#,###.#0") : "0";
                    Red_Odds_WinAmount = oddRedWinAmount > 0 ? oddRedWinAmount.ToString("#,###.#0") : "0";

                    IsFixedPrizeCancelled = GameRound.FixedCancelled;
                    IsRunningOddsCancelled = GameRound.RunningOddsCancelled;
                }

            }
        }
    }
    public async Task GetRoundBetSummary()
    {
        try
        {
            var roundBetDetail = await _igameRoundService.GetBetSummaryOnRound(_icurrentUser.Id, GametypeId);
            if (roundBetDetail != null)
            {
                int totalNumber = 0;
                if (roundBetDetail.Go12Bets != null && roundBetDetail.Go12Bets.Count > 0)
                {

                    foreach (var g in roundBetDetail.Go12Bets)
                    {
                        if (g.Number == int.Parse(Constants.One))
                        {
                            Number_01_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_1 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet.Replace(",", "")) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Two))
                        {
                            Number_02_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_2 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Three))
                        {
                            Number_03_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_3 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Four))
                        {
                            Number_04_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_4 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Five))
                        {
                            Number_05_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_5 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Six))
                        {
                            Number_06_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_6 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Seven))
                        {
                            Number_07_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_7 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Eight))
                        {
                            Number_08_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_8 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Nine))
                        {
                            Number_09_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_9 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Ten))
                        {
                            Number_10_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_10 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Eleven))
                        {
                            Number_11_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_11 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        else if (g.Number == int.Parse(Constants.Twelve))
                        {
                            Number_12_UserBet = !string.IsNullOrEmpty(g.Bet) ? g.Bet : "0";
                            Number_Payout_12 = !string.IsNullOrEmpty(g.Bet) ? (Convert.ToDecimal(g.Bet) * FixedPriceMultiplier).ToString("#,##0") : "0";
                        }
                        totalNumber += int.Parse(!string.IsNullOrEmpty(g.Bet) ? g.Bet.Replace(",", "") : "0");
                    }

                    UserTotalBet_FixedPrize_Black = !string.IsNullOrEmpty(roundBetDetail.LeftFixedBet) ? roundBetDetail.LeftFixedBet : "0";
                    SampleWinFixedPrize_Black = roundBetDetail.LeftFixedWinnings;

                    UserTotalBet_FixedPrize_Red = !string.IsNullOrEmpty(roundBetDetail.RightFixedBet) ? roundBetDetail.RightFixedBet : "0";
                    SampleWinFixedPrize_Red = roundBetDetail.RightFixedWinnings;


                }
                TotalBets = totalNumber + int.Parse(UserTotalBet_FixedPrize_Black) + int.Parse(UserTotalBet_FixedPrize_Red);
                RunningOdds_Black_Userbet = !string.IsNullOrEmpty(roundBetDetail.LeftOddsBet) ? roundBetDetail.LeftOddsBet : "0";
                RunningOdds_Red_Userbet = !string.IsNullOrEmpty(roundBetDetail.RightOddsBet) ? roundBetDetail.RightOddsBet : "0";

                decimal oddsPercentage = GameSetting.RunningOddsPercentage != null ? Convert.ToDecimal(GameSetting.RunningOddsPercentage) : 0;
                decimal LeftOddsBet = !string.IsNullOrEmpty(roundBetDetail.LeftOddsBet) ? Convert.ToDecimal(roundBetDetail.LeftOddsBet) : 0;
                decimal RightOddsBet = !string.IsNullOrEmpty(roundBetDetail.RightOddsBet) ? Convert.ToDecimal(roundBetDetail.RightOddsBet) : 0;
                decimal oddBlackWinAmount = oddsPercentage > 0 && LeftOddsBet > 0 ? oddsPercentage / 100 * LeftOddsBet : 0;
                decimal oddRedWinAmount = oddsPercentage > 0 && RightOddsBet > 0 ? oddsPercentage / 100 * RightOddsBet : 0;
                Black_Odds_WinAmount = oddBlackWinAmount > 0 ? oddBlackWinAmount.ToString("#,###.#0") : "0";
                Red_Odds_WinAmount = oddRedWinAmount > 0 ? oddRedWinAmount.ToString("#,###.#0") : "0";
            }


        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }
    }
    public async Task SetTotalBetsPerNumber(List<Go12Model> Go12Bets)
    {
        if (Go12Bets != null && Go12Bets.Count > 0)
        {
            foreach (var g in Go12Bets)
            {
                if (g.Number == int.Parse(Constants.One))
                    Number_01_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Two))
                    Number_02_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Three))
                    Number_03_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Four))
                    Number_04_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Five))
                    Number_05_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Six))
                    Number_06_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Seven))
                    Number_07_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Eight))
                    Number_08_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Nine))
                    Number_09_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Ten))
                    Number_10_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Eleven))
                    Number_11_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
                else if (g.Number == int.Parse(Constants.Twelve))
                    Number_12_TotalBets = g.NumberBetValue > 0 ? g.NumberBetValue.ToString("#,##0") : "0";
            }
        }
    }
    public async Task GetCurrentRoundBets()
    {
        var tempRoundBets = await _igameRoundService.GetBets(_icurrentUser.Id, GametypeId, null);
        if (tempRoundBets != null)
        {
            UserBets = new ObservableCollection<BetModel>(tempRoundBets);
        }
    }
    public async Task GetTrends()
    {
        var tempTrends = await _igameRoundService.GetTrends(GametypeId);
        if (tempTrends != null)
        {
            await SetPayoutTrendsDisplay(tempTrends);
            //await SetRunningOddsTrendsDisplay(tempTrends);
        }
    }
    public async Task SetPayoutTrendsDisplay(List<GameRoundModel> tempTrends)
    {
        if (tempTrends != null && tempTrends.Count > 0)
        {
            ObservableCollection<TrendsDisplayModel> resultHolder = new ObservableCollection<TrendsDisplayModel>();
            ObservableCollection<GameRoundModel> CurrentList = new ObservableCollection<GameRoundModel>();
            TrendsDisplayModel Current = new TrendsDisplayModel();
            int counter = 0;
            int rowCounter = 0;
            int indexCounter = 0;

            foreach (var t in tempTrends.OrderBy(x => x.Id))
            {
                counter++;
                rowCounter++;
                if (rowCounter != trendsRowLimit)
                {
                    if (counter != tempTrends.Count)
                    {
                        CurrentList.Add(t);
                    }
                    else
                    {
                        CurrentList.Add(t);
                        Current = new TrendsDisplayModel();
                        indexCounter++;
                        Current.ColumnIndex = indexCounter;
                        Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderByDescending(x => x.Id));
                        resultHolder.Add(Current);

                        CurrentList = new ObservableCollection<GameRoundModel>();

                        rowCounter = 0;
                        counter = 0;
                    }
                }
                else if (rowCounter == trendsRowLimit)
                {
                    CurrentList.Add(t);
                    Current = new TrendsDisplayModel();
                    indexCounter++;
                    Current.ColumnIndex = indexCounter;
                    Current.CurrentList = new ObservableCollection<GameRoundModel>(CurrentList.OrderByDescending(x => x.Id));
                    resultHolder.Add(Current);

                    CurrentList = new ObservableCollection<GameRoundModel>();

                    rowCounter = 0;
                }
            }

            if (resultHolder != null)
            {
                PayoutTrendsDisplay = new ObservableCollection<TrendsDisplayModel>(resultHolder.OrderByDescending(x => x.ColumnIndex));
            }
        }
    }

    #region Bet Methods
    public async Task SetGameChips(int GameVariantId, bool isSubGames)
    {
        if (!isSubGames)
        {
            GameChips = Go12GameChips;
        }
        else
        {
            switch (GameVariantId)
            {
                case (int)GameVariant.G12Reg:
                    GameChips = _mapper.Map<GameChipModel>(G12VarGameChips);
                    break;
            }
        }

        await CallInvoke();
    }
    public async Task SetBetSelectedValue(object value)
    {
        BetAmount = (int)value;

        switch (BetCombinationValue)
        {
            case Constants.FixedRed:
            case Constants.FixedBlack:
                Payout = Convert.ToDecimal(BetAmount) * G12RegSettings.FixedPriceMultiplier;
                break;
            default:
                Payout = Convert.ToDecimal(BetAmount) * GameSetting.FixedPriceMultiplier.Value;
                break;
        }
    }
    public async Task<bool> IsDoubleBet()
    {
        bool result = false;
        if (UserBets != null && UserBets.Count > 0)
        {
            var duplicateBets = UserBets.Where(x => x.BetValue.ToLower().Contains(BetCombinationValue.ToLower())).ToList();
            if (duplicateBets != null && duplicateBets.Count > 0)
            {
                DuplicateBetvalue = duplicateBets.FirstOrDefault().BetValue;
                result = true;
            }
        }
        return result;
    }
    public async Task<bool> ValidateBet()
    {
        //check if divisible by 10
        var result = BetAmount % 5;
        if (result > 0)
        {
            _toastService.ShowError("Bet amount should be divisible by 5.");
            return false;
        }
        // CEHCK Min and Max Bet

        if (GameVariantId > 0)
        {
            //For Regular
            if (BetAmount < G12RegSettings.MinimumBet)
            {
                _toastService.ShowError("Below Minimum Bet");
                return false;
            }
            else if (BetAmount > G12RegSettings.MaximumBet)
            {
                _toastService.ShowError("Above Maximum Bet");
                return false;
            }
        }
        else
        {
            //For Numbers
            if (BetAmount < GameSetting.MinimumBet.Value)
            {
                _toastService.ShowError("Below Minimum Bet");
                return false;
            }
            else if (BetAmount > GameSetting.MaximumBet.Value)
            {
                _toastService.ShowError("Above Maximum Bet");
                return false;
            }
        }

        if (BetAmount > _icurrentUser.Credits)
        {
            popupModal.Show<PopupAddCredit>("Insufficient Credits", new ModalOptions() { Class = "op-modal", HideHeader = false });
            return false;
        }

        return true;
    }
    public async Task SubmitBet()
    {
        try
        {
            BetDTO betParams = new BetDTO();
            betParams.GameTypeId = GametypeId;
            betParams.GameRoundId = GameRound.Id;
            betParams.UserId = _icurrentUser.Id;
            betParams.BetAmount = BetAmount;
            betParams.BetValue = BetCombinationValue;

            var popupRes = popupModal.Show<PopupLoading>("");
            var tempBetResult = await _igameRoundService.BetOnRound(betParams);
            if (tempBetResult == null)
            {
                popupRes.Close();
                _toastService.ShowError("Unable to place bet.");
                await HideDivToken();
                return;
            }
            else if (tempBetResult != null && tempBetResult.Bet == null)
            {
                popupRes.Close();
                _toastService.ShowError(tempBetResult.Message);
                await HideDivToken();
                return;
            }
            else if (tempBetResult != null)
            {
                popupRes.Close();

                TokenDiv = "none";
                BetAmount = 0;
                BetCombinationValue = string.Empty;

                if (BetCombinationValue.ToUpper().Contains(Constants.FixedBlack))
                {
                    await UpdateFixed_Black_BetAmount(GametypeId, BetAmount);
                }
                else if (BetCombinationValue.ToUpper().Contains(Constants.FixedRed))
                {
                    await UpdateFixed_Red_BetAmount(GametypeId, BetAmount);
                }

                await GetCurrentRoundBets();
                await GetRoundBetSummary();
            }
            await GetGameRound();
            await ValidateUser();
            await CallInvoke();
        }
        catch (Exception ex)
        {
            //Console.WriteLine(ex);
        }

    }
    public async Task UpdateFixed_Black_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal FixedPriceMultiplier = Convert.ToDecimal(G12RegSettings.FixedPriceMultiplier);
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrize_Black) ? decimal.Parse(UserTotalBet_FixedPrize_Black) + Amount : Amount;
            UserTotalBet_FixedPrize_Black = totalBets.ToString("#,##0");
            SampleWinFixedPrize_Black = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";
        }
    }
    public async Task UpdateFixed_Red_BetAmount(int GameType, decimal Amount)
    {
        if (GameType == GametypeId)
        {
            decimal FixedPriceMultiplier = Convert.ToDecimal(G12RegSettings.FixedPriceMultiplier);
            decimal totalBets = !string.IsNullOrEmpty(UserTotalBet_FixedPrize_Red) ? decimal.Parse(UserTotalBet_FixedPrize_Red) + Amount : Amount;
            UserTotalBet_FixedPrize_Red = totalBets.ToString("#,##0");
            SampleWinFixedPrize_Red = totalBets != 0 ? (totalBets * FixedPriceMultiplier).ToString("#,###.#0") : "0";
        }
    }
    #endregion

    #endregion

}