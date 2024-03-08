using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Transactions;
using TrackrAPI.Dtos.GestionExpediente;

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

        public IEnumerable<EntidadEstructuraDto> ConsultarPorEntidad(string clave)
        {
            return entidadEstructuraRepository.ConsultarPorEntidad(clave)
                .Select(e => new EntidadEstructuraDto
                {
                    IdEntidadEstructura = e.IdEntidadEstructura,
                    Nombre = e.Nombre,
                    Clave = e.Clave,
                    Tabulacion = e.Tabulacion,
                    IdEntidad = e.IdEntidad,
                    IdSeccion = e.IdSeccion,
                    IdEntidadEstructuraPadre = e.IdEntidadEstructuraPadre
                });
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarParaTabulador(int idEntidad)
        {
            List<EntidadEstructuraDto> estructurasPadre = entidadEstructuraRepository.ConsultarPadres(idEntidad).ToList();

            return ConsultarHijos(estructurasPadre).OrderBy(e => e.IdEntidadEstructura);
        }

        public IEnumerable<EntidadEstructuraDto> ConsultarHijos(IEnumerable<EntidadEstructuraDto> estructurasPadre)
        {
            foreach (EntidadEstructuraDto padre in estructurasPadre)
            {
                padre.Hijos = entidadEstructuraRepository.ConsultarHijos(padre.IdEntidadEstructura).ToList();

                if (padre.Hijos.Any())
                {
                    ConsultarHijos(padre.Hijos);
                }
            }

            return estructurasPadre;
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
                IdEntidadEstructuraPadre = estructuraDto.IdEntidadEstructuraPadre,
                IdIcono = estructuraDto.IdIcono,
                IdTipoWidget = estructuraDto.IdTipoWidget,
                EsAntecedente = estructuraDto.EsAntecedente ?? false,
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
                var entidadEstructura = entidadEstructuraRepository.Consultar(idEntidadEstructura);

                if (entidadEstructura is not null)
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

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarPadecimientosParaSelector()
        {
            return entidadEstructuraRepository.ConsultarPadecimientosParaSelector();
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarDiagnosticosParaSelector()
        {
            return entidadEstructuraRepository.ConsultarDiagnosticosParaSelector();
        }

        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarAntecedentesParaSelector()
        {
            return entidadEstructuraRepository.ConsultarAntecedentesParaSelector();
        }
    }
}
