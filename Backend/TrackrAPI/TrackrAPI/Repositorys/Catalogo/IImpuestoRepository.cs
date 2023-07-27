using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using TrackrAPI.Models;
using TrackrAPI.Dtos.Catalogo;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IImpuestoRepository : IRepository<Impuesto>
    {
        public IEnumerable<ImpuestoGridDto> ConsultarTodosParaGrid(int idCompania);
        public IEnumerable<ImpuestoDto> ConsultarTodosParaSelector(int idCompania);
        public Impuesto Consultar(int idImpuesto);
        public ImpuestoDto ConsultarDto(int idImpuesto);
        public ImpuestoDto ConsultarDto(string clave, int idCompania);
        public Impuesto ConsultarPorClave(string clave, int idCompania);
        public ImpuestoDto ConsultarPorNombre(int idCompania, string nombre);
        public Impuesto ConsultarDependencias(int idImpuesto);
    }
}
