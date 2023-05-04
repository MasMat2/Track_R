using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Contabilidad
{
    public interface IPolizaRepository : IRepository<Poliza>
    {
        public Poliza ConsultarPorIdentificador(string identificador, int idCompania);
        public Poliza ConsultarUltimaPoliza(int idCompania);
        public Poliza Consultar(int idPoliza);
        public IEnumerable<PolizaGridDto> ConsultarTodosParaGrid(int idCompania);
        public PolizaDto ConsultarDto(int idPoliza);
        public bool TienePolizasGeneradas(int idCompania);
        public Poliza ConsultarPorNumero(string numero);
        public IEnumerable<PolizaGridDto> ConsultarFiltroParaGrid(PolizaFiltroDto filtro);
        public Poliza ConsultarPorDocumentoOrigen(int idDocumentoOrigen, string origen);
    }
}
