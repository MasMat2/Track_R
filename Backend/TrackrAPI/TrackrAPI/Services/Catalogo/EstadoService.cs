using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class EstadoService
    {
        private readonly IEstadoRepository estadoRepository;
        private readonly EstadoValidatorService estadoValidatorService;

        public EstadoService(
            IEstadoRepository estadoRepository,
            EstadoValidatorService estadoValidatorService)
        {
            this.estadoRepository = estadoRepository;
            this.estadoValidatorService = estadoValidatorService;
        }

        public EstadoDto? Consultar(int idEstado)
        {
            var estado = estadoRepository.Consultar(idEstado);

            if (estado is null)
            {
                return null;
            }

            var estadoDto = new EstadoDto
            {
                IdEstado = estado.IdEstado,
                Clave = estado.Clave ?? string.Empty,
                Nombre = estado.Nombre,
                IdPais = estado.IdPais
            };

            return estadoDto;
        }

        public EstadoFormularioConsultaDto? ConsultarParaFormulario(int idEstado)
        {
            var estado = estadoRepository.Consultar(idEstado);

            if (estado is null)
            {
                return null;
            }

            var estadoDto = new EstadoFormularioConsultaDto
            {
                IdEstado = estado.IdEstado,
                Clave = estado.Clave ?? string.Empty,
                Nombre = estado.Nombre,
                IdPais = estado.IdPais
            };

            return estadoDto;
        }

        public IEnumerable<EstadoGridDto> ConsultarParaGrid()
        {
            var estados = estadoRepository.ConsultarParaGrid();

            var estadosDto = estados
                .OrderBy(e => e.Nombre)
                .Select(e => new EstadoGridDto
                {
                    IdEstado = e.IdEstado,
                    Nombre = e.Nombre,
                    Clave = e.Clave ?? string.Empty,
                    NombrePais = e.IdPaisNavigation.Nombre
                });

            return estadosDto;
        }

        public IEnumerable<EstadoSelectorDto> ConsultarPorPaisParaSelector(int idPais)
        {
            var estados = estadoRepository.ConsultarPorPais(idPais);

            var estadosDto = estados
                .Select(e => new EstadoSelectorDto(
                    e.IdEstado,
                    e.Nombre
                ));

            return estadosDto;
        }

        public void Agregar(EstadoFormularioCapturaDto estadoDto)
        {
            estadoValidatorService.ValidarAgregar(estadoDto);

            var estado = new Estado
            {
                Clave = estadoDto.Clave,
                Nombre = estadoDto.Nombre,
                IdPais = estadoDto.IdPais
            };

            estadoRepository.Agregar(estado);
        }

        public void Editar(EstadoFormularioCapturaDto estadoDto)
        {
            estadoValidatorService.ValidarEditar(estadoDto);

            var estado = estadoRepository.Consultar(estadoDto.IdEstado)!;

            estado.Nombre = estadoDto.Nombre;
            estado.IdPais = estadoDto.IdPais;

            estadoRepository.Editar(estado);
        }

        public void Eliminar(int idEstado)
        {
            estadoValidatorService.ValidarEliminar(idEstado);

            var estado = estadoRepository.Consultar(idEstado)!;

            estadoRepository.Eliminar(estado);
        }
    }
}
