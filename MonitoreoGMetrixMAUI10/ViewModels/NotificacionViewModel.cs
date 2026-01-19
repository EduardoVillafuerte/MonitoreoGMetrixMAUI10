using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmHelpers; //

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    [QueryProperty(nameof(Correos), "Correos")]
    public class NotificacionViewModel : BaseViewModel
    {
        // 1. Campo privado de respaldo
        private ObservableCollection<string> _correos = new();

        // 2. Propiedad pública con notificación de cambios
        public ObservableCollection<string> Correos
        {
            get => _correos;
            set => SetProperty(ref _correos, value); // IMPORTANTE: Esto avisa al XAML que llegaron los datos
        }

        public ICommand EnviarCommand { get; private set; }

        public NotificacionViewModel()
        {
            EnviarCommand = new Command(async () => await SimularEnvio());
        }

        private async Task SimularEnvio()
        {
            await Application.Current.MainPage.DisplayAlert("Éxito", "La notificación ha sido enviada.", "OK");
            await Shell.Current.GoToAsync("..");
        }
    }
}