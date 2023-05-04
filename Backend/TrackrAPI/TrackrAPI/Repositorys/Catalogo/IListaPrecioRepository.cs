using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IListaPrecioRepository : IRepository<ListaPrecio>
    {
        public IEnumerable<ListaPrecioDto> ConsultarTodosParaSelector();
        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaGrid(int idHospital);
        public IEnumerable<ListaPrecioGridDto> ConsultarTodosPorHospitalParaSelector(int idHospital);
        public ListaPrecioDto ConsultarDto(int idListaPrecios);
        public IEnumerable<ListaPrecioDto> ConsultarVigente(int idHospital);
        public ListaPrecio Consultar(int idListaPrecios);
        public ListaPrecio Consultar(string nombre, int idHospital);
        public ListaPrecio ConsultarPorClave(string clave, int idHospital);
        public ListaPrecioDetalle ConsultarPorPresentacion(int idPresentacion);
        public ListaPrecio ConsultarDependencias(int idListaPrecio);
        public IEnumerable<ListaPrecioDto> ConsultarVigentes();
        public IEnumerable<ListaPrecio> ConsultarPorCompania(int idCompania);
        public IEnumerable<ListaPrecio> ConsultarVigentesPorCompania(int idCompania);

    }
}
