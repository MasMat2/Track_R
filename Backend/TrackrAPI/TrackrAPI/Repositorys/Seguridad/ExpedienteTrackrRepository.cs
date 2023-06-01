using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class ExpedienteTrackrRepository: Repository<ExpedienteTrackr>, IExpedienteTrackrRepository
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
                .Include(et => et.ExpedientePadecimiento)
                .ThenInclude(ep => ep.IdPadecimientoNavigation)
                .Select(et => new UsuarioExpedienteGridDTO
                {
                    IdExpedienteTrackr = et.IdExpediente,
                    IdUsuario = et.IdUsuario,
                    NombreCompleto = et.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    Patologias = et.ExpedientePadecimiento.ObtenerPadecimientos()

                })
                .ToList();
        }
    }
}
