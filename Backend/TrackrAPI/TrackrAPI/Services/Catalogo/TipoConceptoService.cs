using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class TipoConceptoService
    {
        private ITipoConceptoRepository tipoConceptoRepository;
        private TipoConceptoValidatorService tipoConceptoValidatorService;

        public TipoConceptoService(
            ITipoConceptoRepository tipoConceptoRepository,
            TipoConceptoValidatorService tipoConceptoValidatorService
        )
        {
            this.tipoConceptoRepository = tipoConceptoRepository;
            this.tipoConceptoValidatorService = tipoConceptoValidatorService;
        }

        public IEnumerable<TipoConcepto> ConsultarTodos()
        {
            return tipoConceptoRepository.ConsultarTodos();
        }

        public TipoConcepto Consultar(int idTipoConcepto)
        {
            return tipoConceptoRepository.Consultar(idTipoConcepto);
        }

        public TipoConcepto ConsultarPorClave(string clave)
        {
            return tipoConceptoRepository.ConsultarPorClave(clave);
        }

        public int Agregar(TipoConcepto tipoConcepto)
        {
            tipoConceptoValidatorService.ValidarAgregar(tipoConcepto);
            tipoConceptoRepository.Agregar(tipoConcepto);
            return tipoConcepto.IdTipoConcepto;
        }

        public void Editar(TipoConcepto tipoConcepto)
        {
            tipoConceptoValidatorService.ValidarEditar(tipoConcepto);
            tipoConceptoRepository.Editar(tipoConcepto);
        }

        public void Eliminar(int idTipoConcepto)
        {
            tipoConceptoValidatorService.ValidarEliminar(idTipoConcepto);

            TipoConcepto tipoConcepto = tipoConceptoRepository.Consultar(idTipoConcepto);
            tipoConceptoRepository.Eliminar(tipoConcepto);
        }
    }
}
