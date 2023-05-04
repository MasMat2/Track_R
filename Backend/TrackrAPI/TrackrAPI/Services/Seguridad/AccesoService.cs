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
        private IAccesoRepository accesoRepository;
        private AccesoValidatorService accesoValidatorService;
        private IUsuarioRepository usuarioRepository;
        private IRolAccesoRepository rolAccesoRepository;

        public AccesoService(IAccesoRepository accesoRepository,
            IUsuarioRepository usuarioRepository,
            IRolAccesoRepository rolAccesoRepository,
            AccesoValidatorService accesoValidatorService)
        {
            this.accesoRepository = accesoRepository;
            this.usuarioRepository = usuarioRepository;
            this.accesoValidatorService = accesoValidatorService;
            this.rolAccesoRepository = rolAccesoRepository;
        }

        public Acceso Consultar(int id)
        {
            return accesoRepository.Consultar(id);
        }

        public AccesoDto ConsultarDto(int id)
        {
            return accesoRepository.ConsultarDto(id);
        }
        public Acceso ConsultarPorClave(string claveAcceso)
        {
            return accesoRepository.ConsultarPorClave(claveAcceso);
        }

        public IEnumerable<AccesoGridDto> ConsultarGeneral()
        {
            return accesoRepository.ConsultarGeneral();
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

        public List<AccesoMenuDto> ConsultarMenu(int idUsuario)
        {
            List<AccesoMenuDto> accesosPadre = accesoRepository.ConsultarPadrePorUsuario(idUsuario).ToList();
            Usuario usuario = usuarioRepository.Consultar(idUsuario);

            var accesos = accesoRepository.ConsultarTodosPorPerfil((int)usuario.IdPerfil).ToList();

            ConsultarHijosPorUsuario(accesosPadre, accesos);
            return accesosPadre;
        }

        public IEnumerable<AccesoMenuDto> ConsultarMenuPorAccesoPadre(string claveAccesoPadre, int idUsuario)
        {
            return accesoRepository.ConsultarMenuPorAccesoPadre(claveAccesoPadre, idUsuario);
        }

        public List<AccesoMenuDto> ConsultarHijosPorUsuario(List<AccesoMenuDto> accesoList, List<AccesoMenuDto> accesos)
        {
            foreach (AccesoMenuDto acceso in accesoList)
            {
                if (!(acceso.Clave == GeneralConstant.ClaveAccesoSistemaDistribucion))
                {
                    acceso.Hijos = accesos.Where(a => a.IdAccesoPadre == acceso.IdAcceso).ToList();

                    if (acceso.Hijos.Count > 0)
                    {
                        ConsultarHijosPorUsuario(acceso.Hijos, accesos);
                    }
                }
                else
                {
                    acceso.Hijos = null;
                }
            }
            return accesoList;
        }

        public IEnumerable<AccesoDto> ConsultarPorPerfil(int idPerfil)
        {
            return accesoRepository.ConsultarPorPerfil(idPerfil);
        }

        public List<AccesoMenuDto> ConsultarArbol(int idUsuarioSesion)
        {
            List<AccesoMenuDto> accesosPadre = accesoRepository.ConsultarPadre(idUsuarioSesion).ToList();
            var accesos = accesoRepository.ConsultarTodosPorUsuario(idUsuarioSesion);
            ConsultarHijosMenu(accesosPadre, accesos);
            return accesosPadre;
        }

        public List<AccesoMenuDto> ConsultarHijosMenu(List<AccesoMenuDto> accesoList, IEnumerable<AccesoMenuDto> accesos)
        {
            foreach (AccesoMenuDto acceso in accesoList)
            {
                acceso.Hijos = accesos.Where(a => a.IdAccesoPadre == acceso.IdAcceso).ToList();

                if (acceso.Hijos.Any())
                {
                    ConsultarHijosMenu(acceso.Hijos, accesos);
                }
            }

            return accesoList;
        }

        public List<AccesoGridDto> ConsultarHijosJerarquia(List<AccesoGridDto> accesosPadre, List<AccesoGridDto> accesos)
        {
            foreach (AccesoGridDto padre in accesosPadre)
            {
                if (padre.Nombre == "Recetas")
                {
                    int a = 1;
                }

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

        public IEnumerable<AccesoGridDto> ConsultarPorRolAcceso(int idRolAcceso)
        {
            return accesoRepository.ConsultarPorRolAcceso(idRolAcceso);
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
    }
}
