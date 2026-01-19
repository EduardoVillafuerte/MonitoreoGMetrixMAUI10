using Microcharts;
using MonitoreoGMetrixMAUI10.Controllers;
using MonitoreoGMetrixMAUI10.Models;
using MonitoreoGMetrixMAUI10.Views;
using SkiaSharp;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MonitoreoGMetrixMAUI10.ViewModels
{
    [QueryProperty(nameof(NRC), "NRC")]
    [QueryProperty(nameof(PeriodoID), "PeriodoID")]
    [QueryProperty(nameof(materia), "Materia")]
    [QueryProperty(nameof(Carrera), "Carrera")]
    [QueryProperty(nameof(Certificacion), "Certificacion")]
    [QueryProperty(nameof(periodoCodigo), "PeriodoCodigo")]
    public class DetallesViewModel : BaseViewModel
    {
        private readonly ReportesService _service;
        private int periodo;
        private string certificacion;
        private string materia;

        public string NRC { get; set; }
        public int PeriodoID { get; set; }

        // Add the missing property for periodoCodigo
        public string periodoCodigo { get; set; }

        public ObservableCollection<DetallesModel> Estudiantes { get; set; } = new();

        public Chart GraficoBarras { get; set; }
        public Chart GraficoPie { get; set; }

        public ICommand VerIntentosCommand { get; }

        public DetallesViewModel()
        {
            _service = new ReportesService();
            VerIntentosCommand = new Command<DetallesModel>(async (m) => await AbrirIntentos(m));
        }

        public DetallesViewModel(string nrc, int periodo, string certificacion, string materia)
        {
            NRC = nrc;
            this.periodo = periodo;
            this.certificacion = certificacion;
            this.materia = materia;
        }

        public async void OnAppearing()
        {
            await CargarDatos();
        }

        // =====================================
        // CARGAR DATOS
        // =====================================
        private async Task CargarDatos()
        {
            IsBusy = true;

            var datos = await _service.ObtenerDetallesAsync(NRC, PeriodoID);

            Estudiantes.Clear();

            foreach (var item in datos)
            {
                if (item.promedio != null)
                {
                    var texto = item.promedio.ToString();

                    // Solo toma hasta 5 caracteres máximo
                    if (texto.Length > 5)
                        texto = texto.Substring(0, 6);

                    // Reconvertir a decimal
                    if (decimal.TryParse(texto, out var nuevoPromedio))
                        item.promedio = nuevoPromedio;
                }

                Estudiantes.Add(item);
            }

            GenerarGraficos();

            IsBusy = false;
        }


        // =====================================
        // CREAR GRÁFICOS MICROCHARTS
        // =====================================
        private void GenerarGraficos()
        {
            int buenos = Estudiantes.Count(c => c.promedio > 799);
            int regulares = Estudiantes.Count(c => c.promedio > 599 && c.promedio <= 799);
            int malos = Estudiantes.Count(c => c.promedio <= 599);

            // Barra
            GraficoBarras = new BarChart
            {
                Entries = new[]
                {
                    new ChartEntry(buenos){Color = SKColor.Parse("#CDEFDA"), Label = "Buenos", ValueLabel = buenos.ToString()},
                    new ChartEntry(regulares){Color = SKColor.Parse("#FFD7B1"), Label = "Regulares", ValueLabel = regulares.ToString()},
                    new ChartEntry(malos){Color = SKColor.Parse("#FFCCCC"), Label = "Malos", ValueLabel = malos.ToString()}
                }
            };

            // Pie
            GraficoPie = new PieChart
            {
                Entries = new[]
                {
                    new ChartEntry(buenos){Color = SKColor.Parse("#CDEFDA")},
                    new ChartEntry(regulares){Color = SKColor.Parse("#FFD7B1")},
                    new ChartEntry(malos){Color = SKColor.Parse("#FFCCCC")}
                }
            };
        }

        // =====================================
        // NAVEGAR A INTENTOS
        // =====================================
        private async Task AbrirIntentos(DetallesModel d)
        {
            var navParams = new Dictionary<string, object>
            {
                { "NRC", NRC },
                { "PeriodoID", PeriodoID },
                { "UserName", d.UserName }
            };

            await Shell.Current.GoToAsync(nameof(IntentosPage), navParams);
        }
    }
}
