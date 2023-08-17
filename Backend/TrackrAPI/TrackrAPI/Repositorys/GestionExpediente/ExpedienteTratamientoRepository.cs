using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;

using System;

namespace TrackrAPI.Repositorys.GestionExpediente;
public class ExpedienteTratamientoRepository: Repository<ExpedienteTratamiento>, IExpedienteTratamientoRepository
{
    public ExpedienteTratamientoRepository(TrackrContext context): base(context)
    {
        base.context = context;
    }

    public ExpedienteTratamiento? Consultar(int IdExpedienteTratamiento)
    {
        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteTratamiento == IdExpedienteTratamiento)
            .FirstOrDefault();
    }

    public IEnumerable<ExpedienteTratamiento> ConsultarParaGrid(int idUsuario)
    {
        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario);
    }

    public IEnumerable<ExpedienteTratamiento> ConsultarPorUsuario(int idUsuario)
    {

        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario)
            .Include(et => et.IdPadecimientoNavigation)
            .Include(et => et.TratamientoRecordatorio)
            .ThenInclude(tr => tr.TratamientoToma)
            .ToList();
    }

    public IEnumerable<ExpedienteTratamiento> ConsultarTratamientos(int idUsuario)
    {

        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario)
            .Include(et => et.IdPadecimientoNavigation)
            .Include(et => et.TratamientoRecordatorio)
            .ThenInclude(tr => tr.TratamientoToma)
            .ToList();
    }


    public IEnumerable<ExpedienteSelectorDto> SelectorDePadecimiento(int idUsuario)
    {
        return context.ExpedienteTrackr
        .Where(et => et.IdUsuario == idUsuario)
        .SelectMany(et => et.ExpedientePadecimiento)
        .Select(ep => new ExpedienteSelectorDto
        {
            Id = ep.IdPadecimientoNavigation.IdEntidadEstructura,
            Nombre = ep.IdPadecimientoNavigation.Nombre
        }).ToList();

    }


    public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor(){
        return context.Usuario
                .Where(u => u.UsuarioRol.Any(ur => ur.IdRol == 2))
                .Select(u => new ExpedienteSelectorDto
                {
                    Id = u.IdUsuario,
                    Nombre = u.Nombre
                })
                .ToList();
    }


    public int Agregar(ExpedienteTratamiento expedienteTratamiento)
    {
        var entry = context.ExpedienteTratamiento.Add(expedienteTratamiento);
        context.SaveChanges();
        return entry.Entity.IdExpedienteTratamiento;
    }


    public void AgregarRecordatorios(IEnumerable<TratamientoRecordatorio> recordatorios){
        context.TratamientoRecordatorio.AddRange(recordatorios);
        context.SaveChanges();
    }


}





