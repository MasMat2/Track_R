﻿using Microsoft.EntityFrameworkCore;
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

    public IEnumerable<UsuarioExpedienteGridDTO> ConsultarParaGrid(int idDoctor)
    {
        return context.ExpedienteTrackr
            .Include(et => et.IdUsuarioNavigation)
            .Include(et => et.ExpedientePadecimiento)
            .ThenInclude(ep => ep.IdPadecimientoNavigation)
            .Join(
                context.ExpedienteDoctor,
                et => et.IdExpediente,
                ed => ed.IdExpediente,
                (et, ed) => new
                {
                    Expediente = et,
                    DoctorId = ed.IdUsuarioDoctor
                }
                )
            .Where(joinResult =>
                            joinResult.Expediente.IdUsuarioNavigation.IdTipoUsuarioNavigation.Clave == GeneralConstant.ClaveTipoUsuarioPaciente  &&
                            joinResult.DoctorId == idDoctor )
            .Select(joinResult => new UsuarioExpedienteGridDTO
            {
                IdExpedienteTrackr = joinResult.Expediente.IdExpediente,
                IdUsuario = joinResult.Expediente.IdUsuario,
                NombreCompleto = joinResult.Expediente.IdUsuarioNavigation.ObtenerNombreCompleto(),
                Patologias = joinResult.Expediente.ExpedientePadecimiento.ObtenerPadecimientos(),
                Edad = (DateTime.Today.Year - joinResult.Expediente.FechaNacimiento.Year).ToString() + " años",
                TipoMime = joinResult.Expediente.IdUsuarioNavigation.ImagenTipoMime
            })
            .ToList();
    }

    public int VariablesFueraRango(int idUsuario)
    {
        var currentDateUtc = DateTime.UtcNow;
        var maxDate = currentDateUtc.AddDays(-2);

        return context.EntidadEstructuraTablaValor
            .Where(eetv => eetv.IdTabla == idUsuario && eetv.FueraDeRango == true && eetv.FechaMuestra >= maxDate)
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

   
  public IEnumerable<ApegoTomaMedicamentoDto> ApegoMedicamentoUsuarios(int idDoctor)
    {
        DateTime fechaInicioSemanaPasada = DateTime.Today.AddDays(-7);

        return context.TratamientoToma
            .Include(tt => tt.IdTratamientoRecordatorioNavigation)
                .ThenInclude(tr => tr.IdExpedienteTratamientoNavigation)
            .Where(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdUsuarioDoctor == idDoctor
                && tt.FechaEnvio >= fechaInicioSemanaPasada)
            .GroupBy(tt => tt.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimiento)
            .Select(group => new ApegoTomaMedicamentoDto
            {
                PadecimientoNombre = group.Select( item => item.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimientoNavigation.Nombre ).First(),
                Apego = (decimal) group.Count(tt => tt.FechaToma != null) / group.Count() * 100
            })
            .ToList();
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



}
