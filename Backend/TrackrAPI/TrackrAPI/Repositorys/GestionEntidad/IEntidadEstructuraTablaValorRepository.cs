using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Dtos.GestionExpediente;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface IEntidadEstructuraTablaValorRepository : IRepository<EntidadEstructuraTablaValor>
    {
        IEnumerable<EntidadEstructuraTablaValor> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla);
        public EntidadEstructuraTablaValor ConsultarPorId(int id);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorNumeroRegistro(int idEntidadEstructura, int idTabla, int numeroRegistro);
        int ConsultarUltimoRegistro(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarValoresPorCampos(int idExpediente, List<int> idsSeccionVariable, bool? fueraRango);
        public IEnumerable<ValoresHistogramaDTO> ConsultarValoresPorClaveCampo(int idSeccionVariable, int idUsuario, DateTime fecha);
        public IEnumerable<ExpedienteMuestrasGridDTO> ConsultarGridMuestras(int idUsuario);
        public string ConsultarUltimoValor(int idUsuario  , int idSeccionVariable);
        public bool ExisteValorEnEntidadEstructura(int idUsuario, int idSeccionVariable);
    }
}
