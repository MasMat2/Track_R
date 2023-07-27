using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class MonedaService
    {
        private IMonedaRepository monedaRepository;
        private MonedaValidatorService monedaValidatorService;


        public MonedaService(IMonedaRepository monedaRepository, MonedaValidatorService monedaValidatorService)
        {
            this.monedaRepository = monedaRepository;
            this.monedaValidatorService = monedaValidatorService;
        }

        public Moneda ConsultarPorId(int idMoneda)
        {
            var moneda = monedaRepository.ConsultarPorId(idMoneda);
            return moneda;
        }
        public MonedaDto ConsultarDto(int idMoneda)
        {
            var moneda = monedaRepository.ConsultarDto(idMoneda);
            return moneda;
        }

        public IEnumerable<MonedaGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return monedaRepository.ConsultarTodosParaGrid(idCompania);
        }

        public IEnumerable<MonedaSelectorDto> ConsultarParaSelector()
        {
            return monedaRepository.ConsultarParaSelector();
        }

        public int Agregar(Moneda moneda)
        {
            monedaValidatorService.ValidarAgregar(moneda);
            monedaRepository.Agregar(moneda);
            return moneda.IdMoneda;
        }

        public void Editar(Moneda moneda)
        {
            monedaValidatorService.ValidarEditar(moneda);
            monedaRepository.Editar(moneda);
        }

        public void Eliminar(int idMoneda)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                var moneda = monedaRepository.ConsultarPorId(idMoneda);
                monedaValidatorService.ValidarEliminar(idMoneda);
                monedaRepository.Eliminar(moneda);
                scope.Complete();
            }

        }
    }
}
