using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class AccesoRepository : Repository<Acceso>, IAccesoRepository
    {
        public AccesoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<AccesoGridDto> ConsultarGeneral()
        {
            return
                context.Acceso
                .Select(a => new AccesoGridDto
                {
                    IdAcceso = a.IdAcceso,
                    Clave = a.Clave,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    AccesoPadre = a.IdAccesoPadreNavigation.Nombre,
                    TipoAcceso = a.IdTipoAccesoNavigation.Nombre,
                    IdRolAcceso = a.IdRolAcceso
                })
                .ToList();
        }

        public AccesoDto ConsultarDto(int idAcceso)
        {
            return
                context.Acceso
                .Where(a => a.IdAcceso == idAcceso)
                .Select(a => new AccesoDto
                {
                    IdAcceso = a.IdAcceso,
                    Clave = a.Clave,
                    Nombre = a.Nombre,
                    IdIcono = a.IdIcono,
                    OrdenMenu = a.OrdenMenu,
                    Url = a.Url,
                    IdAccesoPadre = a.IdAccesoPadre,
                    IdTipoAcceso = a.IdTipoAcceso,
                    IdRolAcceso = a.IdRolAcceso,
                    UrlVideoAyuda = a.UrlVideoAyuda,
                    Descripcion = a.Descripcion
                })
                .FirstOrDefault();
        }

        public Acceso Consultar(int idAcceso)
        {
            var accesoList =
                from a in context.Acceso
                .Include(a => a.IdTipoAccesoNavigation)
                where a.IdAcceso == idAcceso
                select a;

            return accesoList.FirstOrDefault();
        }

        public Acceso ConsultarDependencia(int idAcceso)
        {
            var accesoList =
                from a in context.Acceso
                    .Include(a => a.AccesoPerfil)
                where a.IdAcceso == idAcceso
                select a;

            return accesoList.FirstOrDefault();
        }

        public Acceso ConsultarPorClave(string clave)
        {
            var accesoList =
                from a in context.Acceso
                where a.Clave == clave
                select a;

            return accesoList.FirstOrDefault();
        }

        public IEnumerable<AccesoDto> ConsultarPorPerfil(int idPerfil)
        {
            return
                context.AccesoPerfil
                .Where(ap => ap.IdPerfil == idPerfil)
                .Select(ap => new AccesoDto
                {
                    IdAcceso = ap.IdAcceso,
                    Clave = ap.IdAccesoNavigation.Clave,
                    Nombre = ap.IdAccesoNavigation.Nombre,
                    IdIcono = ap.IdAccesoNavigation.IdIcono,
                    OrdenMenu = ap.IdAccesoNavigation.OrdenMenu,
                    Url = ap.IdAccesoNavigation.Url,
                    IdAccesoPadre = ap.IdAccesoNavigation.IdAccesoPadre,
                    IdTipoAcceso = ap.IdAccesoNavigation.IdTipoAcceso
                })
                .ToList();
        }




        public IEnumerable<AccesoMenuDto> ConsultarPorPerfilParaMenu(int idPerfil)
        {
            return context.AccesoPerfil
                .Where(ap => ap.IdPerfil == idPerfil)
                .Select(ap => new AccesoMenuDto
                {
                    IdAcceso = ap.IdAcceso,
                    Nombre = ap.IdAccesoNavigation.Nombre,
                    Clave = ap.IdAccesoNavigation.Clave,
                    Url = ap.IdAccesoNavigation.Url,
                    ClaseIcono = ap.IdAccesoNavigation.IdIconoNavigation.Clase,
                    Hijos = new List<AccesoMenuDto>(),
                    ClaveRolAcceso = ap.IdAccesoNavigation.IdRolAccesoNavigation.Clave,
                    ClaveTipoAcceso = ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave
                });
        }

        public IEnumerable<AccesoGridDto> ConsultarParaReporteArbol(string claveAccesoRol)
        {
            var accesos =
                context.Acceso
                .Include(a => a.InverseIdAccesoPadreNavigation)
                    .ThenInclude(h => h.InverseIdAccesoPadreNavigation)
                .Where(a => string.IsNullOrEmpty(claveAccesoRol) || a.IdRolAccesoNavigation.Clave == claveAccesoRol)
                .Select(a => new AccesoGridDto
                {
                    IdAcceso = a.IdAcceso,
                    Clave = a.Clave,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    AccesoPadre = a.IdAccesoPadreNavigation.Nombre,
                    TipoAcceso = a.IdTipoAccesoNavigation.Nombre,
                    OrdenMenu = a.OrdenMenu,
                    IdAccesoPadre = a.IdRolAccesoNavigation.Clave == GeneralConstant.ClaveRolAccesoATI && a.IdAccesoPadre == null ? 0 : a.IdAccesoPadre
                })
                .OrderBy(a => a.OrdenMenu)
                .ToList();
            return accesos;
        }

        public bool TieneAcceso(int idUsuario, string codigoAcceso)
        {
            var perfil =
                from up in context.Usuario
                where up.IdUsuario == idUsuario
                select up.IdPerfil;

            var accesoList =
                from ap in context.AccesoPerfil
                where perfil.Contains(ap.IdPerfil)
                && ap.IdAccesoNavigation.Clave == codigoAcceso
                select ap.IdAccesoNavigation;

            return accesoList.ToList().Count > 0;
        }
    }
}
