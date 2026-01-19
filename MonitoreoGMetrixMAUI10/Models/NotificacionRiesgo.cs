using Microsoft.Maui.Graphics;

namespace MonitoreoGMetrixMAUI10.Models
{
    public class NotificacionRiesgo
    {
        public string Estudiante { get; set; }
        public string NivelRiesgo { get; set; } // "Alto", "Medio", "Bajo"
        public string MensajeIA { get; set; }
        public float ProbabilidadReprobacion { get; set; }

        // Propiedad visual para cambiar el color del texto según el riesgo
        public Color ColorRiesgo
        {
            get
            {
                if (NivelRiesgo == "Alto") return Colors.Red;
                if (NivelRiesgo == "Medio") return Colors.Orange;
                return Colors.Green;
            }
        }
    }
}