namespace MonitoreoGMetrixMAUI10.Models
{
    public class Periodo
    {
        public int PeriodoID { get; set; }
        public string? Codigo { get; set; }
    }
    
    public class Facultad
    {
        public int? FacultadID { get; set; }
        public string? Nombre { get; set; }
    }

    public class Carrera
    {
        public int CarreraID { get; set; }
        public string? Nombre { get; set; }
    }

    public class Certificacion
    {
        public int CertificacionId { get; set; }
        public string? Nombre { get; set; }
    }

    public class Aptos
    {
        public string? UserName { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public string? nrc { get; set; }
    }
}
