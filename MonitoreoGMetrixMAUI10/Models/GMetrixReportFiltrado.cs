using System.ComponentModel;

namespace MonitoreoGMetrixMAUI10.Models
{
    public class GMetrixReportFiltrado
    {
        [DisplayName("Código de Materia")]
        public string? materiaCodigo { get; set; }
        [DisplayName("NRC")]
        public string? NRC {get;set;}   
        [DisplayName("Certificado")]
        public string? certificadoNombre {get;set;}
        public string? nombreMateria {get;set;}
        [DisplayName("Número de estudiantes")]
        public int? totalUsuarios {get;set;}
        [DisplayName("Promedio General")]
        public decimal? promedio {get;set;}
        public int? periodo {get;set;}
        public string? periodoCodigo {get;set;}
        public string? nombreCarrera {get;set;}
    }

    public class CertificacionViewModel
    {
        public string? NombreCertificacion { get; set; }
        public List<GMetrixReportFiltrado> Materias { get; set; } = new();
    }

}
