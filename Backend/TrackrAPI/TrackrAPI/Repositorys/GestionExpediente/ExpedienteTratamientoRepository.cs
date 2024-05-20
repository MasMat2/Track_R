using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.GestionExpediente;

public class ExpedienteTratamientoRepository: Repository<ExpedienteTratamiento>, IExpedienteTratamientoRepository
{
    public ExpedienteTratamientoRepository(TrackrContext context): base(context)
    {
        base.context = context;
    }

    public IEnumerable<ExpedienteTratamiento> ConsultarParaGrid(int idUsuario)
    {
        return context.ExpedienteTratamiento
            .Include(et => et.IdPadecimientoNavigation)
            .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario);
    }

    public ExpedienteTratamiento ConsultarTratamiento(int idExpedienteTratamiento)
    {
        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteTratamiento == idExpedienteTratamiento)
            .Include(et => et.IdPadecimientoNavigation)
            .Include(et => et.IdUsuarioDoctorNavigation)
            .ThenInclude(et => et.IdTituloAcademicoNavigation)
            .Include(et => et.TratamientoRecordatorio)
            .ThenInclude(tr => tr.TratamientoToma)
            .AsSplitQuery()
            .SingleOrDefault();
    }

    public IEnumerable<ExpedienteTratamiento> ConsultarTratamientos(int idUsuario)
    {
        return context.ExpedienteTratamiento
            .Where(et => et.IdExpedienteNavigation.IdUsuario == idUsuario)
            .Include(et => et.IdPadecimientoNavigation)
            .Include(et => et.IdUsuarioDoctorNavigation)
            .ThenInclude(et => et.IdTituloAcademicoNavigation)
            .Include(et => et.TratamientoRecordatorio)
            .ThenInclude(tr => tr.TratamientoToma)
            .AsSplitQuery()
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

    public IEnumerable<ExpedienteSelectorDto> SelectorDeDoctor()
    {
        return context.Usuario
                .Where(u => u.UsuarioRol.Any(ur => ur.IdRol == 2))
                .Select(u => new ExpedienteSelectorDto
                {
                    Id = u.IdUsuario,
                    Nombre = u.ObtenerNombreCompleto()
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

    public void EliminarRecordatorios(IEnumerable<TratamientoRecordatorio> recordatorios)
    {
        context.TratamientoRecordatorio.RemoveRange(recordatorios);
        context.SaveChanges();
    }

    public IEnumerable<TratamientoToma> ConsultarTratamientoTomas(int idTratamientoRecordatorio)
    {
        return context.TratamientoToma.Where(tt => tt.IdTratamientoRecordatorio == idTratamientoRecordatorio);
    }

    public void EliminarTratamientoTomas(IEnumerable<TratamientoToma> tt)
    {
        context.TratamientoToma.RemoveRange(tt);
        context.SaveChanges();
    }



}
