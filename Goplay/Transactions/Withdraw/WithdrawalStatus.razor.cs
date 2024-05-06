using Blazored.Modal.Services;
using GoplayasiaBlazor.Models;
using GoPlayAsiaWebApp.Goplay.Shared.Popup;
using GoPlayAsiaWebApp.Goplay.ViewModels;
using Microsoft.AspNetCore.Components;

namespace GoPlayAsiaWebApp.Goplay.Transactions.Withdraw;

public partial class WithdrawalStatus
{
    [Parameter]
    public long tranid { get; set; }
    [Parameter]
    public string amount { get; set; }
    public TransactionModel Transaction { get; set; }
    [CascadingParameter] public IModalService popupModal { get; set; }
    protected override async Task OnParametersSetAsync()
    {
        var popupRes = popupModal.Show<PopupLoading>("");
        Transaction = await _transactionService.Transaction(tranid);
        popupRes.Close();
    }
}
