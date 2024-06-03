using TrackrAPI.Dtos;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Perfil
{
    public class InformacionGeneralDTO
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdGenero { get; set; }
        public decimal Peso { get; set; }
        public decimal Cintura { get; set; }
        public decimal Estatura { get; set; }
        public string Correo { get; set; }
        public string CorreoPersonal { get; set; }
        public string TelefonoMovil { get; set; }
        public bool? CorreoConfirmado { get; set; }
    }
}