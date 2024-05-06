using Blazored.Modal;
using Blazored.Modal.Services;
using GoplayasiaBlazor.Core.Global.Interface;
using GoplayasiaBlazor.Models.Constants;
using GoPlayAsiaWebApp.Shared.Popup;
using GoPlayAsiaWebApp.ViewModels;
using Microsoft.AspNetCore.Components;
using static GoplayasiaBlazor.Models.Constants.Settings;

namespace GoPlayAsiaWebApp.Games.Go12
{
    public partial class Go12
    {

        [Inject]
        Go12ViewModel go12Model { get; set; }
        [CascadingParameter] public IModalService popupModal { get; set; }
        public IModalReference popupRef { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                go12Model.Notify += OnNotify;
                go12Model.popupModal = popupModal;
                go12Model.TokenDiv = "tokenhide";

                await go12Model.ConnectSignalR();
                await go12Model.AssignSignalRMethods();
                popupRef = popupModal.Show<PopupLoading>("");



                await go12Model.GetGameSetting();
                await go12Model.GetGameRound();

                List<Task> TaskList = new List<Task>();
                TaskList.Add(go12Model.GetPlayerCategory()); //new
                TaskList.Add(go12Model.GetGameChipsByCategory()); //new
                TaskList.Add(go12Model.GetGameVariantChipsByCategory()); //new

                TaskList.Add(go12Model.GetCurrentRoundBets());
                if (go12Model.streamKey == string.Empty)
                    TaskList.Add(go12Model.GenerateStreamKey());
                await Task.WhenAll(TaskList.ToArray());

                popupRef.Close();
                await go12Model.GetTrends();

            }
            catch (Exception)
            {
                //throw;
            }
        }

        private async Task setGameChips()
        {
            await go12Model.GetGameChipsByCategory();
            await go12Model.GetGameVariantChipsByCategory();

            if (go12Model.BetCombinationValue == Constants.FixedRed || go12Model.BetCombinationValue == Constants.FixedBlack)
            {
                await go12Model.SetGameChips((int)GameVariant.G12Reg, true);
            }
            else
            {
                await go12Model.SetGameChips(go12Model.GametypeId, false);
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



        }
        public async ValueTask DisposeAsync()
        {
            go12Model.Notify -= OnNotify;
            //await go12Model.DisconnectSignalR();
        }
    }
}
