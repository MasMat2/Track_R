using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class UsuarioRolRepository : Repository<UsuarioRol>, IUsuarioRolRepository
    {
        public UsuarioRolRepository(TrackrContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<UsuarioRolDto> ConsultarPorUsuario(int idUsuario)
        {
            return context.UsuarioRol
                .OrderBy(ur => ur.IdRolNavigation.Nombre)
                .Where(ur => ur.IdUsuario == idUsuario)
                .Select(ur => new UsuarioRolDto
                {
                    IdUsuarioRol = ur.IdUsuarioRol,
                    IdUsuario = ur.IdUsuario,
                    IdRol = ur.IdRol
                })
                .ToList();
        }

        public IEnumerable<UsuarioRolGridDto> ConsultarPorUsuarioParaGrid(int idUsuario)
        {
            return context.UsuarioRol
                .OrderBy(ur => ur.IdRolNavigation.Nombre)
                .Where(ur => ur.IdUsuario == idUsuario)
                .Select(ur => new UsuarioRolGridDto
                {
                    IdUsuarioRol = ur.IdUsuarioRol,
                    IdRol = ur.IdRol,
                    IdCuentaContable = ur.IdConceptoNavigation.IdCuentaContable,
                    NombreRol = ur.IdRolNavigation.Nombre,
                    CuentaContable = ur.IdCuentaContableNavigation.NumeroNombre(),
                    ClaveRol = ur.IdRolNavigation.Clave,
                    Concepto = ur.IdConceptoNavigation.Nombre,
                    IdConcepto = ur.IdConcepto
                })
                .ToList();
        }
        public UsuarioRol Consultar(int idUsuarioRol)
        {
            return context.UsuarioRol
                .Where(ur => ur.IdUsuarioRol == idUsuarioRol)
                .FirstOrDefault();
        }

        public UsuarioRol Consultar(int idUsuario, int idRol)
        {
            return context.UsuarioRol
                .Where(ur => ur.IdUsuario == idUsuario && ur.IdRol == idRol)
                .FirstOrDefault();
        }
    }
}
