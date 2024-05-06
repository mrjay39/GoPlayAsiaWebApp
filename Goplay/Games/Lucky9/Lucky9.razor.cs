using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Goplay.Games.Lucky9
{
    public partial class Lucky9
    {
        [Inject]
        Lucky9ViewModel luckyViewModel { get; set; }
        [CascadingParameter] public IModalService popupModal { get; set; }

        public IModalReference popupRef { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadPageData();
        }
        private async Task LoadPageData()
        {
            try
            {
                luckyViewModel.Notify += OnNotify;
                luckyViewModel.popupModal = popupModal;
                luckyViewModel.Token_Animation = "";
                luckyViewModel.TokenDiv = "tokenhide";
                luckyViewModel.IsbuttonDisabled = false;

                await luckyViewModel.ConnectSignalR();
                await luckyViewModel.AssignSignalRMethods();
                popupRef = popupModal.Show<PopupLoading>("");


                await luckyViewModel.GetGameSetting();
                await luckyViewModel.GetGameSettingVariant();
                await luckyViewModel.GetGameRound();

                List<Task> TaskList = new List<Task>();
                TaskList.Add(luckyViewModel.GetPlayerCategory()); //new
                TaskList.Add(luckyViewModel.GetGameChipsByCategory()); //new
                TaskList.Add(luckyViewModel.GetGameVariantChipsByCategory()); //new
                TaskList.Add(luckyViewModel.GetCurrentRoundBets()); //modified
                if (luckyViewModel.streamKey == string.Empty)
                    TaskList.Add(luckyViewModel.GenerateStreamKey()); //modified                
                await Task.WhenAll(TaskList.ToArray());

                popupRef.Close();
                await luckyViewModel.GetTrends();
            }

            catch (Exception)
            {

                throw;
            }
        }
        private async Task setGameChips()
        {
            await luckyViewModel.GetGameChipsByCategory();
            await luckyViewModel.GetGameVariantChipsByCategory();

            if (luckyViewModel.CurrentGameType == (int)L9MainBetTypes.Player || luckyViewModel.CurrentGameType == (int)L9MainBetTypes.Banker)
            {
                await luckyViewModel.SetGameChips(luckyViewModel.GametypeId, false);
            }
            else
            {
                switch (luckyViewModel.CurrentGameType)
                {
                    case (int)L9SubBetTypes.Target9Player:
                    case (int)L9SubBetTypes.Target9Banker:
                        await luckyViewModel.SetGameChips((int)GameVariant.L9Target, true);
                        break;
                    case (int)L9MainBetTypes.Draw:
                        await luckyViewModel.SetGameChips((int)GameVariant.L9Draw, true);
                        break;
                    case (int)L9SubBetTypes.AnypairPlayer:
                    case (int)L9SubBetTypes.AnyPairBanker:
                        await luckyViewModel.SetGameChips((int)GameVariant.L9Pair, true);
                        break;
                    case (int)L9SubBetTypes.SameSuitePlayer:
                    case (int)L9SubBetTypes.SameSuiteBanker:
                        await luckyViewModel.SetGameChips((int)GameVariant.L9Suits, true);
                        break;
                    case (int)L9SubBetTypes.TwoRedPlayer:
                    case (int)L9SubBetTypes.TwoRedBanker:
                    case (int)L9SubBetTypes.TwoBlackPlayer:
                    case (int)L9SubBetTypes.TwoBlackBanker:
                        await luckyViewModel.SetGameChips((int)GameVariant.L9Color, true);
                        break;
                }
            }
        }
        protected override void OnAfterRender(bool firstRender)
        {

        }

        public async void OnNotify()
        {
            await InvokeAsync(() => StateHasChanged());
        }

        public void Dispose()
        {
            //luckyViewModel.DisconnectSignalR(); ;
        }
        public async ValueTask DisposeAsync()
        {
            luckyViewModel.Notify -= OnNotify;
            // await luckyViewModel.DisconnectSignalR();
        }
    }
}

