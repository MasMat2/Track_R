using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadEstructuraValorService
    {
        private readonly IEntidadEstructuraValorRepository entidadEstructuraValorRepository;

        public EntidadEstructuraValorService(IEntidadEstructuraValorRepository entidadEstructuraValorRepository)
        {
            this.entidadEstructuraValorRepository = entidadEstructuraValorRepository;
        }

        public void Guardar(List<EntidadEstructuraValor> entidadEstructuraValorList)
        {
            var valoresEliminar = entidadEstructuraValorList.Where(campoValor =>
                    (campoValor.IdEntidadEstructuraValor > 0 && string.IsNullOrWhiteSpace(campoValor.Valor))
                || (campoValor.IdEntidadEstructuraValor > 0 && campoValor.Valor == "false"));

            var valoresAgregar = entidadEstructuraValorList.Where(campoValor =>
                !string.IsNullOrWhiteSpace(campoValor.Valor) && campoValor.IdEntidadEstructuraValor == 0);

            var valoresEditar = entidadEstructuraValorList.Where(campoValor =>
                !string.IsNullOrWhiteSpace(campoValor.Valor) && campoValor.IdEntidadEstructuraValor > 0);

            entidadEstructuraValorRepository.Agregar(valoresAgregar);
            entidadEstructuraValorRepository.Editar(valoresEditar);
            entidadEstructuraValorRepository.Eliminar(valoresEliminar);
        }

        public IEnumerable<EntidadEstructuraValorDto> ConsultarPorTabulacion(int idEntidadEstructura, int idTabla)
        {
            var valores = entidadEstructuraValorRepository.ConsultarPorTabulacion(idEntidadEstructura, idTabla);
            return valores;
        }
    }
}
