using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionExpediente.ExpedienteDoctor;
using TrackrAPI.Models;


namespace TrackrAPI.Repositorys.GestionExpediente;
public class ExpedienteDoctorRepository : Repository<ExpedienteDoctor>, IExpedienteDoctorRepository
{

    public ExpedienteDoctorRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public IEnumerable<ExpedienteDoctor> Consultar(int idExpediente)
    {
        return context.ExpedienteDoctor
        .Include(ed => ed.IdUsuarioDoctorNavigation)
        .Include(ed => ed.IdUsuarioDoctorNavigation.IdCompaniaNavigation)
        .Where(ed => ed.IdExpediente == idExpediente);
    }
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarDoctores()
    {
        return context.Usuario
            .Join(
                context.UsuarioRol,
                usuario => usuario.IdUsuario,
                usuarioRol => usuarioRol.IdUsuario,
                (usuario, usuarioRol) => new { Usuario = usuario, UsuarioRol = usuarioRol })
            .Where(ti => ti.UsuarioRol.IdRol == 2)
            .Select(ti => new ExpedienteDoctorSelectorDTO
            {
                IdUsuarioDoctor = ti.Usuario.IdUsuario,
                Ambito = ti.Usuario.IdCompaniaNavigation.Nombre,
                Nombre = ti.Usuario.Nombre + " " + ti.Usuario.ApellidoPaterno + " " + ti.Usuario.ApellidoMaterno,
                IdRol = ti.UsuarioRol.IdRol
            })
            .ToList();
    }
    public IEnumerable<ExpedienteDoctorSelectorDTO> ConsultarSelector(int idExpediente)
    {
        var doctoresCompletos = ConsultarDoctores();

        var doctoresExpediente = Consultar(idExpediente).ToList();

        var doctoresFaltantes = doctoresCompletos.Where(dc => !doctoresExpediente.Any(de => de.IdUsuarioDoctor == dc.IdUsuarioDoctor)).ToList();

        return doctoresFaltantes;
    }


}