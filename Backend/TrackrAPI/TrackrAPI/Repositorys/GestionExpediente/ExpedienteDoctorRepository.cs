using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;
using TrackrAPI.Helpers;
using TrackrAPI.Models;


namespace TrackrAPI.Repositorys.GestionExpediente;
public class ExpedienteDoctorRepository : Repository<ExpedienteDoctor>, IExpedienteDoctorRepository
{

    public ExpedienteDoctorRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public IEnumerable<ExpedienteDoctor> ConsultarExpediente(int idExpediente)
    {
        return context.ExpedienteDoctor
        .Include(ed => ed.IdUsuarioDoctorNavigation)
        .Include(ed => ed.IdUsuarioDoctorNavigation.IdCompaniaNavigation)
        .Include(ed => ed.IdUsuarioDoctorNavigation.IdTituloAcademicoNavigation)
        .Where(ed => ed.IdExpediente == idExpediente);
    }
    public ExpedienteDoctor ConsultarExpedientePorDoctor(int idExpediente , int idUsuarioDoctor){
        return context.ExpedienteDoctor
        .Include(ed => ed.IdUsuarioDoctorNavigation)
        .Include(ed => ed.IdUsuarioDoctorNavigation.IdCompaniaNavigation)
        .Where(ed => ed.IdExpediente == idExpediente && ed.IdUsuarioDoctor == idUsuarioDoctor)
        .FirstOrDefault();
    }
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarDoctores(int idCompania)
    {
        return context.Usuario
            .Join(
                context.UsuarioRol,
                usuario => usuario.IdUsuario,
                usuarioRol => usuarioRol.IdUsuario,
                (usuario, usuarioRol) => new { Usuario = usuario, UsuarioRol = usuarioRol })
            .Where(ti => ti.UsuarioRol.IdRolNavigation.Clave == GeneralConstant.ClaveTipoUsuarioMedicoExterno && ti.Usuario.IdCompania == idCompania)
            .Select(ti => new ExpedienteDoctorSelectorDTO
            {
                IdUsuarioDoctor = ti.Usuario.IdUsuario,
                Ambito = ti.Usuario.IdCompaniaNavigation.Nombre,
                Nombre = ti.Usuario.Nombre + " " + ti.Usuario.ApellidoPaterno + " " + ti.Usuario.ApellidoMaterno,
                IdRol = ti.UsuarioRol.IdRol
            });
    }
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarDoctores()
    {
        return context.Usuario
            .Join(
                context.UsuarioRol,
                usuario => usuario.IdUsuario,
                usuarioRol => usuarioRol.IdUsuario,
                (usuario, usuarioRol) => new { Usuario = usuario, UsuarioRol = usuarioRol })
            .Where(ti => ti.UsuarioRol.IdRolNavigation.Clave == GeneralConstant.ClaveTipoUsuarioMedicoExterno)
            .Select(ti => new ExpedienteDoctorSelectorDTO
            {
                IdUsuarioDoctor = ti.Usuario.IdUsuario,
                Ambito = ti.Usuario.IdCompaniaNavigation.Nombre,
                Nombre = ti.Usuario.Nombre + " " + ti.Usuario.ApellidoPaterno + " " + ti.Usuario.ApellidoMaterno,
                IdRol = ti.UsuarioRol.IdRol
            });
    }
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarSelector(int idExpediente , int idCompania)
    {
        var doctoresCompletos = ConsultarDoctores(idCompania).ToList();

        var doctoresExpediente = ConsultarExpediente(idExpediente).ToList();

        var doctoresFaltantes = doctoresCompletos.Where(dc => !doctoresExpediente.Any(de => de.IdUsuarioDoctor == dc.IdUsuarioDoctor)).ToList();

        return doctoresFaltantes;
    }

    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarPorUsuarioParaSelector(int idExpediente)
    {
        var doctoresExpediente = ConsultarExpediente(idExpediente).ToList();

        return doctoresExpediente.Select(d => new ExpedienteDoctorSelectorDTO
        {
            IdUsuarioDoctor = d.IdUsuarioDoctor,
            Nombre = d.IdUsuarioDoctorNavigation.Nombre + ' ' + d.IdUsuarioDoctorNavigation.ApellidoPaterno + ' ' + d.IdUsuarioDoctorNavigation.ApellidoMaterno,
            Ambito = d.IdUsuarioDoctorNavigation.IdCompaniaNavigation.Nombre,
        });
    }

    public IEnumerable<ExpedientePadecimientoDTO> ConsultarPacientesPorPadecimiento(List<int> idDoctores){
        return context.ExpedienteDoctor
        .Where(ed => ed.IdExpedienteNavigation.IdUsuarioNavigation.IdTipoUsuarioNavigation.Clave == GeneralConstant.ClaveTipoUsuarioPaciente)
        .Where( ed => idDoctores.Contains(ed.IdUsuarioDoctor))
        .SelectMany(
            ed => ed.IdExpedienteNavigation.ExpedientePadecimiento.Select(
                ep => new ExpedientePadecimientoDTO{
                    IdExpedientePadecimiento = ep.IdExpedientePadecimiento,
                    IdPadecimiento  = ep.IdPadecimiento,
                    FechaDiagnostico = ep.FechaDiagnostico,
                    NombrePadecimiento = ep.IdPadecimientoNavigation.Nombre ?? string.Empty 
                }
            )   
        ).ToList();
    }
}