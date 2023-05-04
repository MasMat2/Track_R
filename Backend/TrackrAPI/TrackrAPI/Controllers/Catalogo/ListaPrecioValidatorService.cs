using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class ListaPrecioValidatorService
    {
        private IListaPrecioRepository listaPrecioRepository;

        public ListaPrecioValidatorService(IListaPrecioRepository listaPrecioRepository)
        {
            this.listaPrecioRepository = listaPrecioRepository;
        }
        private readonly string MensajeClaveRequerida = "La clave es requerida";
        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeFechaInicioRequerida = "La fecha de inicio es requerida";
        private readonly string MensajeFechaFinRequerida = "La fecha de fin es requerida";
        private readonly string MensajeMonedaRequerida = "El tipo de moneda es requerido";
        private readonly string MensajeObservacionesRequerida = "Las observaciones son requeridas";

        private readonly string MensajeExistencia = "La lista de precio no existe";
        private readonly string MensajeDuplicado = "La lista de precio ya existe";

        private readonly string MensajeClaveFormato = "El formato de clave es alfanumérico";
        private readonly string MensajeNombreFormato = "El formato de nombre es alfanumérico";
        private readonly string MensajeObservacionesFormato = "El formato de las observaciones es alfanumérico";

        private static readonly int LongitudClave = 20;
        private static readonly int LongitudNombre = 50;
        private static readonly int LongitudObservaciones = 50;

        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son { LongitudClave } caracteres";
        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son { LongitudNombre } caracteres";
        private readonly string MensajeObservacionesLongitud = $"La longitud máxima de las observaciones son { LongitudObservaciones } caracteres";

        private readonly string MensajeClienteDependencia = "La lista de precio está asociada al menos a un cliente y no se puede eliminar";
        private readonly string MensajeListaPrecioDefaultDependencia = "La lista de precio está asociada como default en al menos una locación y no se puede eliminar";
        private readonly string MensajeListaPrecioLineaDependencia = "La lista de precio está asociada para compras en línea en al menos una locación y no se puede eliminar";
        private readonly string MensajeSucursalDependencia = "La lista de precio está asociada al menos a una sucursal y no se puede eliminar";

        private readonly string MensajeFechasNoDisponibles = "Las fechas de vigencia propuestas ya se encuentran registradas con otra lista de precio";

        public void ValidarAgregar(ListaPrecio listaPrecio, int idHospital)
        {
            ValidarRequerido(listaPrecio);
            ValidarFormato(listaPrecio);
            ValidarRango(listaPrecio);
            ValidarDuplicado(listaPrecio, idHospital);
        }

        public void ValidarEditar(ListaPrecio listaPrecio, int idHospital)
        {
            ValidarRequerido(listaPrecio);
            ValidarFormato(listaPrecio);
            ValidarRango(listaPrecio);
            ValidarExistencia(listaPrecio.IdListaPrecio);
            ValidarDuplicado(listaPrecio, idHospital);
        }

        public void ValidarEliminar(int idListaPrecio)
        {
            ValidarExistencia(idListaPrecio);
            ValidarDependencia(idListaPrecio);
        }

        public void ValidarRequerido(ListaPrecio listaPrecio)
        {
            Validator.ValidarRequerido(listaPrecio.Clave, MensajeClaveRequerida);
            Validator.ValidarRequerido(listaPrecio.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(listaPrecio.FechaInicioVigencia, MensajeFechaInicioRequerida);
            Validator.ValidarRequerido(listaPrecio.FechaFinVigencia, MensajeFechaFinRequerida);
            Validator.ValidarRequerido(listaPrecio.IdMoneda, MensajeMonedaRequerida);
            Validator.ValidarRequerido(listaPrecio.Observaciones, MensajeObservacionesRequerida);
        }

        public void ValidarFormato(ListaPrecio listaPrecio)
        {
            Validator.ValidarAlfanumerico(listaPrecio.Nombre, MensajeNombreFormato);
            Validator.ValidarAlfanumerico(listaPrecio.Observaciones, MensajeObservacionesFormato);
        }

        public void ValidarRango(ListaPrecio listaPrecio)
        {
            Validator.ValidarLongitudRangoString(listaPrecio.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudRangoString(listaPrecio.Nombre, LongitudNombre, MensajeNombreLongitud);
            Validator.ValidarLongitudRangoString(listaPrecio.Observaciones, LongitudObservaciones, MensajeObservacionesLongitud);
        }

        public void ValidarExistencia(ListaPrecioDto listaPrecios)
        {
            if (listaPrecios == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarExistencia(int idListaPrecio)
        {
            var listaPrecio = listaPrecioRepository.Consultar(idListaPrecio);

            if (listaPrecio == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(ListaPrecio listaPrecio, int idHospital)
        {
            var listaPrecioDuplicado = listaPrecioRepository.Consultar(listaPrecio.Nombre, idHospital);

            if (listaPrecioDuplicado != null && listaPrecio.IdListaPrecio != listaPrecioDuplicado.IdListaPrecio)
            {
                throw new CdisException(MensajeDuplicado);
            }

            listaPrecioDuplicado = listaPrecioRepository.ConsultarPorClave(listaPrecio.Clave, idHospital);

            if (listaPrecioDuplicado != null && listaPrecio.IdListaPrecio != listaPrecioDuplicado.IdListaPrecio)
            {
                throw new CdisException(MensajeDuplicado);
            }
        }

        public void ValidarDependencia(int idListaPrecio)
        {
            var listaPrecio = listaPrecioRepository.ConsultarDependencias(idListaPrecio);

            if (listaPrecio.HospitalIdListaPrecioDefaultNavigation.Any())
            {
                throw new CdisException(MensajeListaPrecioDefaultDependencia);
            }

            if (listaPrecio.HospitalIdListaPrecioLineaNavigation.Any())
            {
                throw new CdisException(MensajeListaPrecioLineaDependencia);
            }

        }

        public void ValidarFechasValidas(ListaPrecio listaPrecio, int idHospital)
        {
            var listaPrecios = listaPrecioRepository.ConsultarTodosPorHospitalParaGrid(idHospital);

            foreach(ListaPrecioGridDto listaPrecioExistente in listaPrecios)
            {
                if (Utileria.FechasOverlap(listaPrecio.FechaInicioVigencia, listaPrecioExistente.FechaInicioVigencia, listaPrecio.FechaFinVigencia, listaPrecioExistente.FechaFinVigencia) && listaPrecio.IdListaPrecio != listaPrecioExistente.IdListaPrecio)
                {
                    throw new CdisException(MensajeFechasNoDisponibles);
                }
            }
        }
    }
}
