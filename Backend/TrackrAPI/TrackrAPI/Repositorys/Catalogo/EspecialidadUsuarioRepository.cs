using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class EspecialidadUsuarioRepository : Repository<EspecialidadUsuario>, IEspecialidadUsuarioRepository
    {
        public EspecialidadUsuarioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public async Task<EspecialidadUsuario>? ConsultarPorUsuario(int idUsuario, int idEspecialidad)
        {
            return await context.EspecialidadUsuario
                .Where(es => es.IdUsuario == idUsuario && es.IdEspecialidad == idEspecialidad)
                .FirstOrDefaultAsync();  
        }

        public async Task AgregarAsync(EspecialidadUsuario especialidadUsuario)
        {
            context.EspecialidadUsuario.Add(especialidadUsuario);
            await context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int idEspecialidadUsuario)
        {
            var especialidadUsuario = await context.EspecialidadUsuario.FindAsync(idEspecialidadUsuario);

            if(especialidadUsuario == null)
            {
                throw new Exception("No se encontro la especialidad del usuario");
            }

            context.EspecialidadUsuario.Remove(especialidadUsuario);
            await context.SaveChangesAsync();
        }

        public async Task EditarAsync(EspecialidadUsuario especialidadUsuario)
        {
            context.Update(especialidadUsuario);
            await context.SaveChangesAsync();
        }
    }
}