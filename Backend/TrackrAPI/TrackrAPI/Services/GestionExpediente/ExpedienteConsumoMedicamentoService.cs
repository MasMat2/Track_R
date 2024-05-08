using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Repositorys.GestionExpediente;
using System.Transactions;

namespace TrackrAPI.Services.GestionExpediente
{
    public class ExpedienteConsumoMedicamentoService
    {
        private readonly IExpedienteConsumoMedicamentoRepository expedienteConsumoMedicamentoRepository;

        public ExpedienteConsumoMedicamentoService(
            IExpedienteConsumoMedicamentoRepository expedienteConsumoMedicamentoRepository)
        {
            this.expedienteConsumoMedicamentoRepository = expedienteConsumoMedicamentoRepository;
        }

        public IEnumerable<ExpedienteConsumoMedicamentoGridDto> ConsultarParaGrid(int idUsuario)
        {
            var expedienteConsumoMedicamento = expedienteConsumoMedicamentoRepository.ConsultarParaGrid(idUsuario);

            var ExpedienteConsumoMedicamentoDto = expedienteConsumoMedicamento.Select(et => new ExpedienteConsumoMedicamentoGridDto
            {
                IdTomaTratamiento = et.IdTomaTratamiento,
                Farmaco = et.Farmaco,
                Cantidad = et.Cantidad,
                Unidad = et.Unidad,
                Indicaciones = et.Indicaciones,
                Padecimiento = et.Padecimiento?? string.Empty,
                FechaEstablecida = et.FechaEstablecida,
                FechaTomada = et.FechaTomada
            });

            return ExpedienteConsumoMedicamentoDto;
        }

        // Consultar Consumo Medicamento
        public IEnumerable<ExpedienteConsumoMedicamentoDto> ConsultarConsumoMedicamento(int idUsuario)
        {
            var expedienteConsumoMedicamento = expedienteConsumoMedicamentoRepository.ConsultarConsumoMedicamento(idUsuario);

            var ExpedienteConsumoMedicamentoDto = expedienteConsumoMedicamento.Select(et => new ExpedienteConsumoMedicamentoDto
            {
                IdTomaTratamiento = et.IdTomaTratamiento,
                Farmaco = et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Farmaco,
                Cantidad = et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Cantidad,
                Unidad = et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Unidad,
                Indicaciones = et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.Indicaciones,
                Padecimiento =  et.IdTratamientoRecordatorioNavigation.IdExpedienteTratamientoNavigation.IdPadecimientoNavigation.IdEntidadEstructuraPadreNavigation.Nombre ?? string.Empty,
                FechaEnvio = et.FechaEnvio,
                FechaToma = et.FechaEnvio,
            });

            return ExpedienteConsumoMedicamentoDto;
        }

    }
}
