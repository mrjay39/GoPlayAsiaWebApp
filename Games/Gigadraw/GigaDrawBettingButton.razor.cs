using Blazored.Modal;
using Blazored.Modal.Services;
using Blazored.Toast.Services;
using GoPlayAsiaWebApp.Games.Lucky9;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Gigadraw;

public partial class GigaDrawBettingButton
{
    public int keymode { get; set; } = 0;

    public IModalReference popupConfirm { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    [Parameter]
    public string JackpotPrize { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserBets { get; set; }
    [Parameter]
    public ObservableCollection<BetModel> UserPrevBets { get; set; }
    [Parameter]
    public GameRoundModel PrevGameRound { get; set; }
    [Parameter]
    public string ConsolationPrize { get; set; }
    [Parameter]
    public bool IsLuckyPickEnabled { get; set; }
    [Parameter]
    public bool IsGameOpen { get; set; }
    [Parameter]
    public string BaseBetValue { get; set; }
    public bool IsBetPopupShown = false;
    public ElementReference txtbetnumber;
    public ElementReference btnbet;
    [Parameter] public string GameResultString { get; set; }

    private string cssborder = "border: 3px solid #47e60f;";
    public string cssInputBorder = "border: 3px solid #47e60f;";
    public string cssInputNumBetBorder = "";
    public string cssBtnBetBorder = "";
    private async Task setBet(char e)
    {
        endisLuckyPicky(false);
        if (iGigadrawModel.LetterValue.Length == 3)
        {
            return;
        }

        iGigadrawModel.LetterValue += e;
        if (iGigadrawModel.LetterValue.Length >= 3)
        {
            if (iGigadrawModel.NumberValueInput.Length == 1)
            {
                keymode = 2;
                setCssMode();
                btnbet.FocusAsync();
            }
            else
            {
                endisLuckyPicky(false);
                cssInputBorder = "";
                cssBtnBetBorder = "";
                cssInputNumBetBorder = cssborder;
                keymode = 1;
                return;
            }


        }
        else
        {
            cssInputBorder = cssborder;
            cssBtnBetBorder = "";
            cssInputNumBetBorder = "";
        }

    }
    private async Task setKeyMode(int mode)
    {
        endisLuckyPicky(false);
        keymode = mode;
        setCssMode();
        await CheckifBetisEnabled();
    }
    private async Task CheckifBetisEnabled()
    {
        if (iGigadrawModel.LetterValue.Length >= 3 && iGigadrawModel.NumberValueInput.Length >= 1)
        {
            iGigadrawModel.IsBetEnabled = true;
        }
        else
        {
            iGigadrawModel.IsBetEnabled = false; ;
        }
    }
    private async Task endisLuckyPicky(bool en)
    {
        if (iGigadrawModel.IsLuckyPick1 || iGigadrawModel.IsLuckyPick5 || iGigadrawModel.IsLuckyPick10)
        {
            iGigadrawModel.BetAmount = int.Parse(iGigadrawModel.BaseBetValue);
        }
        iGigadrawModel.IsLuckyPick1 = en;
        iGigadrawModel.IsLuckyPick5 = en;
        iGigadrawModel.IsLuckyPick10 = en;
    }
    private async Task setBetnum(char e)
    {
        endisLuckyPicky(false);
        iGigadrawModel.NumberValueInput += e;
        if (iGigadrawModel.NumberValueInput.Length >= 1)
        {
            if (iGigadrawModel.LetterValue.Length < 3)
            {
                keymode = 0;
                setCssMode();
            }
            else
            {
                iGigadrawModel.NumberValueInput = iGigadrawModel.NumberValueInput.Substring(0, 1);
                endisLuckyPicky(false);
                keymode = 2;
                setCssMode();
                iGigadrawModel.IsBetEnabled = true;
                btnbet.FocusAsync();
                return;
            }

        }
        else
        {
            cssBtnBetBorder = "";
            cssInputBorder = "";
            cssInputNumBetBorder = cssborder;
            iGigadrawModel.IsBetEnabled = false;
        }

    }

    protected override async Task OnInitializedAsync()
    {
        try
        {

            iGigadrawModel.txtbetnumber = txtbetnumber;

            endisLuckyPicky(false);
        }
        catch (Exception)
        {

            //throw;
        }

    }
    private async Task onchange_txtbetletter(ChangeEventArgs e)
    {

        var value = (string)e.Value;


        if (value.Length > 3)
        {
            iGigadrawModel.LetterValue = value.Substring(0, 3);
        }
        else if (value.Length == 3)
        {
            endisLuckyPicky(false);
            iGigadrawModel.BetAmount = int.Parse(iGigadrawModel.BaseBetValue);
            iGigadrawModel.NumberValueInput = "";
            await txtbetnumber.FocusAsync();
        }

        if (iGigadrawModel.NumberValueInput.Length == 1 && value.Length == 3)
        {
            iGigadrawModel.IsBetEnabled = true;
        }
        else
        {
            iGigadrawModel.IsBetEnabled = false;
        }
        //iGigadrawModel.LetterValue = iGigadrawModel.LetterValue.ToUpper();
        StateHasChanged();
    }
    private async Task onchange_txtbetnumber(ChangeEventArgs e)
    {

        var value = (string)e.Value;
        if (value.Length == 1)
        {
            endisLuckyPicky(false);
            iGigadrawModel.BetAmount = int.Parse(iGigadrawModel.BaseBetValue);
            iGigadrawModel.IsBetEnabled = true;
            btnbet.FocusAsync();
        }
        else if (value.Length > 1)
        {
            @iGigadrawModel.NumberValueInput = value.Substring(0, 1);
            //txtbetnumber.Context.
            btnbet.FocusAsync();
        }
        else
        {
            iGigadrawModel.IsBetEnabled = false;
        }
        if (iGigadrawModel.LetterValue.Length == 3 && value.Length == 1)
        {
            iGigadrawModel.IsBetEnabled = true;
        }
        else
        {
            iGigadrawModel.IsBetEnabled = false;
        }
        StateHasChanged();

    }
    private async Task Clear()
    {
        iGigadrawModel.LetterValue = string.Empty;
        iGigadrawModel.NumberValue = 0;
        iGigadrawModel.NumberValueInput = string.Empty;
        iGigadrawModel.BetAmount = int.Parse(iGigadrawModel.BaseBetValue);
        iGigadrawModel.TotalBetAmount = iGigadrawModel.BetAmount;
        keymode = 0;
    }
    private async Task DelKey()
    {
        if (iGigadrawModel.LetterValue.Length > 0)
        {
            //cssInputBorder = cssborder;
            //cssBtnBetBorder = "";
            iGigadrawModel.LetterValue = iGigadrawModel.LetterValue.Remove(iGigadrawModel.LetterValue.Length - 1, 1);
            cssInputBorder = cssborder;
            cssBtnBetBorder = "";
            cssInputNumBetBorder = "";
            keymode = 0;
            endisLuckyPicky(false);

            setCssMode();
        }
    }
    private async Task DelKeyNum()
    {
        if (iGigadrawModel.NumberValueInput.Length > 0)
        {
            //cssInputBorder = cssborder;
            //cssBtnBetBorder = "";
            iGigadrawModel.NumberValueInput = iGigadrawModel.NumberValueInput.Remove(iGigadrawModel.NumberValueInput.Length - 1, 1);
            keymode = 1;
            endisLuckyPicky(false);

            setCssMode();
        }
    }
    private async Task BetOptionSelected(object type)
    {
        if (IsBetPopupShown) return;
        if (!iGigadrawModel.IsBetEnabled && type == Constants.NormalPick) return;

        if (iGigadrawModel.IsBetEnabled && (iGigadrawModel.IsLuckyPick1 || iGigadrawModel.IsLuckyPick10 || iGigadrawModel.IsLuckyPick5))
        {
            if (iGigadrawModel.IsLuckyPick1)
                type = Constants.LuckyPick;
            else if (iGigadrawModel.IsLuckyPick5)
                type = Constants.LuckyPickx5;
            else if (iGigadrawModel.IsLuckyPick10)
                type = Constants.LuckyPickx10;
        }

        bool valid = await iGigadrawModel.BetOptionSelected(type);
        if (valid)
        {
            var parameters = new ModalParameters();
            string msg = "";
            switch (type)
            {
                case Constants.NormalPick:

                    msg = "Confirm bet " + iGigadrawModel.TotalBetAmount + " for " + iGigadrawModel.BetOption;
                    break;
                case Constants.LuckyPick:
                    msg = "Confirm bet " + iGigadrawModel.TotalBetAmount + " for " + iGigadrawModel.BetOption;
                    break;
                case Constants.LuckyPickx5:
                    ;
                    msg = "Confirm Lucky Pick x5 Total " + iGigadrawModel.TotalBetAmount;
                    break;
                case Constants.LuckyPickx10:
                    msg = "Confirm Lucky Pick x10 Total " + iGigadrawModel.TotalBetAmount;
                    break;
            }
            parameters.Add("Message", msg);

            popupConfirm = popupModal.Show<PopupConfirm>("", parameters, new ModalOptions() { Class = "op-modal" });
            IsBetPopupShown = true;
            ModalResult result = await popupConfirm.Result;
            IsBetPopupShown = false;
            if (result.Data != null)
            {
                if ((bool)result.Data)
                {
                    popupConfirm.Close();
                    //for (int i = 0; i < 100000; i++)
                    //{

                    await iGigadrawModel.SubmitBet();
                    keymode = 0;
                    setCssMode();
                    //}
                }
            }
        }
    }
    private async Task setCssMode()
    {
        switch (keymode)
        {
            case 0:
                cssInputBorder = cssborder;
                cssBtnBetBorder = "";
                cssInputNumBetBorder = "";
                break;
            case 1:
                cssInputBorder = "";
                cssBtnBetBorder = "";
                cssInputNumBetBorder = cssborder;
                break;
            case 2:
                cssInputBorder = "";
                cssBtnBetBorder = cssborder;
                cssInputNumBetBorder = "";
                break;
        }
    }
    private async Task SetLuckyPickBet(string param)
    {
        cssInputBorder = "";
        cssBtnBetBorder = "";
        await iGigadrawModel.SetLuckyPickBet(param);

    }

}
