using TrackrAPI.Dtos.Catalogo;
using System;
using System.Collections.Generic;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.Catalogo
{
    public interface IConceptoRepository : IRepository<Concepto>
    {
        public IEnumerable<ConceptoGridDto> ConsultarTodosParaGrid(int idCompania);
        public IEnumerable<ConceptoGridDto> ConsultarTodosParaSelector(int idCompania);
        public IEnumerable<ConceptoSelectorDto> ConsultarSelectorParaPresentacion(int idCompania);
        public IEnumerable<ConceptoGridDto> ConsultarParaDesgloseSelector(int idCompania);
        public IEnumerable<ConceptoGridDto> ConsultarOperativosParaSelector(int idCompania);
        public IEnumerable<Concepto> ConsultarPorTipo(string claveTipoConcepto, int idCompania);
        public IEnumerable<Concepto> ConsultarPorCompaniaBase();
        public ConceptoDto ConsultarDto(int idConcepto);
        public Concepto Consultar(int idConcepto);
        public Concepto Consultar(string nombre, int idCompania);
        public Concepto ConsultarPorClave(string clave, int idCompania);
        public Concepto ConsultarPorUsuarioRol(int idUsuario, string claveRol);
        public Concepto ConsultarDependencias(int idConcepto);
    }
}
