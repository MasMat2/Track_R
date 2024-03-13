using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class ConfiguracionConceptoService
    {
        private IConfiguracionConceptoRepository configuracionConceptoRepository;
        private ConfiguracionConceptoValidatorService configuracionConceptoValidatorService;

        public ConfiguracionConceptoService(
            IConfiguracionConceptoRepository configuracionConceptoRepository,
            ConfiguracionConceptoValidatorService configuracionConceptoValidatorService
        )
        {
            this.configuracionConceptoRepository = configuracionConceptoRepository;
            this.configuracionConceptoValidatorService = configuracionConceptoValidatorService;
        }

        public IEnumerable<ConfiguracionConcepto> ConsultarTodos()
        {
            return configuracionConceptoRepository.ConsultarTodos();
        }

        public IEnumerable<ConfiguracionConcepto> ConsultarPorConcepto(int idConcepto)
        {
            return configuracionConceptoRepository.ConsultarPorConcepto(idConcepto);
        }

        public ConfiguracionConcepto Consultar(int idConfiguracionConcepto)
        {
            return configuracionConceptoRepository.Consultar(idConfiguracionConcepto);
        }

        public int Agregar(ConfiguracionConcepto configuracionConcepto)
        {
            configuracionConceptoValidatorService.ValidarAgregar(configuracionConcepto);
            configuracionConceptoRepository.Agregar(configuracionConcepto);
            return configuracionConcepto.IdConfiguracionConcepto;
        }

        public void Editar(ConfiguracionConcepto configuracionConcepto)
        {
            configuracionConceptoValidatorService.ValidarEditar(configuracionConcepto);
            configuracionConceptoRepository.Editar(configuracionConcepto);
        }

        public void Eliminar(int idConfiguracionConcepto)
        {
            ConfiguracionConcepto configuracionConcepto = configuracionConceptoRepository.Consultar(idConfiguracionConcepto);

            configuracionConceptoValidatorService.ValidarEliminar(idConfiguracionConcepto);
            configuracionConceptoRepository.Eliminar(configuracionConcepto);
        }

        public void Guardar(List<ConfiguracionConcepto> nuevaConfiguracion)
        {
            using TransactionScope ts = new();

            int idConcepto = nuevaConfiguracion[0].IdConcepto;
            var configuracionActual = configuracionConceptoRepository.ConsultarPorConcepto(idConcepto);

            var eliminados = configuracionActual.Except(nuevaConfiguracion);
            foreach (ConfiguracionConcepto configuracion in eliminados)
            {
                Eliminar(configuracion.IdConfiguracionConcepto);
            }

            var nuevos = nuevaConfiguracion.Except(configuracionActual);
            foreach (ConfiguracionConcepto configuracion in nuevos)
            {
                Agregar(configuracion);
            }

            ts.Complete();
        }
    }
}
