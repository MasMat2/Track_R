using Microsoft.EntityFrameworkCore;
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


}