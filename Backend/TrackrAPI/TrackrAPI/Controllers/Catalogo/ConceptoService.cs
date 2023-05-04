using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class ConceptoService
    {
        private IConceptoRepository conceptoRepository;
        private ConceptoValidatorService conceptoValidatorService;
        private ConfiguracionConceptoService configuracionConceptoService;

        public ConceptoService(
            IConceptoRepository conceptoRepository,
            ConceptoValidatorService conceptoValidatorService,
            ConfiguracionConceptoService configuracionConceptoService
        )
        {
            this.conceptoRepository = conceptoRepository;
            this.conceptoValidatorService = conceptoValidatorService;
            this.configuracionConceptoService = configuracionConceptoService;
        }

        public IEnumerable<ConceptoGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return conceptoRepository.ConsultarTodosParaGrid(idCompania);
        }

        public IEnumerable<ConceptoGridDto> ConsultarTodosParaSelector(int idCompania)
        {
            return conceptoRepository.ConsultarTodosParaSelector(idCompania);
        }

        public IEnumerable<ConceptoSelectorDto> ConsultarSelectorParaPresentacion(int idCompania)
        {
            return conceptoRepository.ConsultarSelectorParaPresentacion(idCompania);
        }

        public IEnumerable<ConceptoGridDto> ConsultarParaDesgloseSelector(int idCompania)
        {
            return conceptoRepository.ConsultarParaDesgloseSelector(idCompania);
        }

        public IEnumerable<ConceptoGridDto> ConsultarOperativosParaSelector(int idCompania)
        {
            return conceptoRepository.ConsultarOperativosParaSelector(idCompania);
        }
        public IEnumerable<Concepto> ConsultarPorTipo(string claveTipoConcepto, int idCompania)
        {
            return conceptoRepository.ConsultarPorTipo(claveTipoConcepto, idCompania);
        }

        public ConceptoDto ConsultarDto(int idConcepto)
        {
            var concepto = conceptoRepository.ConsultarDto(idConcepto);
            conceptoValidatorService.ValidarExistencia(idConcepto);
            return concepto;
        }

        public Concepto Consultar(int idConcepto)
        {
            return conceptoRepository.Consultar(idConcepto);
        }

        public void Agregar(ConceptoFormularioDto conceptoDto)
        {
            Concepto concepto = MapearConcepto(conceptoDto);

            using TransactionScope ts = new();

            conceptoValidatorService.ValidarAgregar(conceptoDto);
            conceptoRepository.Agregar(concepto);

            var tipoConceptos = conceptoDto.IdsTipoConcepto
                .Select(idTipoConcepto => new ConfiguracionConcepto
                {
                    IdConcepto = concepto.IdConcepto,
                    IdTipoConcepto = idTipoConcepto
                })
                .ToList();

            configuracionConceptoService.Guardar(tipoConceptos);

            ts.Complete();
        }

        public void Editar(ConceptoFormularioDto conceptoDto)
        {
            Concepto concepto = MapearConcepto(conceptoDto);

            using TransactionScope ts = new();

            conceptoValidatorService.ValidarEditar(conceptoDto);
            conceptoRepository.Editar(concepto);

            var tipoConceptos = conceptoDto.IdsTipoConcepto
                .Select(idTipoConcepto => new ConfiguracionConcepto
                {
                    IdConcepto = concepto.IdConcepto,
                    IdTipoConcepto = idTipoConcepto
                })
                .ToList();

            configuracionConceptoService.Guardar(tipoConceptos);

            ts.Complete();
        }

        public void Eliminar(int idConcepto)
        {
            Concepto concepto = conceptoRepository.Consultar(idConcepto);
            var configuracionesConcepto = configuracionConceptoService.ConsultarPorConcepto(idConcepto);

            using TransactionScope ts = new();

            foreach (ConfiguracionConcepto configuracion in configuracionesConcepto)
            {
                configuracionConceptoService.Eliminar(configuracion.IdConfiguracionConcepto);
            }

            conceptoValidatorService.ValidarEliminar(idConcepto);
            conceptoRepository.Eliminar(concepto);

            ts.Complete();
        }

        private Concepto MapearConcepto(ConceptoFormularioDto conceptoDto)
        {
            return new Concepto()
            {
                IdConcepto = conceptoDto.IdConcepto,
                Nombre = conceptoDto.Nombre,
                IdCompania = conceptoDto.IdCompania,
                Clave = conceptoDto.Clave,
                TipoMovimiento = conceptoDto.TipoMovimiento,
                IdCuentaContable = conceptoDto.IdCuentaContable,
                Operativo = conceptoDto.Operativo,
                IdSatProductoServicio = conceptoDto.IdSatProductoServicio,
                IdSatUnidad = conceptoDto.IdSatUnidad,
                IdTipoAuxiliar = conceptoDto.IdTipoAuxiliar
            };
        }
    }
}
