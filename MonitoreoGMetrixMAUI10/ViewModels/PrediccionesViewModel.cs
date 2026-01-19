using MvvmHelpers;
using MonitoreoGMetrixMAUI10.Models;
using MonitoreoGMetrixMAUI10.Controllers;
using System.Collections.ObjectModel;

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    [QueryProperty(nameof(NRC), "NRC")]
    [QueryProperty(nameof(PeriodoID), "PeriodoID")]
    public class PrediccionesViewModel : BaseViewModel
    {
        private readonly ReportesService _service;

        public string NRC { get; set; }
        public int PeriodoID { get; set; }

        public ObservableCollection<NotificacionRiesgo> Predicciones { get; set; } = new();

        public PrediccionesViewModel()
        {
            _service = new ReportesService();
        }

        public async Task OnAppearing()
        {
            IsBusy = true;

            // Llamamos al nuevo método del servicio
            var datos = await _service.ObtenerPrediccionesAsync(NRC, PeriodoID);

            Predicciones.Clear();
            foreach (var item in datos)
            {
                Predicciones.Add(item);
            }

            IsBusy = false;
        }
    }
}