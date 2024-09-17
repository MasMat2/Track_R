using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;
using TrackrAPI.Repositorys.Catalogo;

namespace TrackrAPI.Services.Seguridad
{
    public class EspecialidadUsuarioService
    {
        private readonly IEspecialidadUsuarioRepository _especialidadUsuarioRepository;

        public EspecialidadUsuarioService(IEspecialidadUsuarioRepository especialidadUsuarioRepository)
        {
            _especialidadUsuarioRepository = especialidadUsuarioRepository;
        }

        public async Task Agregar(EspecialidadUsuario especialidadUsuario)
        {
            var especialidadUsuarioExistente = await _especialidadUsuarioRepository.ConsultarPorUsuario((int) especialidadUsuario.IdUsuario, (int) especialidadUsuario.IdEspecialidad);

            if(especialidadUsuarioExistente != null)
            {
                await _especialidadUsuarioRepository.EditarAsync(especialidadUsuario);
            }else{
                await _especialidadUsuarioRepository.AgregarAsync(especialidadUsuario);
            }
        }
    }
}
