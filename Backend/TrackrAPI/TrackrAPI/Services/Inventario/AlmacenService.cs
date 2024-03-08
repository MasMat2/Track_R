using TrackrAPI.Dtos.Inventario;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Inventario;
using System.Collections.Generic;
using System.Transactions;

namespace TrackrAPI.Services.Inventario
{
    public class AlmacenService
    {
        private IAlmacenRepository almacenRepository;
        private IUbicacionRepository ubicacionRepository;
        private IUsuarioAlmacenRepository usuarioAlmacenRepository;
        private AlmacenValidatorService almacenValidatorService;

        public AlmacenService(IAlmacenRepository almacenRepository,
            IUbicacionRepository ubicacionRepository,
            IUsuarioAlmacenRepository usuarioAlmacenRepository,
            AlmacenValidatorService almacenValidatorService)
        {
            this.almacenRepository = almacenRepository;
            this.ubicacionRepository = ubicacionRepository;
            this.usuarioAlmacenRepository = usuarioAlmacenRepository;
            this.almacenValidatorService = almacenValidatorService;
        }

        public AlmacenDto ConsultarDto(int idAlmacen)
        {
            return almacenRepository.ConsultarDto(idAlmacen);
        }

        public IEnumerable<AlmacenGridDto> ConsultarGeneral(int idUsuario)
        {
            return almacenRepository.ConsultarGeneral(idUsuario);
        }

        public IEnumerable<AlmacenDto> ConsultarPorCompania(int idCompania, int idUsuario)
        {
            return almacenRepository.ConsultarPorCompania(idCompania, idUsuario);
        }

        public IEnumerable<AlmacenGridDto> ConsultarPorEstado(int idEstado)
        {
            return almacenRepository.ConsultarPorEstado(idEstado);
        }

        public IEnumerable<AlmacenGridDto> ConsultarPorEstatus(int idEstatusAlmacen)
        {
            return almacenRepository.ConsultarPorEstatus(idEstatusAlmacen);
        }

        public IEnumerable<AlmacenGridDto> ConsultarPorUsuario(int idUsuarioResponsable)
        {
            return almacenRepository.ConsultarPorUsuario(idUsuarioResponsable);
        }

        public IEnumerable<AlmacenDto> ConsultarTodosParaSelector(int idCompania)
        {
            return almacenRepository.ConsultarTodosParaSelector(idCompania);
        }

        public int Agregar(Almacen almacen)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                almacenValidatorService.ValidarAgregar(almacen);

                var idAlmacen = almacenRepository.Agregar(almacen).IdAlmacen;

                var ubicacionDefault = new Ubicacion();
                ubicacionDefault.Nombre = "General";
                ubicacionDefault.IdAlmacen = almacen.IdAlmacen;

                ubicacionRepository.Agregar(ubicacionDefault);

                scope.Complete();

                return idAlmacen;
            }
        }

        public void Editar(Almacen almacen)
        {
            almacenValidatorService.ValidarEditar(almacen);

            almacenRepository.Editar(almacen);
        }

        public void Eliminar(int idAlmacen)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Almacen almacen = almacenRepository.Consultar(idAlmacen);

                almacenValidatorService.ValidarEliminar(idAlmacen);

                var usuariosAlmacen = usuarioAlmacenRepository.ConsultarPorAlmacen(idAlmacen);

                usuarioAlmacenRepository.Eliminar(usuariosAlmacen);

                almacenRepository.Eliminar(almacen);

                scope.Complete();
            }
        }
    }
}
