using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using MonitoreoGMetrixMAUI10.Models;

namespace MonitoreoGMetrixMAUI10.Controllers
{
    public class ActualizarDatosService
    {
        private readonly HttpClient _client;

        public ActualizarDatosService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://tfswcl69-5001.use2.devtunnels.ms/GMetrix/") // <-- Reemplaza con tu URL
            };
        }

        // SUBIR CSV A LA API
        public async Task<(bool Exito, string Mensaje)> SubirArchivoCSVAsync(Stream archivoStream)
        {
            if (archivoStream == null || archivoStream.Length == 0)
                return (false, "Archivo vacío o no válido.");

            var listaDatos = new List<GMetrixReport>();

            using (var reader = new StreamReader(archivoStream))
            {
                string? linea;
                bool primeraLinea = true;

                while ((linea = await reader.ReadLineAsync()) != null)
                {
                    if (primeraLinea)
                    {
                        primeraLinea = false; // Saltar cabecera
                        continue;
                    }

                    var columnas = linea.Split(',');

                    var reporte = new GMetrixReport
                    {
                        Test = SafeCol(columnas, 1),
                        FirstName = SafeCol(columnas, 2),
                        LastName = SafeCol(columnas, 3),
                        StudentNumber = SafeCol(columnas, 4),
                        UserName = SafeCol(columnas, 5),
                        CompleteDate = ParseFecha(SafeCol(columnas, 6)),
                        Mode = SafeCol(columnas, 7),
                        ScorePercent = SafeCol(columnas, 8),
                        Score = SafeCol(columnas, 9),
                        Classroom = SafeCol(columnas, 10),
                        Code = SafeCol(columnas, 11),
                        ElapsedTimeSpan = SafeCol(columnas, 12)
                    };

                    listaDatos.Add(reporte);
                }
            }

            // Serializar a JSON
            var json = JsonConvert.SerializeObject(listaDatos);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviar a tu API
            var response = await _client.PostAsync("IngresarIntentos", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return (false, $"Error al subir archivo: {error}");
            }

            return (true, "Archivo subido correctamente.");
        }

        // HELPERS

        private static string SafeCol(string[] cols, int index)
        {
            return cols.Length > index ? cols[index].Replace("\"", "").Trim() : "";
        }

        private static DateTime? ParseFecha(string valor)
        {
            return DateTime.TryParseExact(
                valor,
                "MM/dd/yyyy hh:mm:ss tt",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var fecha
            ) ? fecha : null;
        }

        internal async Task<bool> SubirArchivoCSVAsync(string text)
        {
            throw new NotImplementedException();
        }
    }
}
