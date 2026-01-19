using MonitoreoGMetrixMAUI10.Controllers;
using MonitoreoGMetrixMAUI10.Models;
using System.Collections.ObjectModel;
using MvvmHelpers;

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    [QueryProperty(nameof(Certificacion), "Certificacion")]
    [QueryProperty(nameof(PeriodoID), "PeriodoID")]
    public class AptosViewModel : BaseViewModel
    {
        private readonly ReportesService _service;

        public string Certificacion { get; set; }
        public int PeriodoID { get; set; }

        public ObservableCollection<Aptos> Aptos { get; set; } = new();

        public AptosViewModel()
        {
            _service = new ReportesService();
        }

        public async void OnAppearing()
        {
            var datos = await _service.ObtenerAptosAsync(Certificacion, PeriodoID);

            Aptos.Clear();
            foreach (var a in datos)
                Aptos.Add(a);
        }
    }
}
