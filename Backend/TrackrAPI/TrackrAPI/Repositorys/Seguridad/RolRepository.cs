using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;


namespace TrackrAPI.Repositorys.Seguridad
{
    public class RolRepository: Repository<Rol>, IRolRepository
    {

        public RolRepository(TrackrContext context): base(context)
        {
            base.context = context;
        }

        public Rol Consultar(int idRol)
        {
            return context.Rol.Where(r => r.IdRol == idRol).FirstOrDefault();
        }

        public RolDto ConsultarDto(int idRol)
        {
            return context.Rol
                      .Where(p => p.IdRol == idRol)
                      .Select(p => new RolDto
                      {
                          IdRol = p.IdRol,
                          Clave = p.Clave,
                          Nombre = p.Nombre,
                          Filtrado = p.Filtrado != null && (bool)p.Filtrado,
                          IdCompania = p.IdCompania
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<RolGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            int? idCompaniaBase = context.Compania.FirstOrDefault(cc => cc.Clave == GeneralConstant.ClaveCompaniaBase)?.IdCompania;

            return context.Rol
                      .Where(tp => tp.IdCompania == idCompania || tp.IdCompania == idCompaniaBase)
                      .OrderBy(tp => tp.Clave)
                      .Select(tp => new RolGridDto(
                          tp.IdRol,
                          tp.Clave,
                          tp.Nombre,
                          tp.Filtrado,
                          tp.IdCompania))
                      .ToList();
        }

        public IEnumerable<RolDto> ConsultarPorUsuario(int idUsuario)
        {
            return context.UsuarioRol
                      .Where(ur => ur.IdUsuario == idUsuario)
                      .Select(tp => new RolDto
                      {
                          IdRol = tp.IdRol,
                          Clave = tp.IdRolNavigation.Clave,
                      })
                      .ToList();
        }

        public Rol ConsultarPorClave(string clave)
        {
            return context.Rol
                      .Where(e => e.Clave == clave)
                      .FirstOrDefault();
        }

        public Rol ConsultarPorNombre(string nombre)
        {
            return context.Rol
                      .Where(e => e.Nombre.ToLower() == nombre.ToLower())
                      .FirstOrDefault();
        }

        public Rol ConsultarDependencias(int idRol)
        {
            return context.Rol
                .Include(e => e.TipoComisionDetalle)
                .Include(e => e.UsuarioRol)
                .Where(e => e.IdRol == idRol)
                .FirstOrDefault();
        }

        public IEnumerable<Rol> ConsultarGeneral()
        {
            var rolList = from r in context.Rol select r;
            return rolList;
        }

        public IEnumerable<RolDto> ConsultarTodosParaSelector()
        {
            return context.Rol
                .OrderBy(r => r.Nombre)
                .Select(r => new RolDto
                {
                    IdRol = r.IdRol,
                    Nombre = r.Nombre,
                    Clave = r.Clave,
                    Filtrado = r.Filtrado != null && (bool)r.Filtrado
                })
                .ToList();
        }

    }
}
