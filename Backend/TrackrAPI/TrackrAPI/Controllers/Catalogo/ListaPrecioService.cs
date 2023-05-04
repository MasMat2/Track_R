using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class ListaPrecioService
    {
        private IListaPrecioRepository listaPrecioRepository;
        private ListaPrecioValidatorService listaPrecioValidatorService;
        private IListaPrecioClinicaRepository listaPrecioClinicaRepository;
        private IListaPrecioDetalleRepository listaPrecioDetalleRepository;

        public ListaPrecioService(IListaPrecioRepository ListaPrecioRepository,
            ListaPrecioValidatorService listaPrecioValidatorService,
            IListaPrecioClinicaRepository listaPrecioClinicaRepository,
            IListaPrecioDetalleRepository listaPrecioDetalleRepository)
        {
            this.listaPrecioRepository = ListaPrecioRepository;
            this.listaPrecioValidatorService = listaPrecioValidatorService;
            this.listaPrecioClinicaRepository = listaPrecioClinicaRepository;
            this.listaPrecioDetalleRepository = listaPrecioDetalleRepository;
        }

        public IEnumerable<ListaPrecioDto> ConsultarTodosParaSelector()
        {
            return listaPrecioRepository.ConsultarTodosParaSelector();
        }

        public ListaPrecioDto ConsultarDto(int idListaPrecios)
        {
            var listaPrecios = listaPrecioRepository.ConsultarDto(idListaPrecios);
            listaPrecioValidatorService.ValidarExistencia(listaPrecios);
            return listaPrecios;
        }

        public IEnumerable<ListaPrecioDto> ConsultarVigente(int idHospital)
        {
            return listaPrecioRepository.ConsultarVigente(idHospital);
        }

        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaGrid(int idHospital)
        {
            return listaPrecioRepository.ConsultarTodosPorHospitalParaGrid(idHospital); ;
        }

        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaSelector(int idHospital)
        {
            return listaPrecioRepository.ConsultarTodosPorHospitalParaSelector(idHospital); ;
        }

        public int Agregar(ListaPrecio listaPrecio, int idHospital)
        {
            listaPrecioValidatorService.ValidarAgregar(listaPrecio, idHospital);

            listaPrecio.FechaAlta = DateTime.Now;

            return listaPrecioRepository.Agregar(listaPrecio).IdListaPrecio;
        }

        public void Editar(ListaPrecio listaPrecio, int idHospital)
        {
            listaPrecioValidatorService.ValidarEditar(listaPrecio, idHospital);
            listaPrecioRepository.Editar(listaPrecio);
        }

        public void Eliminar(int idListaPrecio)
        {
            listaPrecioValidatorService.ValidarEliminar(idListaPrecio);

            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                ListaPrecio listaPrecio = listaPrecioRepository.Consultar(idListaPrecio);

                var listaPrecioClinicaList = listaPrecioClinicaRepository.ConsultarPorListaPrecio(idListaPrecio);

                foreach (var listaPrecioClinica in listaPrecioClinicaList)
                {
                    ListaPrecioClinica lpc = listaPrecioClinicaRepository.Consultar(listaPrecioClinica.IdListaPrecioClinica);

                    listaPrecioClinicaRepository.Eliminar(lpc);
                }

                var listaPrecioDetalleList = listaPrecioDetalleRepository.ConsultarPorListaPrecio(listaPrecio.IdListaPrecio);

                foreach (var listaPrecioDetalle in listaPrecioDetalleList)
                {
                    listaPrecioDetalleRepository.Eliminar(listaPrecioDetalle);
                }

                listaPrecioRepository.Eliminar(listaPrecio);

                scope.Complete();
            }
        }

        public ListaPrecioDetalle ConsultarPorPresentacion(int idPresentacion)
        {
            return listaPrecioRepository.ConsultarPorPresentacion(idPresentacion);
        }

        public void Copiar(dynamic listaPrecio, int idHospital)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                ListaPrecio listaPrecioOrigen = listaPrecioRepository.ConsultarDependencias((int)listaPrecio.idListaPrecio);

                var listaPrecioDestino = new ListaPrecio()
                {
                    Clave = (string)listaPrecio.clave,
                    FechaAlta = DateTime.Now,
                    Nombre = (string)listaPrecio.nombre,
                    Observaciones = listaPrecioOrigen.Observaciones,
                    FechaInicioVigencia = (DateTime)listaPrecio.fechaInicioVigencia,
                    FechaFinVigencia = (DateTime)listaPrecio.fechaFinVigencia,
                    TodasClinicas = false,
                    IdMoneda = listaPrecio.idMoneda
                };

                listaPrecioValidatorService.ValidarAgregar(listaPrecioDestino, idHospital);
                listaPrecioRepository.Agregar(listaPrecioDestino);

                foreach (var lpc in listaPrecioOrigen.ListaPrecioClinica)
                {
                    var listaPrecioClinica = new ListaPrecioClinica()
                    {
                        IdClinica = lpc.IdClinica,
                        IdListaPrecio = listaPrecioDestino.IdListaPrecio
                    };

                    listaPrecioClinicaRepository.Agregar(listaPrecioClinica);
                }

                foreach (var lpd in listaPrecioOrigen.ListaPrecioDetalle)
                {
                    var listaPrecioDetalle = new ListaPrecioDetalle()
                    {
                        Clave = lpd.Clave,
                        FechaAlta = DateTime.Now,
                        PrecioBase = lpd.PrecioBase,
                        IdImpuesto = lpd.IdImpuesto,
                        IdComision = lpd.IdComision,
                        IdUsuarioAlta = lpd.IdUsuarioAlta,
                        IdListaPrecio = listaPrecioDestino.IdListaPrecio,
                        IdPresentacion = lpd.IdPresentacion,
                        IdDescuento = lpd.IdDescuento
                    };

                    listaPrecioDetalleRepository.Agregar(listaPrecioDetalle);
                }

                scope.Complete();
            }
        }
    }
}
