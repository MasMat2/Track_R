using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Usuario ConsultarPorCorreo(string correo)
        {
            var usuario =
                from u in context.Usuario
                where u.Correo == correo
                select u;
            return usuario.FirstOrDefault();
        }
    }
}
