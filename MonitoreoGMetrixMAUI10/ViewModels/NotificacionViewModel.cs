using System.Collections.ObjectModel;
using MvvmHelpers;

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    [QueryProperty(nameof(Correos), "Correos")]
    public class NotificacionViewModel : BaseViewModel
    {
        public ObservableCollection<string> Correos { get; set; } = new();

        public async Task Enviar()
        {
            await Application.Current.MainPage.DisplayAlert("Enviado", "Notificación enviada con éxito", "OK");
        }
    }
}
