namespace CoreApiPOC.Services
{
    using Acr.UserDialogs;
    using System.Threading.Tasks;

    public class DialogService : IDialogService
    {
        public Task ShowAlertAsync(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}
