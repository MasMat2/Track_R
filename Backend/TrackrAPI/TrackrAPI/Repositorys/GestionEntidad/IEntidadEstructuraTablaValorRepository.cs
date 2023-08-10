using TrackrAPI.Models;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionEntidad;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public interface IEntidadEstructuraTablaValorRepository : IRepository<EntidadEstructuraTablaValor>
    {
        IEnumerable<EntidadEstructuraTablaValor> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorPestanaSeccion(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarPorNumeroRegistro(int idEntidadEstructura, int idTabla, int numeroRegistro);
        int ConsultarUltimoRegistro(int idEntidadEstructura, int idTabla);
        public IEnumerable<EntidadEstructuraTablaValor> ConsultarValoresPorCampos(int idExpediente, IEnumerable<string> claveCampos, bool? fueraRango);
        public IEnumerable<ValoresHistogramaDTO> ConsultarValoresPorClaveCampo(string claveCampo, int idUsuario, DateTime fecha);
    }
}
