using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IEspecialidadUsuarioRepository : IRepository<EspecialidadUsuario>
    {
        Task<EspecialidadUsuario>? ConsultarPorUsuario(int idUsuario, int idEspecialidad);
        
        Task AgregarAsync(EspecialidadUsuario especialidadUsuario);
        Task EliminarAsync(int idEspecialidadUsuario);
        Task EditarAsync(EspecialidadUsuario especialidadUsuario);
    }
}