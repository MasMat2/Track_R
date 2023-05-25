using TrackrAPI.Dtos.Contabilidad;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadEstructuraService
    {
        private readonly IEntidadEstructuraRepository entidadEstructuraRepository;
        private readonly EntidadEstructuraValidatorService entidadEstructuraValidatorService;

        public EntidadEstructuraService
        (
            IEntidadEstructuraRepository entidadEstructuraRepository,
            EntidadEstructuraValidatorService entidadEstructuraValidatorService
        )
        {
            this.entidadEstructuraRepository = entidadEstructuraRepository;
            this.entidadEstructuraValidatorService = entidadEstructuraValidatorService;
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarPorEntidadParaSelector(int idEntidad)
        {
            return entidadEstructuraRepository.ConsultarPorEntidad(idEntidad)
                   .Where(e => e.Tabulacion == true)
                   .Select( e => new EntidadEstructuraDto
                   {
                       IdEntidadEstructura = e.IdEntidadEstructura,
                       Nombre = e.Nombre,
                   });
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarArbol(int idEntidad)
        {
            var arbol = entidadEstructuraRepository.ConsultarArbol(idEntidad);

            return arbol;
        }

        public void Agregar(EntidadEstructuraDto estructuraDto)
        {
            EntidadEstructura entidadEstructura = new()
            {
                Nombre = estructuraDto.Nombre,
                Clave = estructuraDto.Clave,
                Tabulacion = estructuraDto.Tabulacion,
                IdEntidad = estructuraDto.IdEntidad,
                IdSeccion = estructuraDto.IdSeccion,
                IdEntidadEstructuraPadre = estructuraDto.IdEntidadEstructuraPadre
            };

            entidadEstructuraValidatorService.ValidarAgregar(entidadEstructura);
            entidadEstructuraRepository.Agregar(entidadEstructura);

            estructuraDto.IdEntidadEstructura = entidadEstructura.IdEntidadEstructura;
        }

        public void Agregar(List<EntidadEstructuraDto> estructurasDto)
        {
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }
            );

            foreach (EntidadEstructuraDto estructuraDto in estructurasDto)
            {
                Agregar(estructuraDto);
            }

            scope.Complete();
        }

        public void Eliminar(int idEntidadEstructura)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                EntidadEstructura entidadEstructura = entidadEstructuraRepository.Consultar(idEntidadEstructura);

                if (entidadEstructura != null)
                {
                    var hijos = entidadEstructuraRepository.ConsultarHijosDeEstructura(entidadEstructura.IdEntidadEstructura);
                    EliminarHijos(hijos);

                    entidadEstructuraValidatorService.ValidarEliminar(entidadEstructura.IdEntidadEstructura);
                    entidadEstructuraRepository.Eliminar(entidadEstructura);
                }
                scope.Complete();
            }
        }

        private void EliminarHijos(List<EntidadEstructura> entidadEstucturaList)
        {
            foreach (EntidadEstructura estructura in entidadEstucturaList)
            {
                var hijos = entidadEstructuraRepository.ConsultarHijosDeEstructura(estructura.IdEntidadEstructura);

                if (hijos.Any())
                {
                    EliminarHijos(hijos);
                }

                entidadEstructuraValidatorService.ValidarEliminar(estructura.IdEntidadEstructura);
                entidadEstructuraRepository.Eliminar(estructura);
            }
        }
    }
}
