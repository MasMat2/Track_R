using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Seguridad
{
    public class AccesoService
    {
        private readonly IAccesoRepository accesoRepository;
        private readonly AccesoValidatorService accesoValidatorService;
        private readonly IRolAccesoRepository rolAccesoRepository;
        private readonly JerarquiaAccesoEstructuraService jerarquiaAccesoEstructuraService;
        private readonly IPerfilRepository perfilRepository;
        private readonly UsuarioService usuarioService;

        public AccesoService(IAccesoRepository accesoRepository,
            IRolAccesoRepository rolAccesoRepository,
            AccesoValidatorService accesoValidatorService,
            JerarquiaAccesoEstructuraService jerarquiaAccesoEstructuraService,
            IPerfilRepository perfilRepository,
            UsuarioService usuarioService)
        {
            this.accesoRepository = accesoRepository;
            this.accesoValidatorService = accesoValidatorService;
            this.rolAccesoRepository = rolAccesoRepository;
            this.jerarquiaAccesoEstructuraService = jerarquiaAccesoEstructuraService;
            this.perfilRepository = perfilRepository;
            this.usuarioService = usuarioService;
        }

        public AccesoDto ConsultarDto(int id)
        {
            return accesoRepository.ConsultarDto(id);
        }

        public Acceso ConsultarPorClave(string claveAcceso)
        {
            return accesoRepository.ConsultarPorClave(claveAcceso);
        }

        public IEnumerable<AccesoGridDto> ConsultarParaGrid()
        {
            return accesoRepository.ConsultarGeneral();
        }

        public IEnumerable<AccesoDto> ConsultarPorPerfil(int idPerfil)
        {
            return accesoRepository.ConsultarPorPerfil(idPerfil);
        }

        public List<AccesoGridDto> ConsultarHijosJerarquia(List<AccesoGridDto> accesosPadre, List<AccesoGridDto> accesos)
        {
            foreach (AccesoGridDto padre in accesosPadre)
            {
                padre.Hijos = accesos.Where(a => a.IdAccesoPadre == padre.IdAcceso).ToList();

                if (padre.Hijos.Any())
                {
                    ConsultarHijosJerarquia(padre.Hijos, accesos);
                }
            }

            return accesos;
        }

        public bool TieneAcceso(int idUsuario, string codigoAcceso)
        {
            return accesoRepository.TieneAcceso(idUsuario, codigoAcceso);
        }

        public IEnumerable<AccesoGridDto> ConsultarParaReporteArbol(int idRolAcceso)
        {
            string clave = "";

            if (idRolAcceso > 0)
            {
                var rolAcceso = rolAccesoRepository.Consultar(idRolAcceso);
                clave = rolAcceso.Clave;
            }

            var accesos = accesoRepository.ConsultarParaReporteArbol(clave).ToList();
            ConsultarHijosJerarquia(accesos, accesos);

            foreach (AccesoGridDto acceso in accesos)
            {
                if (acceso.Hijos.Any())
                {
                    acceso.CantidadDescendientes = ObtenerDescendientesJerarquia(acceso.Hijos);
                }
                else
                {
                    acceso.CantidadDescendientes = 0;
                }
            }

            return accesos;
        }

        private int ObtenerDescendientesJerarquia(List<AccesoGridDto> accesosHijos)
        {
            var r = accesosHijos.Select(a => ObtenerDescendientesJerarquia(a.Hijos)).ToArray();
            return r.Any() ? r.Sum(p => p + 1) : 0;
        }

        public IEnumerable<AccesoMenuDto> ConsultarMenu(int idUsuarioSesion)
        {
            // Consultar al usuario en sesión
            var usuario = usuarioService.Consultar(idUsuarioSesion);
            if (usuario is null)
            {
                throw new CdisException("El usuario no existe");
            }

            // Consultar el perfil del usuario
            var idPerfil = usuario.IdPerfil;
            if (idPerfil is null)
            {
                throw new CdisException("El usuario no tiene perfil asignado");
            }

            var perfil = perfilRepository.Consultar(idPerfil.Value);
            if (perfil is null)
            {
                throw new CdisException("El perfil no existe");
            }

            // Consultar los accesos del perfil
            var accesos = accesoRepository
                .ConsultarPorPerfilParaMenu(idPerfil.Value)
                .ToList();

            // TODO: 2023-07-31 -> ¿Qué se hace si el perfil no tiene una jerarquía?
            var idJerarquiaAcceso = perfil.IdJerarquiaAcceso ?? 0;
            var estructuras = jerarquiaAccesoEstructuraService
                .ConsultarPorJerarquiaAcceso(idJerarquiaAcceso)
                .ToList();

            var estructurasPadre = estructuras
                .Where(je => je.IdJerarquiaAccesoEstructuraPadre == null)
                .Reverse()
                .ToList();

            Stack<JerarquiaAccesoEstructura> stack = new();
            var menu = new List<AccesoMenuDto>();

            // Agregar los padres al stack y sus accesos al menú
            foreach (var estructuraPadre in estructurasPadre)
            {
                stack.Push(estructuraPadre);
            }

            while (stack.Count > 0)
            {
                var estructura = stack.Pop();

                // Buscar el acceseo correspondiente a la estructura
                var acceso = accesos.Find(a => a.IdAcceso == estructura.IdAcceso);

                // Si el usuario no cuenta con el acceso, se omite la estructura actual y todos sus hijos
                if (acceso is null || acceso.ClaveTipoAcceso == GeneralConstant.ClaveTipoAccesoSistema)
                {
                    continue;
                }

                // Se consultan los hijos de la estructura actual al stack
                var estructurasHijas = estructuras
                    .Where(je => je.IdJerarquiaAccesoEstructuraPadre == estructura.IdJerarquiaAccesoEstructura)
                    .Reverse()
                    .ToList();

                // Se agregan los hijos al stack
                foreach (var estructuraHija in estructurasHijas)
                {
                    stack.Push(estructuraHija);
                }

                // Se asignan los hijos al acceso
                acceso.Hijos = accesos
                    .Where(a => estructurasHijas.Any(e => e.IdAcceso == a.IdAcceso))
                    .Where(a => a.ClaveTipoAcceso != GeneralConstant.ClaveTipoAccesoEvento)
                    .ToList();
            }

            return estructurasPadre
                .Reverse<JerarquiaAccesoEstructura>()
                .Select(je => accesos.Find(a => a.IdAcceso == je.IdAcceso))
                .Where(a => a is not null);
        }

        public Acceso Agregar(Acceso acceso)
        {
            accesoValidatorService.ValidarAgregar(acceso);
            accesoRepository.Agregar(acceso);
            return acceso;
        }

        public void Editar(Acceso acceso)
        {
            accesoValidatorService.ValidarEditar(acceso);
            accesoRepository.Editar(acceso);
        }

        public void Eliminar(int idAcceso)
        {
            Acceso acceso = accesoRepository.Consultar(idAcceso);
            accesoValidatorService.ValidarEliminar(idAcceso);
            accesoRepository.Eliminar(acceso);
        }
    }
}
