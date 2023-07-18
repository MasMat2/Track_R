using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente;

public class ExpedienteTrackrRepository : Repository<ExpedienteTrackr>, IExpedienteTrackrRepository
{
    public ExpedienteTrackrRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public ExpedienteTrackr Consultar(int idExpediente)
    {
        return context.ExpedienteTrackr
            .Where(et => et.IdExpediente == idExpediente)
            .FirstOrDefault();
    }

    public ExpedienteTrackr ConsultarPorNumero(string numero)
    {
        return context.ExpedienteTrackr
            .Where(et => et.Numero == numero)
            .FirstOrDefault();
    }

    public ExpedienteTrackr ConsultarPorUsuario(int idUsuario)
    {
        return context.ExpedienteTrackr
            .Include(et => et.ExpedientePadecimiento)
            .Where(et => et.IdUsuario == idUsuario)
            .FirstOrDefault();
    }

    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid()
    {
        return context.ExpedienteTrackr
            .Include(et => et.IdUsuarioNavigation)
            .Include(et => et.ExpedientePadecimiento)
            .ThenInclude(ep => ep.IdPadecimientoNavigation)
            .Select(et => new UsuarioExpedienteGridDTO
            {
                IdExpedienteTrackr = et.IdExpediente,
                IdUsuario = et.IdUsuario,
                NombreCompleto = et.IdUsuarioNavigation.ObtenerNombreCompleto(),
                Patologias = et.ExpedientePadecimiento.ObtenerPadecimientos(),
                Edad = (DateTime.Today.Year - et.FechaNacimiento.Year).ToString() + " años",
                TipoMime = et.IdUsuarioNavigation.ImagenTipoMime
            })
            .ToList();
    }
    public UsuarioExpedienteSidebarDTO ConsultarParaSidebar(int idUsuario)
    {

        var expedienteSidebarDTO = context.ExpedienteTrackr
            .Include(et => et.IdUsuarioNavigation)
                .ThenInclude(ep => ep.IdEstadoNavigation)
            .Include(et => et.ExpedientePadecimiento)
                .ThenInclude(ep => ep.IdPadecimientoNavigation)
            .Where(et => et.IdUsuario == idUsuario)
            .Select(et => new
            {
                Usuario = new UsuarioExpedienteSidebarDTO
                {
                    idUsuario = et.IdUsuario,
                    nombreCompleto = et.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    tipoMime = et.IdUsuarioNavigation.ImagenTipoMime,
                    idGenero = et.IdGenero,
                    edad = (DateTime.Today.Year - et.FechaNacimiento.Year).ToString(),
                    colonia = et.IdUsuarioNavigation.Colonia,
                    ciudad = et.IdUsuarioNavigation.Ciudad,
                    estado = et.IdUsuarioNavigation.IdEstadoNavigation.Nombre,
                },
                Padecimientos = et.ExpedientePadecimiento
                    .Where(ep => ep.IdExpediente == et.IdExpediente)
                    .Select(ep => new ExpedienteSidebarDTO
                    {
                        IdPadecimiento = ep.IdPadecimiento,
                        FechaDiagnostico = ep.FechaDiagnostico,
                        Nombre = ep.IdPadecimientoNavigation.Nombre
                    })
                    .ToList()
            })
            .FirstOrDefault();



        expedienteSidebarDTO.Usuario.padecimientos = expedienteSidebarDTO.Padecimientos;

        return expedienteSidebarDTO.Usuario;
    }


}
