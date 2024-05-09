using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class EspecialidadService
    {
        private readonly IEspecialidadRepository especialidadRepository;
        private readonly EspecialidadValidatorService especialidadValidatorService;

        public EspecialidadService(
            IEspecialidadRepository especialidadRepository,
            EspecialidadValidatorService especialidadValidatorService)
        {
            this.especialidadRepository = especialidadRepository;
            this.especialidadValidatorService = especialidadValidatorService;
        }

        public EspecialidadFormularioConsultaDto? ConsultarParaFormulario(int idEspecialidad)
        {
            var especialidad = especialidadRepository.Consultar(idEspecialidad);

            if (especialidad is null)
            {
                return null;
            }

            var especialidadDto = new EspecialidadFormularioConsultaDto
            {
                IdEspecialidad = especialidad.IdEspecialidad,
                Nombre = especialidad.Nombre
            };

            return especialidadDto;
        }

        public IEnumerable<EspecialidadGridDto> ConsultarParaGrid()
        {
            var especialidades = especialidadRepository.Consultar();

            var especialidadesDto = especialidades
                .OrderBy(es => es.Nombre)
                .Select(es => new EspecialidadGridDto
                {
                    IdEspecialidad = es.IdEspecialidad,
                    Nombre = es.Nombre
                });

            return especialidadesDto;
        }

         public void Agregar(EspecialidadFormularioCapturaDto especialidadDto)
        {
            especialidadValidatorService.ValidarAgregar(especialidadDto);

            var especialidad = new Especialidad
            {
               /*  IdEspecialidad = especialidadDto.IdEspecialidad, */
                Nombre = especialidadDto.Nombre
            };

            especialidadRepository.Agregar(especialidad);
        }

        public void Editar(EspecialidadFormularioCapturaDto especialidadDto)
        {
            especialidadValidatorService.ValidarEditar(especialidadDto);

            var especialidad = especialidadRepository.ConsultarPorNombre(especialidadDto.Nombre)!;

            especialidad.Nombre = especialidadDto.Nombre;
            especialidadRepository.Editar(especialidad);
        }

        public void Eliminar(int idEspecialidad)
        {

            var especialidad = especialidadRepository.Consultar(idEspecialidad)!;

            especialidadRepository.Eliminar(especialidad!);
        }
    }
}