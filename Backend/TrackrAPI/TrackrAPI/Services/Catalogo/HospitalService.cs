using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using TrackrAPI.Repositorys.Contabilidad;
using TrackrAPI.Repositorys.GestionCaja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.Catalogo
{
    public class HospitalService
    {
        private IHospitalRepository hospitalRepository;
        private ICajaRepository cajaRepository;
        private ICajaTurnoRepository cajaTurnoRepository;
        private IListaPrecioClinicaRepository listaPrecioClinicaRepository;
        private IListaPrecioRepository listaPrecioRepository;
        private ICompaniaRepository companiaRepository;
        private ITipoActivoRepository tipoActivoRepository;
        private HospitalValidatorService hospitalValidatorService;
        private HospitalLogotipoService hospitalLogotipoService;

        public HospitalService(IHospitalRepository hospitalRepository,
            ICajaRepository cajaRepository,
            ICajaTurnoRepository cajaTurnoRepository,
            IListaPrecioClinicaRepository listaPrecioClinicaRepository,
            IListaPrecioRepository listaPrecioRepository,
            ICompaniaRepository companiaRepository,
            ITipoActivoRepository tipoActivoRepository,
            HospitalValidatorService hospitalValidatorService,
            HospitalLogotipoService hospitalLogotipoService)
        {
            this.hospitalRepository = hospitalRepository;
            this.cajaRepository = cajaRepository;
            this.cajaTurnoRepository = cajaTurnoRepository;
            this.listaPrecioClinicaRepository = listaPrecioClinicaRepository;
            this.companiaRepository = companiaRepository;
            this.tipoActivoRepository = tipoActivoRepository;
            this.listaPrecioRepository = listaPrecioRepository;
            this.hospitalValidatorService = hospitalValidatorService;
            this.hospitalLogotipoService = hospitalLogotipoService;
        }

        public IEnumerable<HospitalDto> ConsultarPorIdentificadorUrl(string empresa)
        {
            var compania = companiaRepository.ConsultarPorIdentificadorUrl(empresa);

            if (compania == null)
            {
                throw new CdisException("La compañía no existe");
            }

            return hospitalRepository.ConsultarPorCompania(compania.IdCompania)
                   .Where(l => l.IdListaPrecioLinea > 0);
        }

        public HospitalDto ConsultarPorID(int idHospital)
        {
            return hospitalRepository.ConsultarPorID(idHospital);
        }

        public HospitalDto ConsultarDefaultPorCompania(int idCompania)
        {
            return hospitalRepository.ConsultarDefaultPorCompania(idCompania);
        }

        public IEnumerable<HospitalGridDto> ConsultarPorCompaniaParaGrid(int idCompania)
        {
            return hospitalRepository.ConsultarPorCompaniaParaGrid(idCompania);
        }

        public HospitalDto ConsultarDto(int idHospital)
        {
            return hospitalRepository.ConsultarDto(idHospital);
        }

        public IEnumerable<HospitalGridDto> ConsultarGeneral(int idCompania)
        {
            return hospitalRepository.ConsultarGeneral(idCompania);
        }

        public IEnumerable<HospitalDto> ConsultarPorCompania(int idCompania)
        {
            return hospitalRepository.ConsultarPorCompania(idCompania);
        }

        public IEnumerable<HospitalDto> ConsultarTodosParaSelector(int idDominio)
        {
            return hospitalRepository.ConsultarTodosParaSelector(idDominio);
        }

        public IEnumerable<HospitalDto> ConsultarDisponiblesParaListaPrecio(int? idListaPrecioSeleccionada)
        {
            return hospitalRepository.ConsultarDisponiblesParaListaPrecio(idListaPrecioSeleccionada);
        }

        public int Agregar(Hospital hospital)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                hospital.FechaContableActual = Utileria.ObtenerFechaActual();

                hospitalValidatorService.ValidarAgregar(hospital);
                int? idLocacionPredeterminadaAntigua = hospitalValidatorService.ValidarPredeterminada(hospital);
                hospitalRepository.Agregar(hospital);

                if (idLocacionPredeterminadaAntigua > 0)
                {
                    Hospital locacionPredeterminadaAntigua = hospitalRepository.Consultar((int)idLocacionPredeterminadaAntigua);
                    locacionPredeterminadaAntigua.Predeterminada = false;
                    hospitalRepository.Editar(locacionPredeterminadaAntigua);
                }

                var listaPrecio = new ListaPrecio()
                {
                    Nombre = "Lista de precio base",
                    Clave = $"Base-{hospital.IdHospital}",
                    FechaAlta = DateTime.Now,
                    FechaInicioVigencia = DateTime.Today,
                    FechaFinVigencia = DateTime.Today.AddYears(1),
                    Observaciones = "Ninguna",
                    TodasClinicas = false
                };

                listaPrecioRepository.Agregar(listaPrecio);

                var listaPrecioClinica = new ListaPrecioClinica()
                {
                    IdClinica = hospital.IdHospital,
                    IdListaPrecio = listaPrecio.IdListaPrecio
                };

                listaPrecioClinicaRepository.Agregar(listaPrecioClinica);

                // Se agrega la caja automatica

                var cajaAutomatica = new Caja();
                cajaAutomatica.Automatica = true;
                cajaAutomatica.Nombre = "Caja automática";
                cajaAutomatica.Descripcion = "Caja automática para los pagos en línea, depósitos y transferencias";
                cajaAutomatica.IdHospital = hospital.IdHospital;
                cajaAutomatica.IdTipoActivo = tipoActivoRepository.ConsultarPorClave(GeneralConstant.ClaveTipoActivoCirculanteCaja).IdTipoActivo;

                cajaRepository.Agregar(cajaAutomatica);

                // Se agrega la caja 1
                var caja = new Caja();
                caja.Automatica = false;
                caja.Nombre = "Caja 1";
                caja.Descripcion = "Caja 1";
                caja.IdHospital = hospital.IdHospital;
                caja.IdTipoActivo = tipoActivoRepository.ConsultarPorClave(GeneralConstant.ClaveTipoActivoCirculanteCaja).IdTipoActivo;

                cajaRepository.Agregar(caja);

                scope.Complete();

                return hospital.IdHospital;
            }
        }

        public void Editar(Hospital hospital)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                hospitalValidatorService.ValidarEditar(hospital);
                int? idLocacionPredeterminadaAntigua = hospitalValidatorService.ValidarPredeterminada(hospital);
                hospitalRepository.Editar(hospital);

                if (idLocacionPredeterminadaAntigua > 0)
                {
                    Hospital locacionPredeterminadaAntigua = hospitalRepository.Consultar((int)idLocacionPredeterminadaAntigua);
                    locacionPredeterminadaAntigua.Predeterminada = false;
                    hospitalRepository.Editar(locacionPredeterminadaAntigua);
                }

                scope.Complete();
            }
        }

        public void Eliminar(int idHotel)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                hospitalValidatorService.ValidarEliminar(idHotel);

                Hospital hospital = hospitalRepository.Consultar(idHotel);

                // var cajaTurnos = cajaTurnoRepository.ConsultarPorHotel(idHotel).ToList();
                // cajaTurnoRepository.Eliminar(cajaTurnos);

                var cajas = cajaRepository.ConsultarPorHotel(idHotel);
                foreach (var caja in cajas)
                {
                    cajaRepository.Eliminar(caja);
                }

                foreach (var imagen in hospital.HospitalLogotipo)
                {
                    hospitalLogotipoService.Eliminar((int)imagen.IdHospitalLogotipo);
                }

                hospitalRepository.Eliminar(hospital);

                scope.Complete();
            }
        }
    }
}
