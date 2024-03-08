using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class PuntoVentaService
    {

        private IPuntoVentaRepository puntoVentaRepository;
        private PuntoVentaValidatorService puntoVentaValidatorService;

        public PuntoVentaService(IPuntoVentaRepository puntoVentaRepository,
            PuntoVentaValidatorService puntoVentaValidatorService)
        {
            this.puntoVentaValidatorService = puntoVentaValidatorService;
            this.puntoVentaRepository = puntoVentaRepository;
        }

        public PuntoVentaDto ConsultarDto(int idPuntoVenta)
        {
            var puntoVenta = puntoVentaRepository.ConsultarDto(idPuntoVenta);
            puntoVentaValidatorService.ValidarExistencia(puntoVenta);
            return puntoVenta;
        }

        public PuntoVentaDto ConsultarDto(int idPuntoVenta, Usuario usuarioSesion)
        {
            var puntoVenta = puntoVentaRepository.ConsultarDto(idPuntoVenta);
            puntoVentaValidatorService.ValidarExistencia(puntoVenta);
            puntoVenta.NombreVendedor = usuarioSesion.Nombre + " " + usuarioSesion.ApellidoPaterno + " " + (usuarioSesion.ApellidoMaterno != null ? usuarioSesion.ApellidoMaterno : " ");
            puntoVenta.IdVendedor = usuarioSesion.IdUsuario;
            return puntoVenta;
        }

        public IEnumerable<PuntoVentaGridDto> ConsultarTodosParaGrid(int idCompania)
        {
            return puntoVentaRepository.ConsultarTodosParaGrid(idCompania);
        }

        public IEnumerable<PuntoVentaDto> ConsultarTodosParaSelector(int idCompania)
        {
            return puntoVentaRepository.ConsultarTodosParaSelector(idCompania);
        }

        public PuntoVentaDto consultarPorUsuarioEnSesion(Usuario usuarioSesion)
        {
            if(usuarioSesion.IdPuntoVenta != null)
                return puntoVentaRepository.ConsultarDto((int)usuarioSesion.IdPuntoVenta);
            else
                return new PuntoVentaDto{ IdPuntoVenta = 0};
        }

        public IEnumerable<UsuarioDto> ConsultarUsuariosAsignados(int idPuntoVenta)
        {
            return puntoVentaRepository.ConsultarUsuariosAsignados(idPuntoVenta);
        }

        public int Agregar(PuntoVenta puntoVenta)
        {
            puntoVentaValidatorService.ValidarAgregar(puntoVenta);
            puntoVentaRepository.Agregar(puntoVenta);
            return puntoVenta.IdPuntoVenta;
        }

        public void Editar(PuntoVenta tipoPresentacion)
        {
            puntoVentaValidatorService.ValidarEditar(tipoPresentacion);
            puntoVentaRepository.Editar(tipoPresentacion);
        }

        public void Eliminar(int idPuntoVenta)
        {
            var puntoVenta = puntoVentaRepository.Consultar(idPuntoVenta);
            puntoVentaValidatorService.ValidarEliminar(idPuntoVenta);
            puntoVentaRepository.Eliminar(puntoVenta);
        }

    }
}
