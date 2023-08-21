using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class EspecialidadRepository : Repository<Especialidad>, IEspecialidadRepository
    {
        public EspecialidadRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public Especialidad? Consultar(int idEspecialidad)
        {
            return context.Especialidad
                .Where(es => es.IdEspecialidad == idEspecialidad)
                .FirstOrDefault();
        }

        public IEnumerable<Especialidad> Consultar()
        {
            return context.Especialidad
                .Include(es => es.IdEspecialidad);
        }

        public Especialidad? ConsultarPorNombre(string nombre)
        {
            return context.Especialidad
                .Where(es => es.Nombre.ToLower() == nombre.ToLower())
                .FirstOrDefault();
        }
    }
}