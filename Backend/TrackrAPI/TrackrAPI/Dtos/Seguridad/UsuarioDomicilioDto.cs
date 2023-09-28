using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Seguridad
{
    public class UsuarioDomicilioDto{
        public string? Estado;

        public virtual Estado? IdEstadoNavigation { get; set;}
        
    }
}