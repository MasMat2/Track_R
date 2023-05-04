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

        public IEnumerable<AccesoGridDto> ConsultarPorRolAcceso(int idRolAcceso)
        {
            return
                context.Acceso
                .Where(a => a.IdRolAcceso == idRolAcceso)
                .Select(a => new AccesoGridDto
                {
                    IdAcceso = a.IdAcceso,
                    Clave = a.Clave,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    AccesoPadre = a.IdAccesoPadreNavigation.Nombre,
                    TipoAcceso = a.IdTipoAccesoNavigation.Nombre,
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

        public IEnumerable<AccesoMenuDto> ConsultarPadrePorUsuario(int idUsuario)
        {
            var perfil =
                from up in context.Usuario
                where up.IdUsuario == idUsuario
                select up.IdPerfil;

            var accesoList =
                from ap in context.AccesoPerfil
                where
                (perfil.Contains(ap.IdPerfil)
                && ap.IdAccesoNavigation.IdAccesoPadre == null
                && (ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoMenu ||
                    ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoSistema)
                && ap.IdAccesoNavigation.IdRolAccesoNavigation.Clave == GeneralConstant.ClaveRolAccesoATI)
                orderby ap.IdAccesoNavigation.OrdenMenu ascending
                select new AccesoMenuDto
                {
                    IdAcceso = ap.IdAccesoNavigation.IdAcceso,
                    Nombre = ap.IdAccesoNavigation.Nombre,
                    Clave = ap.IdAccesoNavigation.Clave,
                    Url = ap.IdAccesoNavigation.Url,
                    ClaseIcono = ap.IdAccesoNavigation.IdIconoNavigation.Clase,
                    ClaveRolAcceso = ap.IdAccesoNavigation.IdRolAccesoNavigation.Clave,
                    ClaveTipoAcceso = ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave
                };

            return accesoList;
        }

        public IEnumerable<AccesoMenuDto> ConsultarMenuPorAccesoPadre(string claveAccesoPadre, int idUsuario)
        {
            var perfil =
                context.Usuario
                .Where(up => up.IdUsuario == idUsuario)
                .Select(up => up.IdPerfil);

            var accesoList =
                context.AccesoPerfil
                .Where(ap => perfil.Contains(ap.IdPerfil)
                    && (ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoMenu ||
                        ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoComponente)
                    && ap.IdAccesoNavigation.IdAccesoPadreNavigation.Clave == claveAccesoPadre
                )
                .OrderBy(ap =>  ap.IdAccesoNavigation.OrdenMenu)
                .Select(ap => new AccesoMenuDto
                {
                    IdAcceso = ap.IdAccesoNavigation.IdAcceso,
                    Nombre = ap.IdAccesoNavigation.Nombre,
                    Url = ap.IdAccesoNavigation.Url,
                    ClaseIcono = ap.IdAccesoNavigation.IdIconoNavigation.Clase
                })
                .ToList();

            return accesoList;
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


        public IEnumerable<AccesoMenuDto> ConsultarPadre(int idUsuarioSesion)
        {
            var compania = context.Usuario
                .Include(u => u.IdCompaniaNavigation)
                .Where(u => u.IdUsuario == idUsuarioSesion)
                .FirstOrDefault().IdCompaniaNavigation;

            if (compania.Clave == GeneralConstant.ClaveCompaniaBase)
            {
                return
                from a in context.Acceso
                where a.IdAccesoPadre == null
                orderby a.OrdenMenu
                select new AccesoMenuDto
                {
                    IdAcceso = a.IdAcceso,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    ClaseIcono = a.IdIconoNavigation.Clase
                };
            }

            var perfilAdministrador = context.Perfil
                .Where(p => p.IdCompaniaNavigation.Clave == GeneralConstant.ClaveCompaniaBase
                    && p.IdTipoCompania == compania.IdTipoCompania
                    && p.Nombre.ToLower().Contains("administrador"))
                .FirstOrDefault();

            return
                (from a in context.AccesoPerfil
                where a.IdAccesoNavigation.IdAccesoPadre == null
                && a.IdPerfil == perfilAdministrador.IdPerfil
                orderby a.IdAccesoNavigation.OrdenMenu
                select new AccesoMenuDto
                {
                    IdAcceso = a.IdAcceso,
                    Nombre = a.IdAccesoNavigation.Nombre,
                    Url = a.IdAccesoNavigation.Url,
                    ClaseIcono = a.IdAccesoNavigation.IdIconoNavigation.Clase
                }).ToList();
        }


        public IEnumerable<AccesoMenuDto> ConsultarHijosPorUsuario(int idPerfil, int idAccesoPadre)
        {
            var accesoList =
                from ap in context.AccesoPerfil
                where ap.IdPerfil == idPerfil
                && ap.IdAccesoNavigation.IdAccesoPadre == idAccesoPadre
                && (ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoMenu ||
                    ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoComponente)
                orderby ap.IdAccesoNavigation.OrdenMenu ascending
                select new AccesoMenuDto
                {
                    IdAcceso = ap.IdAccesoNavigation.IdAcceso,
                    Nombre = ap.IdAccesoNavigation.Nombre,
                    Url = ap.IdAccesoNavigation.Url,
                    ClaseIcono = ap.IdAccesoNavigation.IdIconoNavigation.Clase
                };

            return accesoList;
        }

        public IEnumerable<AccesoMenuDto> ConsultarTodosPorPerfil(int idPerfil)
        {
            var accesoList =
                from ap in context.AccesoPerfil
                where ap.IdPerfil == idPerfil &&
                (ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoMenu ||
                 ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave == GeneralConstant.ClaveTipoAccesoComponente) &&
                 ap.IdAccesoNavigation.IdRolAccesoNavigation.Clave == GeneralConstant.ClaveRolAccesoATI
                orderby ap.IdAccesoNavigation.OrdenMenu ascending
                select new AccesoMenuDto
                {
                    IdAcceso = ap.IdAccesoNavigation.IdAcceso,
                    Nombre = ap.IdAccesoNavigation.Nombre,
                    Url = ap.IdAccesoNavigation.Url,
                    ClaseIcono = ap.IdAccesoNavigation.IdIconoNavigation.Clase,
                    IdAccesoPadre = ap.IdAccesoNavigation.IdAccesoPadre,
                    ClaveRolAcceso = ap.IdAccesoNavigation.IdRolAccesoNavigation.Clave,
                    ClaveTipoAcceso = ap.IdAccesoNavigation.IdTipoAccesoNavigation.Clave
                };

            return accesoList;
        }

        public IEnumerable<Acceso> ConsultarHijosPorPerfil(int idPerfil, int idAccesoPadre)
        {
            var accesoList =
                from ap in context.AccesoPerfil
                where ap.IdPerfil == idPerfil
                && ap.IdAccesoNavigation.IdAccesoPadre == idAccesoPadre
                select ap.IdAccesoNavigation;

            return accesoList.ToList();
        }

        public IEnumerable<AccesoMenuDto> ConsultarHijos(int idAccesoPadre)
        {
            var accesoList =
                from a in context.Acceso
                where a.IdAccesoPadre == idAccesoPadre
                select new AccesoMenuDto
                {
                    IdAcceso = a.IdAcceso,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    ClaseIcono = a.IdIconoNavigation.Clase,
                };

            return accesoList.ToList();
        }

        public IEnumerable<AccesoMenuDto> ConsultarTodos()
        {
            var accesoList =
                from a in context.Acceso
                select new AccesoMenuDto
                {
                    IdAcceso = a.IdAcceso,
                    Nombre = a.Nombre,
                    Url = a.Url,
                    ClaseIcono = a.IdIconoNavigation.Clase,
                    IdAccesoPadre = a.IdAccesoPadre
                };

            return accesoList.ToList();
        }

        public IEnumerable<AccesoMenuDto> ConsultarTodosPorUsuario(int idUsuario)
        {
            var compania = context.Usuario
              .Include(u => u.IdCompaniaNavigation)
              .Where(u => u.IdUsuario == idUsuario)
              .FirstOrDefault().IdCompaniaNavigation;

            if (compania.Clave == GeneralConstant.ClaveCompaniaBase)
            {
                return
                (from a in context.Acceso
                 select new AccesoMenuDto
                 {
                     IdAcceso = a.IdAcceso,
                     Nombre = a.Nombre,
                     Url = a.Url,
                     ClaseIcono = a.IdIconoNavigation.Clase,
                     IdAccesoPadre = a.IdAccesoPadre
                 }).ToList();
            }

            var perfilAdministrador = context.Perfil
                .Where(p => p.IdCompaniaNavigation.Clave == GeneralConstant.ClaveCompaniaBase
                    && p.IdTipoCompania == compania.IdTipoCompania
                    && p.Nombre.ToLower().Contains("administrador"))
                .FirstOrDefault();


            return
                (from a in context.AccesoPerfil
                where a.IdPerfil == perfilAdministrador.IdPerfil
                select new AccesoMenuDto
                {
                    IdAcceso = a.IdAcceso,
                    Nombre = a.IdAccesoNavigation.Nombre,
                    Url = a.IdAccesoNavigation.Url,
                    ClaseIcono = a.IdAccesoNavigation.IdIconoNavigation.Clase,
                    IdAccesoPadre = a.IdAccesoNavigation.IdAccesoPadre
                }).ToList();
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
