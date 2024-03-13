using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class ColoniaService
    {
        private IColoniaRepository coloniaRepository;
        private ColoniaValidatorService coloniaValidatorService;

        public ColoniaService(
            IColoniaRepository coloniaRepository,
            ColoniaValidatorService coloniaValidatorService
        )
        {
            this.coloniaRepository = coloniaRepository;
            this.coloniaValidatorService = coloniaValidatorService;
        }

        public IEnumerable<Colonia> ConsultarPorCodigoParaSelector(string codigoPostal)
        {
            return coloniaRepository.ConsultarPorCodigoParaSelector(codigoPostal);
        }

        public IEnumerable<ColoniaGridDto> ConsultarParaGrid()
        {
            return coloniaRepository.ConsultarParaGrid();
        }

        public void Agregar(Colonia colonia)
        {
            this.coloniaValidatorService.ValidarAgregar(colonia);
            this.coloniaRepository.Agregar(colonia);
        }

        public void Editar(Colonia colonia)
        {
            this.coloniaValidatorService.ValidarEditar(colonia);
            this.coloniaRepository.Editar(colonia);
        }

        public void Eliminar(int idColonia)
        {
            var colonia = coloniaRepository.Consultar(idColonia);
            this.coloniaValidatorService.ValidarEliminar(colonia.IdColonia);
            this.coloniaRepository.Eliminar(colonia);
        }
    }
}
