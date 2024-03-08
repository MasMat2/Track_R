using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using TrackrAPI.Repositorys.Distribucion;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using TrackrAPI.Helpers;

namespace TrackrAPI.Services.Seguridad
{
    public class UsuarioLocacionService
    {
        private IUsuarioLocacionRepository usuarioLocacionRepository;
        private UsuarioLocacionValidatorService usuarioLocacionValidatorService;
        private IPerfilRepository perfilRepository;
        private ICompaniaRepository companiaRepository;
        private ITipoEmpleadoRepository tipoEmpleadoRepository;
        private IUsuarioRepository usuarioRepository;
        private IHospitalRepository hospitalRepository;
        public UsuarioLocacionService(IUsuarioLocacionRepository usuarioLocacionRepository,
            UsuarioLocacionValidatorService usuarioLocacionValidatorService,
            IPerfilRepository perfilRepository,
            ICompaniaRepository companiaRepository,
            ITipoEmpleadoRepository tipoEmpleadoRepository,
            IUsuarioRepository usuarioRepository,
            IHospitalRepository hospitalRepository)
        {
            this.usuarioLocacionRepository = usuarioLocacionRepository;
            this.usuarioLocacionValidatorService = usuarioLocacionValidatorService;
            this.perfilRepository = perfilRepository;
            this.companiaRepository = companiaRepository;
            this.tipoEmpleadoRepository = tipoEmpleadoRepository;
            this.usuarioRepository = usuarioRepository;
            this.hospitalRepository = hospitalRepository;
        }

        public IEnumerable<UsuarioLocacionDto> ConsultarPorUsuario(int idUsuario)
        {
            return usuarioLocacionRepository.ConsultarPorUsuario(idUsuario);
        }

        public void Agregar(UsuarioLocacion usuarioLocacion)
        {
            usuarioLocacionValidatorService.ValidarAgregar(usuarioLocacion);
            usuarioLocacionRepository.Agregar(usuarioLocacion);
        }

        public void Editar(UsuarioLocacion usuarioLocacion)
        {
            usuarioLocacionValidatorService.ValidarEditar(usuarioLocacion);
            usuarioLocacionRepository.Editar(usuarioLocacion);
        }

        public void Eliminar(int idUsuarioLocacion)
        {
            var usuarioLocacion = usuarioLocacionRepository.Consultar(idUsuarioLocacion);
            usuarioLocacionRepository.Eliminar(usuarioLocacion);
        }

    }
}