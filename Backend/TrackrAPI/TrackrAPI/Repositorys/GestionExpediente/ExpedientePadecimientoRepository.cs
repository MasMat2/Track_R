using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Helpers;
namespace TrackrAPI.Repositorys.GestionExpediente
{
    public class ExpedientePadecimientoRepository : Repository<ExpedientePadecimiento>, IExpedientePadecimientoRepository
    {
        public ExpedientePadecimientoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public IEnumerable<ExpedientePadecimientoDTO> Consultar(int idDoctor)
        {
            return context.ExpedientePadecimiento
            .Where(ep => ep.IdExpedienteNavigation.IdUsuarioNavigation.IdTipoUsuarioNavigation.Clave == GeneralConstant.ClaveTipoUsuarioPaciente && ep.IdUsuarioDoctor == idDoctor)
                .Select(ep => new ExpedientePadecimientoDTO
                {
                    IdExpedientePadecimiento = ep.IdExpedientePadecimiento,
                    IdPadecimiento = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico,
                    NombrePadecimiento = ep.IdPadecimientoNavigation.Nombre ?? string.Empty
                }).ToList();
        }

        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario)
        {
            return context.ExpedientePadecimiento
                .Where(ep => ep.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(ep => new ExpedientePadecimientoDTO
                {
                    IdExpedientePadecimiento = ep.IdExpedientePadecimiento,
                    IdPadecimiento = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico,
                    IdExpediente = ep.IdExpediente,
                    NombrePadecimiento = ep.IdPadecimientoNavigation.Nombre,
                    clavePadecimiento = ep.IdPadecimientoNavigation.Clave,
                }).ToList();
        }

        public IEnumerable<ExpedientePadecimientoGridDTO> ConsultarParaGridPorUsuario(int idUsuario)
        {
            return context.ExpedientePadecimiento
                .Where(ep => ep.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(ep => new ExpedientePadecimientoGridDTO
                {
                    IdExpedientePadecimiento = ep.IdExpedientePadecimiento,
                    IdPadecimiento = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico,
                    NombrePadecimiento = ep.IdPadecimientoNavigation.Nombre,
                    NombreDoctor = ep.IdUsuarioDoctorNavigation.ObtenerNombreCompleto()

                });
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector()
        {
            return context.ExpedientePadecimiento
                .Select(ep => new ExpedientePadecimientoSelectorDTO
                {
                    IdPadecimiento = ep.IdPadecimiento,
                    Nombre = ep.IdPadecimientoNavigation.Nombre
                }).ToList();
        }

        public void EliminarPorExpediente(int idExpediente)
        {
            var padecimientos = context.ExpedientePadecimiento.Where(ep => ep.IdExpediente == idExpediente);
            context.ExpedientePadecimiento.RemoveRange(padecimientos);
        }

        public IEnumerable<PadecimientoFueraRangoDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario)
        {
            return context.ExpedientePadecimiento
                .Where(ep => ep.IdPadecimiento == idPadecimiento
                        && ep.IdExpedienteNavigation.IdUsuario == idUsuario)
                .Select(pfrDTO => new PadecimientoFueraRangoDTO
                {
                    nombrePadecimiento = pfrDTO.IdPadecimientoNavigation.Nombre,
                    nombreSeccion = pfrDTO.IdPadecimientoNavigation.IdSeccionNavigation.Nombre,
                    descripcionSecionCampo = pfrDTO.IdPadecimientoNavigation.IdSeccionNavigation.SeccionCampo.FirstOrDefault().Descripcion,
                    valorReferencia = pfrDTO.IdPadecimientoNavigation.IdSeccionNavigation.SeccionCampo.FirstOrDefault().IdDominioNavigation.ValorMinimo.ToString(),
                    fechaMuestra = pfrDTO.IdPadecimientoNavigation.EntidadEstructuraTablaValor.FirstOrDefault().FechaMuestra.ToString(),
                    valorEntidadEstructuraValor = pfrDTO.IdPadecimientoNavigation.EntidadEstructuraTablaValor.FirstOrDefault().Valor,
                }).ToList();
        }

        public IEnumerable<ExpedientePadecimiento> ConsultarPorPadecimiento(int? idPadecimiento)
        {
            return context.ExpedientePadecimiento
                .Where(exp => exp.IdPadecimiento == idPadecimiento)
                .Include(exp => exp.IdExpedienteNavigation);
        }
    }
}
