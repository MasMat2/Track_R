using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IUbicacionVentaRepository : IRepository<UbicacionVenta>
    {
        public IEnumerable<UbicacionVentaDto> ConsultarTodosParaSelector();
        public UbicacionVentaDto ConsultarDto(int idUbicacionVenta);
        public UbicacionVenta Consultar(int idUbicacionVenta);
        public UbicacionVenta ConsultarPorNombre(string nombre, int idCompania);
        public UbicacionVenta ConsultarPorClave(string clave, int idCompania);
        public IEnumerable<UbicacionVentaDto> ConsultarPorPuntoVenta(int idPuntoVenta, int idUsuarioVendedor, DateTime fechaContable);
        public IEnumerable<UbicacionVentaDto> ConsultarTodosParaGrid(int idCompania);
        public UbicacionVenta ConsultarDependencias(int idUbicacionVenta);
    }
}
