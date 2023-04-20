using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Helpers;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class UsuarioRepository : Repository<Usuario> , IUsuarioRepository
    {
        public UsuarioRepository(TrackrContext context) : base(context) 
        {
            base.context = context;
        }

        public Usuario Login(string username, string pasword, string clave)
        {
            return context.Usuario
                .Include(u => u.IdTipoUsuarioNavigation)
                .Where(u => 
                    u.Correo == username && 
                    u.Contrasena == pasword &&
                    u.IdTipoUsuarioNavigation.Clave == clave)
                .FirstOrDefault();
        }
    }
}
