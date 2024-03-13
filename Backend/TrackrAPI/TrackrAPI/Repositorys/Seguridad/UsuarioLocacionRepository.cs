using TrackrAPI.Repositorys;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TrackrAPI.Helpers;
using TrackrAPI.Dtos.Seguridad;
using System;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class UsuarioLocacionRepository : Repository<UsuarioLocacion>, IUsuarioLocacionRepository
    {
        public UsuarioLocacionRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

       public UsuarioLocacion Consultar(int idUsuarioLocacion)
       {
           return context.UsuarioLocacion
               .Where(u => u.IdUsuarioLocacion == idUsuarioLocacion)
               .FirstOrDefault();
       }

        public UsuarioLocacion Consultar(int idUsuario, int idLocacion)
        {
            return context.UsuarioLocacion
                .Where(u => u.IdUsuario == idUsuario && u.IdLocacion == idLocacion)
                .FirstOrDefault();
        }

        public IEnumerable<UsuarioLocacionDto> ConsultarPorUsuario(int idUsuario)
        {
            if (idUsuario == GeneralConstant.UsuarioMaestroAtisc)
            {
                return context.Hospital
                    .Where(u => u.IdCompania != null)
                    .Select(u => new UsuarioLocacionDto
                    {
                        IdUsuario = idUsuario,
                        IdCompania = (int) u.IdCompania,
                        IdLocacion = u.IdHospital,
                        IdPerfil = 1,
                        Compania = u.IdCompaniaNavigation.Nombre,
                        Locacion = u.Nombre,
                    })
                    .ToList();
            }

            return context.UsuarioLocacion
                .Where(u => u.IdUsuario == idUsuario)
                .Select(u => new UsuarioLocacionDto
                {
                    IdUsuarioLocacion = u.IdUsuarioLocacion,
                    IdUsuario = u.IdUsuario,
                    IdCompania = (int)u.IdLocacionNavigation.IdCompania,
                    IdLocacion = u.IdLocacion,
                    IdPerfil = u.IdPerfil,
                    Compania = u.IdLocacionNavigation.IdCompaniaNavigation.Nombre,
                    Locacion = u.IdLocacionNavigation.Nombre,
                    Perfil = u.IdPerfilNavigation.Nombre
                })
                .ToList();
        }

    }
}
