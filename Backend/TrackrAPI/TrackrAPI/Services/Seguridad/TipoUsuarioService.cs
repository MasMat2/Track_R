using TrackrAPI.Dtos;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;

namespace TrackrAPI.Services.Seguridad
{
    public class TipoUsuarioService
    {
        private ITipoUsuarioRepository tipoUsuarioRepository;

        public TipoUsuarioService(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            this.tipoUsuarioRepository = tipoUsuarioRepository;
        }

        public TipoUsuarioDto ConsultarDto(string clave)
        {
            return tipoUsuarioRepository.ConsultarDto(clave);
        }
        public IEnumerable<TipoUsuarioDto> ConsultarTiposUsuarioSelector()
        {
            return tipoUsuarioRepository.ConsultarTiposUsuarioSelector();
        }
    }
}
