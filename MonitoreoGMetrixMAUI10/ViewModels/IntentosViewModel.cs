using MonitoreoGMetrixMAUI10.Controllers;
using MonitoreoGMetrixMAUI10.Models;
using System.Collections.ObjectModel;

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    [QueryProperty(nameof(NRC), "NRC")]
    [QueryProperty(nameof(PeriodoID), "PeriodoID")]
    [QueryProperty(nameof(UserName), "UserName")]
    public class IntentosViewModel : BaseViewModel
    {
        private readonly ReportesService _service;

        public string NRC { get; set; }
        public int PeriodoID { get; set; }
        public string UserName { get; set; }

        public ObservableCollection<DetallesFiltrados> Intentos { get; set; } = new();

        public IntentosViewModel()
        {
            _service = new ReportesService();
        }

        public async void OnAppearing()
        {
            // 1. Activar indicador de carga
            IsBusy = true;

            try
            {
                var datos = await _service.ObtenerIntentosUsuarioAsync(NRC, PeriodoID, UserName);

                Intentos.Clear();
                foreach (var i in datos)
                    Intentos.Add(i);
            }
            finally
            {
                // 2. Desactivar indicador al terminar
                IsBusy = false;
            }
        }
    }
}