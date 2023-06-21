using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class AyudaSeccionService
    {
        private IAyudaSeccionRepository ayudaSeccionRepository;
        private AyudaSeccionValidatorService ayudaSeccionValidatorService;
        public AyudaSeccionService(IAyudaSeccionRepository ayudaSeccionRepository,
            AyudaSeccionValidatorService ayudaSeccionValidatorService)
        {
            this.ayudaSeccionRepository = ayudaSeccionRepository;
            this.ayudaSeccionValidatorService = ayudaSeccionValidatorService;
        }

        public IEnumerable<AyudaSeccionSelectorDto> ConsultarParaSelector()
        {
            return ayudaSeccionRepository.ConsultarParaSelector();
        }

        public void Agregar(AyudaSeccion ayudaSeccion)
        {
            this.ayudaSeccionValidatorService.ValidarAgregar(ayudaSeccion);
            this.ayudaSeccionRepository.Agregar(ayudaSeccion);
        }

        public void Editar(AyudaSeccion ayudaSeccion)
        {
            this.ayudaSeccionValidatorService.ValidarEditar(ayudaSeccion);
            this.ayudaSeccionRepository.Editar(ayudaSeccion);
        }

        public void Eliminar(int idAyudaSeccion)
        {
            var ayudaSeccion = this.ayudaSeccionRepository.Consultar(idAyudaSeccion);
            this.ayudaSeccionValidatorService.ValidarEliminar(idAyudaSeccion);
            this.ayudaSeccionRepository.Eliminar(ayudaSeccion);
        }
    }
}
