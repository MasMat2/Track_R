using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class ConfiguracionConceptoValidatorService
    {
        private IConfiguracionConceptoRepository configuracionConceptoRepository;

        public ConfiguracionConceptoValidatorService(
            IConfiguracionConceptoRepository configuracionConceptoRepository
        )
        {
            this.configuracionConceptoRepository = configuracionConceptoRepository;
        }

        private readonly string MensajeConceptoRequerido = "El concepto es requerido";
        private readonly string MensajeTipoConceptoRequerido = "El tipo de concepto es requerido";

        private readonly string MensajeExistencia = "La configuracionConcepto no existe";

        public void ValidarAgregar(ConfiguracionConcepto configuracionConcepto)
        {
            ValidarRequerido(configuracionConcepto);
            ValidarDuplicado(configuracionConcepto);
        }

        public void ValidarEditar(ConfiguracionConcepto configuracionConcepto)
        {
            ValidarRequerido(configuracionConcepto);
            ValidarExistencia(configuracionConcepto.IdConfiguracionConcepto);
            ValidarDuplicado(configuracionConcepto);
        }

        public void ValidarEliminar(int idConfiguracionConcepto)
        {
            ValidarExistencia(idConfiguracionConcepto);
        }

        public void ValidarRequerido(ConfiguracionConcepto configuracionConcepto)
        {
            Validator.ValidarRequerido(configuracionConcepto.IdConcepto, MensajeConceptoRequerido);
            Validator.ValidarRequerido(configuracionConcepto.IdTipoConcepto, MensajeTipoConceptoRequerido);
        }

        public void ValidarExistencia(int idConfiguracionConcepto)
        {
            var configuracionConcepto = configuracionConceptoRepository.Consultar(idConfiguracionConcepto);

            if (configuracionConcepto == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(ConfiguracionConcepto configuracionConcepto)
        {
            List<ConfiguracionConcepto> configuracionesConcepto = configuracionConceptoRepository.ConsultarPorConcepto(configuracionConcepto.IdConcepto).ToList();

            if (configuracionesConcepto.Any(cc => cc.IdTipoConcepto == configuracionConcepto.IdTipoConcepto))
            {
                string concepto = configuracionConcepto.IdConceptoNavigation.Nombre;
                string tipoConcepto = configuracionConcepto.IdTipoConceptoNavigation.Nombre;

                throw new CdisException($"El concepto { concepto } ya cuenta con el tipo de concepto { tipoConcepto }");
            }
        }
    }
}
