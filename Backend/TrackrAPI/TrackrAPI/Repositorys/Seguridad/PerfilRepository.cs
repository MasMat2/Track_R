using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class PerfilRepository : Repository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Perfil ConsultarAdministradorPorTipoCompania(int idTipoCompania, int idCompania)
        {
            return context.Perfil
                .Where(p => p.Nombre.ToLower().Contains("administrador") &&
                            p.IdCompania == idCompania &&
                            (p.IdTipoCompania == idTipoCompania ||
                                (p.IdJerarquiaAcceso > 0 && p.IdJerarquiaAccesoNavigation.JerarquiaAccesoTipoCompania.Any(c => c.IdTipoCompania == idTipoCompania))
                            ))
                            .FirstOrDefault();
        }

        public Perfil ConsultarPorClave(string clave, int idCompania)
        {
            return context.Perfil
                .Where(p => p.Clave == clave && p.IdCompania == idCompania)
                .FirstOrDefault();
        }

        public Perfil Consultar(int idPerfil)
        {
            var perfil = from p in context.Perfil where p.IdPerfil == idPerfil select p;
            return perfil.FirstOrDefault();
        }

        public PerfilDto ConsultarDto(int idPerfil)
        {
            return
                context.Perfil
                .Where(p => p.IdPerfil == idPerfil)
                .Select(p => new PerfilDto
                {
                    IdPerfil = p.IdPerfil,
                    Nombre = p.Nombre,
                    Clave = p.Clave,
                    IdCompania = p.IdCompania,
                    IdTipoCompania = p.IdTipoCompania,
                    IdJerarquiaAcceso = p.IdJerarquiaAcceso
                })
                .FirstOrDefault();
        }

        public Perfil ConsultarPorNombre(int idCompania, string nombre)
        {
            var perfil =
                from p in context.Perfil
                where (p.Nombre == nombre
                && p.IdCompania == idCompania)
                select p;
            return perfil.FirstOrDefault();
        }

        public Perfil ConsultarDependencia(int idPerfil)
        {
            var perfil =
                from p in context.Perfil
                .Include(p => p.Usuario)
                .Include(p => p.AccesoPerfil)
                where p.IdPerfil == idPerfil
                select p;
            return perfil.FirstOrDefault();
        }

        public IEnumerable<PerfilDto> ConsultarGeneral(int idCompania)
        {
            var perfilList = from p in context.Perfil
                             .Where(p => p.IdCompania == idCompania)
                             .OrderBy(p => p.IdTipoCompania).ThenBy(p => p.Nombre)
                             select new PerfilDto
                             {
                                 IdPerfil = p.IdPerfil,
                                 Clave = p.Clave,
                                 IdCompania = p.IdCompania,
                                 IdTipoCompania = p.IdTipoCompania,
                                 Nombre = p.Nombre,
                                 NombreTipoCompania = p.IdTipoCompaniaNavigation.Nombre,
                                 NombreJerarquia = p.IdJerarquiaAccesoNavigation != null ?
                                                   p.IdJerarquiaAccesoNavigation.Nombre : ""
                             };
            return perfilList.ToList();
        }

        public IEnumerable<Perfil> ConsultarPorCompaniaBase(int idCompania)
        {
            var perfilList = from p in context.Perfil
                 .Where(p => p.IdCompania == idCompania)
                             select p;
            return perfilList.ToList();
        }

        public IEnumerable<Perfil> ConsultarPorTipoCompania(int idTipoCompania)
        {
            var perfilList = from p in context.Perfil
                 .Include(p => p.IdJerarquiaAccesoNavigation.JerarquiaAccesoTipoCompania)
                 .Where(p => (p.IdTipoCompania == idTipoCompania ||
                              p.IdJerarquiaAcceso > 0 && p.IdJerarquiaAccesoNavigation.JerarquiaAccesoTipoCompania.Any(c => c.IdTipoCompania == idTipoCompania))
                              && p.IdCompaniaNavigation.Clave == GeneralConstant.ClaveCompaniaBase)
                             select p;
            return perfilList.ToList();
        }

        public IEnumerable<PerfilDto> ConsultarTodosParaSelector(int idCompania)
        {
            return context.Perfil
                .Where(p => p.IdCompania == idCompania)
                .OrderBy(p => p.Nombre)
                .Select(p => new PerfilDto
                {
                    IdPerfil = p.IdPerfil,
                    Nombre = p.Nombre
                })
                .ToList();
        }

        public Perfil ConsultarUltimoAgregado(bool esCompaniaBase)
        {
            if (esCompaniaBase)
            {
                return context.Perfil
                    .Where(p => p.IdCompaniaNavigation.Clave == GeneralConstant.ClaveCompaniaBase)
                        .OrderByDescending(c => c.IdPerfil)
                        .FirstOrDefault();
            }
            else
            {
                return context.Perfil
                    .Where(p => p.IdCompaniaNavigation.Clave != GeneralConstant.ClaveCompaniaBase && p.Clave.ToUpper().Contains("BASE") == false)
                        .OrderByDescending(c => c.IdPerfil)
                        .FirstOrDefault();
            }

        }
    }
}
