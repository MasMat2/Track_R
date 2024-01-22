using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.DTOs.Dashboard;
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

    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid(List<int> idDoctorList)
    {
        return context.ExpedientePadecimiento
            .Include(ep => ep.IdExpedienteNavigation)
            .Include(et => et.IdExpedienteNavigation.ExpedientePadecimiento)
            .ThenInclude(ep => ep.IdPadecimientoNavigation)
                .Where(ep => ep.IdExpedienteNavigation.IdUsuarioNavigation.UsuarioRol.Any( ur => ur.IdRolNavigation.Clave == GeneralConstant.ClaveRolPaciente) &&
                       idDoctorList.Contains(ep.IdUsuarioDoctor))
                .GroupBy(ep => ep.IdExpedienteNavigation.IdUsuario)
                .Select(group => new UsuarioExpedienteGridDTO
            {
                IdExpedienteTrackr = group.Key,
                IdUsuario = group.Key,
                DoctorAsociado = group.FirstOrDefault().IdUsuarioDoctorNavigation.IdTituloAcademicoNavigation.Nombre + " " + group.FirstOrDefault().IdUsuarioDoctorNavigation.ApellidoPaterno,
                NombreCompleto = group.FirstOrDefault().IdExpedienteNavigation.IdUsuarioNavigation.ObtenerNombreCompleto(),
                Patologias = group.FirstOrDefault().IdExpedienteNavigation.ExpedientePadecimiento.ObtenerPadecimientos(),
                Edad = (DateTime.Today.Year - group.FirstOrDefault().IdExpedienteNavigation.FechaNacimiento.Year).ToString() + " años",
                TipoMime = group.FirstOrDefault().IdExpedienteNavigation.IdUsuarioNavigation.ImagenTipoMime
            })
            .ToList();
    }

    public int VariablesFueraRango(int idUsuario)
    {
        var currentDateUtc = DateTime.UtcNow.Date;
        return context.EntidadEstructuraTablaValor
            .Where(eetv => eetv.IdTabla == idUsuario && eetv.FueraDeRango == true  && eetv.FechaMuestra.Value.Date == currentDateUtc )
            .Count();
    }

    public int DosisNoTomadas(int idExpediente)
    {
        var expediente =  context.TratamientoToma
            .Join(
            context.TratamientoRecordatorio,
            tt => tt.IdTratamientoRecordatorio,
            tr => tr.IdTratamientoRecordatorio,
            (tt, tr) => new {TratamientoToma = tt ,  TratamientoRecordatorio = tr}
            )
            .Join(
                context.ExpedienteTratamiento,
                temp => temp.TratamientoRecordatorio.IdExpedienteTratamiento,
                et => et.IdExpedienteTratamiento,
                (temp , et) => new { temp.TratamientoToma , ExpedienteTratamiento = et}
            )
            .Where( x => x.ExpedienteTratamiento.IdExpediente == idExpediente  && x.TratamientoToma.FechaToma == null );

            return expediente.Count();
    }

   
  public IEnumerable<ApegoTomaMedicamentoDto> ApegoMedicamentoUsuarios(List<int> idDoctor)
    {
        DateTime fechaInicioSemanaPasada = DateTime.Today.AddDays(-7);

        return context.TratamientoToma
            .Include(tt => tt.IdTratamientoRecordatorioNavigation)
                .ThenInclude(tr => tr.IdExpedienteTratamientoNavigation)
            .Where(tt => idDoctor.Contains(tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdUsuarioDoctor)
                && tt.FechaEnvio >= fechaInicioSemanaPasada)
            .GroupBy(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimiento)
            .Select(group => new ApegoTomaMedicamentoDto
            {
                PadecimientoNombre = group.Select( item => item.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimientoNavigation.Nombre ).First(),
                Apego = (decimal) group.Count(tt => tt.FechaToma != null) / group.Count() * 100
            })
            .ToList();
    }
  public IEnumerable<ApegoTomaMedicamentoDto> ApegoTratamientoPorPaciente(int idUsuario)
    {
        DateTime fechaInicioSemanaPasada = DateTime.Today.AddDays(-7);

        return context.TratamientoToma
            .Include(tt => tt.IdTratamientoRecordatorioNavigation)
                .ThenInclude(tr => tr.IdExpedienteTratamientoNavigation)
            .Where(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdExpedienteNavigation.IdUsuario == idUsuario
                && tt.FechaEnvio >= fechaInicioSemanaPasada)
            .GroupBy(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimiento)
            .Select(group => new ApegoTomaMedicamentoDto
            {
                PadecimientoNombre = group.Select( item => item.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimientoNavigation.Nombre ).First(),
                Apego = (decimal) group.Count(tt => tt.FechaToma != null) / group.Count() * 100
            })
            .ToList();
    }

    public IEnumerable<RecordatorioUsuarioDto> RecordatoriosPorUsuario(int idUsuario)
    {
        var recordatorios = context.TratamientoRecordatorio
        .Where( tr => tr.IdExpedienteTratamientoNavigation.IdExpedienteNavigation.IdUsuarioNavigation.IdUsuario == idUsuario  && tr.Activo == true)
        .Select( tr => new RecordatorioUsuarioDto{
            Padecimiento = tr.IdExpedienteTratamientoNavigation.IdPadecimientoNavigation.IdEntidadEstructura,
            Indicaciones = tr.IdExpedienteTratamientoNavigation.Indicaciones,
            Dia = tr.Dia,
            Tomas = tr.TratamientoToma.Select( tt => new TomaDto {
                FechaToma = tt.FechaToma,
                FechaEnvio = tt.FechaEnvio,
                IdTomaTratamiento = tt.IdTomaTratamiento
            }).ToList()
        })
        .ToList(); 
        
        return recordatorios;
    }

    public UsuarioExpedienteSidebarDTO ConsultarParaSidebar(int idUsuario)
    {
        var expedienteSidebarDTO = context.ExpedienteTrackr
            .Where(et => et.IdUsuario == idUsuario)
            .Select(et => new
            {
                Usuario = new UsuarioExpedienteSidebarDTO
                {
                    IdUsuario = et.IdUsuario,
                    NombreCompleto = et.IdUsuarioNavigation.ObtenerNombreCompleto(),
                    TipoMime = et.IdUsuarioNavigation.ImagenTipoMime,
                    Genero = et.IdGeneroNavigation.Descripcion ,
                    Edad = (DateTime.Today.Year - et.FechaNacimiento.Year).ToString(),
                    Colonia = et.IdUsuarioNavigation.Colonia,
                    Ciudad = et.IdUsuarioNavigation.Ciudad,
                    Estado = et.IdUsuarioNavigation.IdEstadoNavigation.Nombre,
                },
                Padecimientos = et.ExpedientePadecimiento
                    .Select(ep => new ExpedienteSidebarDTO
                    {
                        IdPadecimiento = ep.IdPadecimiento,
                        FechaDiagnostico = ep.FechaDiagnostico,
                        Nombre = ep.IdPadecimientoNavigation.Nombre
                    })
                    .ToList()
            })
            .FirstOrDefault();

        expedienteSidebarDTO.Usuario.Padecimientos = expedienteSidebarDTO.Padecimientos;

        return expedienteSidebarDTO.Usuario;
    }

    public IEnumerable<ExpedienteTrackr> ConsultarExpedientes()
    {
        return context.ExpedienteTrackr;
    }



}
