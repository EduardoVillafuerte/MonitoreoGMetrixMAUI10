using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _usuario = string.Empty;
        private string _contrasena = string.Empty;

        public string Usuario
        {
            get => _usuario;
            set { _usuario = value; OnPropertyChanged(); }
        }

        public string Contrasena
        {
            get => _contrasena;
            set { _contrasena = value; OnPropertyChanged(); }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await EjecutarLogin());
        }

        private async Task EjecutarLogin()
        {
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                if (string.IsNullOrWhiteSpace(Usuario) || string.IsNullOrWhiteSpace(Contrasena))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese usuario y contraseña.", "OK");
                    return;
                }

                // -------------------------
                // 🔥 LOGIN CON DATOS QUEMADOS
                // -------------------------
                if (Usuario == "edison.villafuerte@udla.edu.ec" && Contrasena == "1234")
                {
                    // Cambiar a Flyout + Shell Navigation
                    Application.Current.MainPage = new AppShell();
                    return;
                }

                await Application.Current.MainPage.DisplayAlert("Error",
                    "Usuario o contraseña incorrectos.",
                    "OK");
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error",
                    $"Ocurrió un error: {ex.Message}",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
