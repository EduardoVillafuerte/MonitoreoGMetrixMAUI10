using MonitoreoGMetrixMAUI10.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MonitoreoGMetrixMAUI10.Controllers
{
    public class ReportesService
    {
        private readonly HttpClient _http;

        public ReportesService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:5001/GMetrix/")
            };
        }

        // FACULTADES
        public async Task<List<Facultad>> ObtenerFacultadesAsync()
        {
            var resp = await _http.GetAsync("Facultades");

            if (!resp.IsSuccessStatusCode)
                return new List<Facultad>();

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Facultad>>(json) ?? new();
        }

        // CARRERAS SEGÚN FACULTAD
        public async Task<List<Carrera>> ObtenerCarrerasAsync(int facultadId)
        {
            var resp = await _http.GetAsync($"Carreras?FacultadID={facultadId}");

            if (!resp.IsSuccessStatusCode)
                return new List<Carrera>();

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Carrera>>(json) ?? new();
        }

        // PERIODOS
        public async Task<List<Periodo>> ObtenerPeriodosAsync()
        {
            var resp = await _http.GetAsync("Periodos");

            if (!resp.IsSuccessStatusCode)
                return new List<Periodo>();

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Periodo>>(json) ?? new();
        }

        // CERTIFICACIONES POR CARRERA
        public async Task<List<Certificacion>> ObtenerCertificacionesAsync(int carreraId)
        {
            var resp = await _http.GetAsync($"Certificaciones?CarreraID={carreraId}");

            if (!resp.IsSuccessStatusCode)
                return new List<Certificacion>();

            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Certificacion>>(json) ?? new();
        }

        // FILTRAR CERTIFICACIONES (Listado principal)
        public async Task<List<GMetrixReportFiltrado>> FiltrarAsync(int facultad, int carrera, int periodo, int certificacion)
        {
            var url = $"Filtrados?Carrera={carrera}&Certificacion={certificacion}&Facultad={facultad}&Periodo={periodo}";
            var datos = await _http.GetFromJsonAsync<List<GMetrixReportFiltrado>>(url);

            return datos ?? new List<GMetrixReportFiltrado>();
        }

        // DETALLES DE CERTIFICACIÓN
        public async Task<List<DetallesModel>> ObtenerDetallesAsync(string NRC, int periodo)
        {
            var url = $"Detalles?NRC={NRC}&Periodo={periodo}";
            var datos = await _http.GetFromJsonAsync<List<DetallesModel>>(url);

            return datos ?? new List<DetallesModel>();
        }

        // DETALLE DE INTENTOS POR USUARIO
        public async Task<List<DetallesFiltrados>> ObtenerIntentosUsuarioAsync(string NRC, int periodoID, string username)
        {
            var url = $"DetalleCertificacion?NRC={NRC}&PeriodoID={periodoID}&UserName={username}";
            var datos = await _http.GetFromJsonAsync<List<DetallesFiltrados>>(url);

            return datos ?? new List<DetallesFiltrados>();
        }

        // ESTUDIANTES APTOS PARA EXAMEN
        public async Task<List<Aptos>> ObtenerAptosAsync(string certificacion, int periodoID)
        {
            var url = $"AptosExamen?Certificacion={certificacion}&Periodo={periodoID}";
            var datos = await _http.GetFromJsonAsync<List<Aptos>>(url);

            return datos ?? new List<Aptos>();
        }

        // RETORNO DE LISTA PARA NOTIFICACIONES (sin ViewBag)
        public async Task<List<string>> AbrirNotificacionAsync(List<string> seleccionados)
        {
            return seleccionados; // En MAUI no usamos PartialViews
        }

        //LISTA PREDICCIONES IA
        public async Task<List<NotificacionRiesgo>> ObtenerPrediccionesAsync(string NRC, int periodo)
        {
            try
            {
                var url = $"PrediccionesIA?Periodo={periodo}&NRC={NRC}";
                var datos = await _http.GetFromJsonAsync<List<NotificacionRiesgo>>(url);
                return datos ?? new List<NotificacionRiesgo>();
            }
            catch
            {
                return new List<NotificacionRiesgo>();
            }
        }
    }
}
