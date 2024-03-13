using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Seguridad;
using System.Collections.Generic;


namespace TrackrAPI.Services.Seguridad
{
    public class RolService
    {

        private IRolRepository rolRepository;
        private RolValidatorService rolValidatorService;

        public RolService(IRolRepository rolRepository,
            RolValidatorService rolValidatorService)
        {
            this.rolRepository = rolRepository;
            this.rolValidatorService = rolValidatorService;

        }

        public Rol Consultar(int idRol)
        {
            return rolRepository.Consultar(idRol);
        }

        public RolDto ConsultarDto(int idRol)
        {
            var rol = rolRepository.ConsultarDto(idRol);
            rolValidatorService.ValidarExistencia(rol);
            return rol;
        }

        public IEnumerable<RolGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return rolRepository.ConsultarTodosParaGrid(idCompania);
        }

        public IEnumerable<RolDto> ConsultarTodosParaSelector()
        {
            return rolRepository.ConsultarTodosParaSelector();
        }

        public IEnumerable<RolDto> ConsultarPorUsuario(int idUsuario)
        {
            return rolRepository.ConsultarPorUsuario(idUsuario);
        }

        public void Agregar(Rol rol)
        {
            rolValidatorService.ValidarAgregar(rol);
            rolRepository.Agregar(rol);
        }

        public void Editar(Rol rol)
        {
            rolValidatorService.ValidarEditar(rol);
            rolRepository.Editar(rol);
        }

        public void Eliminar(int idRol)
        {
            var rol = rolRepository.Consultar(idRol);
            rolValidatorService.ValidarEliminar(idRol);
            rolRepository.Eliminar(rol);
        }

    }
}
