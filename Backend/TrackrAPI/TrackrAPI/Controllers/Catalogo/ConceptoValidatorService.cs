using TrackrAPI.Dtos;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Linq;

namespace TrackrAPI.Services.Catalogo
{
    public class ConceptoValidatorService
    {
        private IConceptoRepository conceptoRepository;

        public ConceptoValidatorService(IConceptoRepository conceptoRepository)
        {
            this.conceptoRepository = conceptoRepository;
        }

        private readonly string MensajeExistencia = "El concepto no existe";
        private readonly string MensajeClaveDuplicado = "Ya existe un concepto con esa clave";
        private readonly string MensajeNombreDuplicado = "Ya existe un concepto con ese nombre";

        private readonly string MensajeClaveRequerido = "La clave es requerida";
        private readonly string MensajeNombreRequerido = "El nombre es requerido";
        private readonly string MensajeTipoMovimientoRequerido = "El tipo de movimiento es requerido";
        private readonly string MensajeTipoConceptoRequerido = "El tipo de concepto es requerido";

        private static readonly int LongitudClave = 50;
        private static readonly int LongitudNombre = 100;

        private readonly string MensajeClaveLongitud = $"La longitud máxima de la clave son { LongitudClave } caracteres";
        private readonly string MensajeNombreLongitud = $"La longitud máxima del nombre son { LongitudNombre } caracteres";

        private readonly string MensajeDependenciaFactura = "El auxiliar está siendo utilizado por al menos una factura y no se puede eliminar";
        private readonly string MensajeDependenciaGasto = "El auxiliar está siendo utilizado por al menos un gasto y no se puede eliminar";
        private readonly string MensajeDependenciaNotaGasto = "El auxiliar está siendo utilizado por al menos una nota de gasto y no se puede eliminar";
        private readonly string MensajeDependenciaNotaVenta = "El auxiliar está siendo utilizado por al menos una nota de venta y no se puede eliminar";
        private readonly string MensajeDependenciaPresentacion = "El auxiliar está siendo utilizado por al menos una presentacion y no se puede eliminar";
        private readonly string MensajeDependenciaPuntoVenta = "El auxiliar está siendo utilizado por al menos un punto de venta y no se puede eliminar";
        private readonly string MensajeDependenciaTipoMovimientoMaterial = "El auxiliar está siendo utilizado por al menos un tipo de movimiento material y no se puede eliminar";
        private readonly string MensajeDependenciaRol = "El auxiliar está siendo utilizado por al menos un rol de usuario y no se puede eliminar";


        public void ValidarAgregar(ConceptoFormularioDto concepto)
        {
            ValidarRequerido(concepto);
            ValidarRango(concepto);
            ValidarDuplicado(concepto);
            ValidarFormato(concepto);
        }

        public void ValidarEditar(ConceptoFormularioDto concepto)
        {
            ValidarRequerido(concepto);
            ValidarRango(concepto);
            ValidarExistencia(concepto.IdConcepto);
            ValidarDuplicado(concepto);
            ValidarFormato(concepto);
        }

        public void ValidarEliminar(int idConcepto)
        {
            ValidarExistencia(idConcepto);
            ValidarDependencias(idConcepto);
        }

        public void ValidarRequerido(ConceptoFormularioDto concepto)
        {
            Validator.ValidarRequerido(concepto.Clave, MensajeClaveRequerido);
            Validator.ValidarRequerido(concepto.Nombre, MensajeNombreRequerido);
            Validator.ValidarRequerido(concepto.IdsTipoConcepto, MensajeTipoConceptoRequerido);
        }
        public void ValidarFormato(ConceptoFormularioDto concepto)
        {
        }

        public void ValidarRango(ConceptoFormularioDto concepto)
        {
            Validator.ValidarLongitudRangoString(concepto.Clave, LongitudClave, MensajeClaveLongitud);
            Validator.ValidarLongitudRangoString(concepto.Nombre, LongitudNombre, MensajeNombreLongitud);
        }

        public void ValidarExistencia(int idConcepto)
        {
            var concepto = conceptoRepository.Consultar(idConcepto);

            if (concepto == null)
            {
                throw new CdisException(MensajeExistencia);
            }
        }

        public void ValidarDuplicado(ConceptoFormularioDto concepto)
        {
            var conceptoDuplicado = conceptoRepository.Consultar(concepto.Nombre, (int)concepto.IdCompania);
            if (conceptoDuplicado != null && concepto.IdConcepto != conceptoDuplicado.IdConcepto)
            {
                throw new CdisException(MensajeNombreDuplicado);
            }

            var conceptoDuplicado2 = conceptoRepository.ConsultarPorClave(concepto.Clave, (int)concepto.IdCompania);
            if (conceptoDuplicado2 != null && concepto.IdConcepto != conceptoDuplicado2.IdConcepto)
            {
                throw new CdisException(MensajeClaveDuplicado);
            }
        }

        public void ValidarDependencias(int idConcepto)
        {
            Concepto aux = conceptoRepository.ConsultarDependencias(idConcepto);
            if (aux.FacturaConcepto.Any())
            {
                throw new CdisException(MensajeDependenciaFactura);
            }
            if (aux.GastoConcepto.Any())
            {
                throw new CdisException(MensajeDependenciaGasto);
            }
            if (aux.NotaGasto.Any())
            {
                throw new CdisException(MensajeDependenciaNotaGasto);
            }
            if (aux.NotaGastoDetalle.Any())
            {
                throw new CdisException(MensajeDependenciaNotaGasto);
            }
            if (aux.NotaVenta.Any())
            {
                throw new CdisException(MensajeDependenciaNotaVenta);
            }
            if (aux.NotaVentaDetalle.Any())
            {
                throw new CdisException(MensajeDependenciaNotaVenta);
            }
            if (aux.Presentacion.Any())
            {
                throw new CdisException(MensajeDependenciaPresentacion);
            }
            if (aux.PuntoVenta.Any())
            {
                throw new CdisException(MensajeDependenciaPuntoVenta);
            }
            if (aux.UsuarioRol.Any())
            {
                throw new CdisException(MensajeDependenciaRol);
            }
        }
    }
}
