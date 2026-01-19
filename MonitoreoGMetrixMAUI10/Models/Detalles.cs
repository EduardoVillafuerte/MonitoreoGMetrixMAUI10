namespace MonitoreoGMetrixMAUI10.Models
{
    public class DetallesModel
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal? promedio {get;set;}
        public string? tiempoPromedio {get;set;}
        public bool IsSelected { get; set; }
    }
    public class DetallesFiltrados
    {
        public string? UserName { get; set; }
        public string? pruebaNombre {get;set;}
        public int? score {get;set;}
        public DateTime? completeDate {get;set;}
        public string? modo {get;set;}
        public string? tiempo {get;set;}
    }
}
