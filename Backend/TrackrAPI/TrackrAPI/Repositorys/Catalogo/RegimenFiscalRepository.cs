using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class RegimenFiscalRepository : Repository<RegimenFiscal>, IRegimenFiscalRepository
    {
        public RegimenFiscalRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public RegimenFiscal Consultar(int idRegimenFiscal)
        {
            return context.RegimenFiscal
                .Where(rf => rf.IdRegimenFiscal == idRegimenFiscal)
                .FirstOrDefault();
        }

        public RegimenFiscal Consultar(string clave)
        {
            return context.RegimenFiscal
                .Where(rf => rf.Clave == clave)
                .FirstOrDefault();
        }

        public IEnumerable<RegimenFiscalDto> ConsultarTodosParaSelector()
        {
            return context.RegimenFiscal
                .OrderBy(rf => rf.Clave)
                .Select(rf => new RegimenFiscalDto
                {
                    IdRegimenFiscal = rf.IdRegimenFiscal,
                    Clave = rf.Clave,
                    Nombre = rf.Nombre,
                    ClaveNombre = rf.ClaveNombre()
                })
                .ToList();
        }

    }
}
