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

        public EspecialdiadService(
            IEspecialidadRepository especialidadRepository,
            EspecialidadValidatorService especialidadValidatorService)
        {
            this.especialidadRepository = especialidadRepository;
            this.especialidadValidatorService = especialidadValidatorService;
        }

        public EspecialidadFormularioConsultaDto? ConsultarParaFormulario(int idEspecialidad)
        {
            var especialidad = especialidadRepository.Consultar(idEspecialidad);

            if (idEspecialidad is null)
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
            var especialidad = especialidadRepository.ConsultarParaGrid();

            var especialidadesDto = especialidades
                .OrderBy(es => es.Nombre)
                .Select(es => new EspecialidadGridDto
                {
                    IdEspecialidad = es.IdEspecialidad,
                    Nombre = es.Nombre
                });

            return especialidadDto;
        }

        public void Agregar(EspecialidadFormularioCapturaDto especialidadDto)
        {
            especialidadValidatorService.ValidarAgregar(especialidadDto);

            var especialidad = new Especialidad
            {
                IdEspecialidad = especialidadDto.idEspecialidad,
                Nombre = especialidadDto.Nombre
            };

            especialidadRepository.Agregar(especialidad);
        }

        public void Editar(EspecialidadFormularioCapturaDto especialidadDto)
        {
            especialidadValidatorService.ValidarEditar(especialdiadDto);

            var especialidad = especialdiadRepository.Consultar(especialidadDto.IdEspecialidad)!;

            especialidad.Nombre = especialidadDto.Nombre;
            especialidad.IdEspecialidad = especialidadDto.IdEspecialidad;

            especialidadRepository.Editar(especialidad);
        }

        public void Eliminar(int idEspecialidad)
        {
            especialidadValidatorService.ValidarEliminar(idEspecialidad);

            var especialdiad = especialidadRepository.Consultar(idEspecialidad)!;

            especialidadRepository.Eliminar(especialdiad);
        }
    }
}