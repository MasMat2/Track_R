using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Transactions;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadService
    {
        private readonly IEntidadRepository entidadRepository;
        private readonly IEntidadEstructuraRepository entidadEstructuraRepository;
        private readonly EntidadValidatorService entidadValidatorService;
        private readonly EntidadEstructuraService entidadEstructuraService;

        public EntidadService(IEntidadRepository entidadRepository,
            IEntidadEstructuraRepository entidadEstructuraRepository,
            EntidadValidatorService entidadValidatorService,
            EntidadEstructuraService entidadEstructuraService)
        {
            this.entidadRepository = entidadRepository;
            this.entidadEstructuraRepository = entidadEstructuraRepository;
            this.entidadValidatorService = entidadValidatorService;
            this.entidadEstructuraService = entidadEstructuraService;
        }

        public IEnumerable<EntidadGridDto> ConsultarTodosParaGrid()
        {
            return entidadRepository.ConsultarTodosParaGrid();
        }

        public Entidad ConsultarPorClave(string clave)
        {
            return entidadRepository.ConsultarPorClave(clave);
        }

        public EntidadDto ConsultarDto(int idEntidad)
        {
            return entidadRepository.ConsultarDto(idEntidad);
        }

        public int Agregar(Entidad entidad)
        {
            entidadValidatorService.ValidarAgregar(entidad);
            entidadRepository.Agregar(entidad);
            return entidad.IdEntidad;
        }

        public void Editar(Entidad entidad)
        {
            entidadValidatorService.ValidarEditar(entidad);
            entidadRepository.Editar(entidad);
        }

        public void Eliminar(int idEntidad)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                Entidad entidad = entidadRepository.Consultar(idEntidad);
                var estructuras = entidadEstructuraRepository.ConsultarPorEntidad(entidad.IdEntidad);

                // Eliminar EntidadEstructura
                foreach (EntidadEstructura estructura in estructuras)
                {
                    entidadEstructuraService.Eliminar(estructura.IdEntidadEstructura);
                }

                // Eliminar Entidad
                entidadValidatorService.ValidarEliminar(idEntidad);
                entidadRepository.Eliminar(entidad);

                scope.Complete();
            }
        }
    }
}
