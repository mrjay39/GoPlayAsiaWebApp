using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoplayasiaBlazor.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Bigwin;

public partial class BigwinBettingButton
{

    #region Variables
    public IModalReference popupConfirm { get; set; }
    public bool IsBetPopupShown = false;
    public ElementReference txtbetnumber;
    public ElementReference btnbet;
    private string cssborder = "border: 3px solid #47e60f;";
    public string cssInputBorder = "border: 3px solid #47e60f;";
    public string cssBtnBetBorder = "";
    #endregion

    #region Parameters
    [Parameter] public int TotalBets { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter] public ObservableCollection<BetModel> UserBets { get; set; }
    [Parameter] public ObservableCollection<BetModel> UserPrevBets { get; set; }
    [Parameter] public GameRoundModel PrevGameRound { get; set; }
    [Parameter] public string JackpotPrize { get; set; }
    [Parameter] public string MiniJackpotPrize { get; set; }
    [Parameter] public string ConsolationPrize { get; set; }
    [Parameter] public bool IsLuckyPickEnabled { get; set; }
    [Parameter] public bool IsGameOpen { get; set; }
    [Parameter] public string BaseBetValue { get; set; }
    [Parameter] public bool IsBetEnabled { get; set; }
    [Parameter] public string GameResultString { get; set; }
    [Parameter] public string RoundStatusString { get; set; }
    #endregion

    #region Lifecycle Events
    protected override async Task OnInitializedAsync()
    {
        try
        {

            iBigWinViewModel.txtbetnumber = txtbetnumber;
            endisLuckyPicky(false);
        }
        catch (Exception)
        {

            //throw;
        }

    }
    #endregion

    #region Local Functions
    private async Task endisLuckyPicky(bool en)
    {
        if (iBigWinViewModel.IsLuckyPick1 || iBigWinViewModel.IsLuckyPick5 || iBigWinViewModel.IsLuckyPick10)
        {
            iBigWinViewModel.BetAmount = int.Parse(iBigWinViewModel.BaseBetValue);
        }
        iBigWinViewModel.IsLuckyPick1 = en;
        iBigWinViewModel.IsLuckyPick5 = en;
        iBigWinViewModel.IsLuckyPick10 = en;
    }

    #endregion



}
