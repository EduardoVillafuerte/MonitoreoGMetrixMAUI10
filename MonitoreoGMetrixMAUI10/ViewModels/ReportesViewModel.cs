using System.Collections.ObjectModel;
using System.Windows.Input;
using MonitoreoGMetrixMAUI10.Controllers;
using MonitoreoGMetrixMAUI10.Models;
using MonitoreoGMetrixMAUI10.Views;
using MvvmHelpers;


namespace MonitoreoGMetrixMAUI10.ViewModels
{
    public class ReportesViewModel : BaseViewModel
    {
        private readonly ReportesService _service;

        public ObservableCollection<Facultad> Facultades { get; set; } = new();
        public ObservableCollection<Carrera> Carreras { get; set; } = new();
        public ObservableCollection<Periodo> Periodos { get; set; } = new();
        public ObservableCollection<Certificacion> Certificaciones { get; set; } = new();

        public ObservableCollection<CertificacionViewModel> ReportesAgrupados { get; set; } = new();

        public Facultad FacultadSeleccionada
        {
            get => _facultadSeleccionada;
            set
            {
                _facultadSeleccionada = value;
                OnPropertyChanged();
                CargarCarreras();
            }
        }
        private Facultad _facultadSeleccionada;

        public Carrera CarreraSeleccionada
        {
            get => _carreraSeleccionada;
            set
            {
                _carreraSeleccionada = value;
                OnPropertyChanged();
                CargarCertificaciones();
            }
        }
        private Carrera _carreraSeleccionada;

        public Periodo PeriodoSeleccionado { get; set; }
        public Certificacion CertificacionSeleccionada { get; set; }

        public ICommand FiltrarCommand { get; }
        public ICommand VerDetallesCommand { get; }

        public ReportesViewModel()
        {
            _service = new ReportesService();

            FiltrarCommand = new Command(async () => await Filtrar());
            VerDetallesCommand = new Command<GMetrixReportFiltrado>(async (m) => await AbrirDetalles(m));

            CargarInicial();
        }

        // ==============================
        // CARGA INICIAL
        // ==============================
        private async void CargarInicial()
        {
            IsBusy = true;

            Facultades.Clear();
            foreach (var f in await _service.ObtenerFacultadesAsync())
                Facultades.Add(f);  

            Periodos.Clear();
            foreach (var p in await _service.ObtenerPeriodosAsync())
                Periodos.Add(p);

            IsBusy = false;
        }

        // ==============================
        // CARRERAS SEGÚN FACULTAD
        // ==============================
        private async void CargarCarreras()
        {
            Carreras.Clear();
            Certificaciones.Clear();

            if (FacultadSeleccionada is null) return;

            var lista = await _service.ObtenerCarrerasAsync(FacultadSeleccionada.FacultadID.Value);

            foreach (var c in lista)
                Carreras.Add(c);
        }

        // ==============================
        // CERTIFICACIONES SEGÚN CARRERA
        // ==============================
        private async void CargarCertificaciones()
        {
            Certificaciones.Clear();

            if (CarreraSeleccionada is null) return;

            var lista = await _service.ObtenerCertificacionesAsync(CarreraSeleccionada.CarreraID);

            foreach (var c in lista)
                Certificaciones.Add(c);
        }

        // ==============================
        // FILTRADO PRINCIPAL
        // ==============================
        private async Task Filtrar()
        {
            if (FacultadSeleccionada == null || PeriodoSeleccionado == null)
            {
                await Application.Current.MainPage.DisplayAlert("Faltan datos", "Seleccione al menos Facultad y Periodo", "Ok");
                return;
            }

            IsBusy = true;

            var datos = await _service.FiltrarAsync(
                    facultad: FacultadSeleccionada.FacultadID.Value,
                    carrera: CarreraSeleccionada?.CarreraID ?? 0,
                    periodo: PeriodoSeleccionado.PeriodoID,
                    certificacion: CertificacionSeleccionada?.CertificacionId ?? 0
                );

            ReportesAgrupados.Clear();

            var agrupado = datos
                .GroupBy(x => x.certificadoNombre)
                .Select(g => new CertificacionViewModel
                {
                    NombreCertificacion = g.Key,
                    Materias = g.ToList()
                });

            foreach (var item in agrupado)
                ReportesAgrupados.Add(item);

            IsBusy = false;
        }

        // ==============================
        // NAVEGAR A DETALLES
        private async Task AbrirDetalles(GMetrixReportFiltrado materia)
        {
            var navParams = new Dictionary<string, object>
            {
                // ESTAS CLAVES DEBEN COINCIDIR CON LAS DEL ApplyQueryAttributes DE ARRIBA
                {"NRC", materia.NRC },
                {"PeriodoID", materia.periodo },
                {"Materia", materia.nombreMateria},
                {"Certificacion", materia.certificadoNombre},
                {"PeriodoCodigo", materia.periodoCodigo},
                {"Carrera", materia.nombreCarrera}
            };

            await Shell.Current.GoToAsync(nameof(DetallesPage), navParams);
        }
    }
}
