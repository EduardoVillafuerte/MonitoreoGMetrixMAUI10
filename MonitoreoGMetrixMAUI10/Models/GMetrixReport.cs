using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MonitoreoGMetrixMAUI10.Models
{
    public class GMetrixReport
    {
        [DisplayName("Prueba")]
        public string? Test { get; set; }
        [DisplayName("Nombres")]
        public string? FirstName { get; set; }
        [DisplayName("Apellidos")]
        public string? LastName {get; set;}
        [DisplayName("Numero de estudiante")]
        public string? StudentNumber {get; set;}
        [DisplayName("Correo electrónico")]
        [EmailAddress]
        public string? UserName {get; set;}
        [DisplayName("Dia de Finalización")]
        public DateTime? CompleteDate {get; set;}
        [DisplayName("Modo")]
        public string? Mode {get; set;}
        [DisplayName("Puntuación Porcentual")]
        public string? ScorePercent {get; set;}
        [DisplayName("Puntuación")]
        public string? Score {get; set;}
        [DisplayName("Clase NRC")]
        public string? Classroom {get; set;}
        [DisplayName("Código")]
        public string? Code {get; set;}
        [DisplayName("Duración de la prueba")]
        public string? ElapsedTimeSpan { get; set;}


    }
}
