using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.GestionEntidad;
using System.Transactions;
using TrackrAPI.Helpers;
using System.Collections.Generic;

namespace TrackrAPI.Services.GestionEntidad
{
    public class EntidadService
    {
        private readonly IEntidadRepository entidadRepository;
        private readonly IEntidadEstructuraRepository entidadEstructuraRepository;
        private readonly EntidadValidatorService entidadValidatorService;
        private readonly EntidadEstructuraService entidadEstructuraService;
        private readonly SeccionCampoService seccionCampoService;
        private readonly SeccionService seccionService;

        public EntidadService(IEntidadRepository entidadRepository,
            IEntidadEstructuraRepository entidadEstructuraRepository,
            EntidadValidatorService entidadValidatorService,
            EntidadEstructuraService entidadEstructuraService,
            SeccionCampoService seccionCampoService,
            SeccionService seccionService)
        {
            this.entidadRepository = entidadRepository;
            this.entidadEstructuraRepository = entidadEstructuraRepository;
            this.entidadValidatorService = entidadValidatorService;
            this.entidadEstructuraService = entidadEstructuraService;
            this.seccionCampoService = seccionCampoService;
            this.seccionService = seccionService;
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
            using var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions {
                    IsolationLevel = IsolationLevel.ReadCommitted
                });

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

        public record ActualizacionExpedienteParams(
            List<EntidadEstructuraDto> ArbolPadecimientos,
            List<EntidadEstructuraDto> ArbolExpediente,
            List<string> ClavesExpediente,
            int IdEntidadExpediente
        );

        public void ActualizarExpedienteTrackr()
        {
            const string clavePadecimientos = "003";
            const string claveExpedienteTrackr = "002";

            var arbolPadecimientos = ConsultarArbol(clavePadecimientos, "padecimientos");
            var arbolExpediente = ConsultarArbol(claveExpedienteTrackr, "expediente trackr");

            var clavesExpediente = arbolExpediente.ConvertAll(pestana => pestana.Clave!);

            var idEntidadExpediente = arbolExpediente.FirstOrDefault()!.IdEntidad;

            var aeParams = new ActualizacionExpedienteParams(
                arbolPadecimientos,
                arbolExpediente,
                clavesExpediente,
                idEntidadExpediente);

            using var ts = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }
            );

            ActualizarMuestras(aeParams);
            // ActualizarPadecimientos(aeParams);

            ts.Complete();
        }

        private List<EntidadEstructuraDto> ConsultarArbol(string clave, string nombreEntidad)
        {
            var entidad = entidadRepository.ConsultarPorClave(clave);
            if (entidad == null)
            {
                throw new CdisException($"No se encontró la entidad {nombreEntidad} con clave {clave}");
            }

            var arbolPadecimientos = entidadEstructuraService.ConsultarParaTabulador(entidad.IdEntidad);
            return arbolPadecimientos.ToList();
        }

        private void ActualizarMuestras(ActualizacionExpedienteParams aeParams)
        {
            // TODO: 2023-06-07 -> Constante
            const string claveSeccionMedidas = "SE-010";
            var seccionMedidas = seccionService.ConsultarPorClave(claveSeccionMedidas);

            if (seccionMedidas == null)
            {
                throw new CdisException("No se encontró la sección <strong>Medidas</strong>");
            }

            var camposMedidas = seccionCampoService.ConsultarPorSeccion(seccionMedidas.IdSeccion);

            var idsSeccionesPadecimientos = aeParams.ArbolPadecimientos
                .SelectMany(padecimiento => padecimiento.Hijos.Select(h => h.IdSeccion))
                .Distinct();

            var fila = 0;
            foreach (var idSeccion in idsSeccionesPadecimientos)
            {
                var seccionPadecimiento = aeParams.ArbolPadecimientos
                    .Where(padecimiento => padecimiento.Hijos.Any(h => h.IdSeccion == idSeccion))
                    .Select(padecimiento => padecimiento.Hijos.Find(h => h.IdSeccion == idSeccion))
                    .FirstOrDefault()!;

                foreach (var campo in seccionPadecimiento.Campos)
                {
                    var campoMedidas = camposMedidas.FirstOrDefault(c => c.Clave == "ME-" + campo.Clave);

                    if (campoMedidas != null)
                    {
                        // Editar
                        var campoDb = new SeccionCampo
                        {
                            IdSeccionCampo = campoMedidas.IdSeccionCampo,
                            IdSeccion = campoMedidas.IdSeccion,
                            IdDominio = campoMedidas.IdDominio,
                            Clave = campoMedidas.Clave,
                            Descripcion = campo.Descripcion,
                            Orden = campoMedidas.Orden,
                            Deshabilitado = campo.Deshabilitado,
                            Requerido = campo.Requerido,
                            TamanoColumna = campo.TamanoColumna,
                            Grupo = campoMedidas.Grupo,
                            Fila = campoMedidas.Fila,
                        };

                        seccionCampoService.Editar(campoDb);
                    }
                    else
                    {
                        var nuevoCampo = new SeccionCampo
                        {
                            IdSeccion = seccionMedidas.IdSeccion!,
                            IdDominio = campo.IdDominio,
                            Clave = "ME-" + campo.Clave,
                            Descripcion = campo.Descripcion,
                            Orden = campo.Orden,
                            Deshabilitado = campo.Deshabilitado,
                            Requerido = campo.Requerido,
                            TamanoColumna = campo.TamanoColumna,
                            Grupo = seccionPadecimiento.Nombre,
                            Fila = fila,
                        };

                        seccionCampoService.Agregar(nuevoCampo);
                    }
                }

                fila++;
            }
        }

        private void ActualizarPadecimientosExistentes(ActualizacionExpedienteParams aeParams)
        {
            var pestanasExistentes = aeParams.ArbolPadecimientos
                .Where(pestana => aeParams.ClavesExpediente.Contains(pestana.Clave!))
                .ToList();

            foreach (var padecimiento in pestanasExistentes)
            {
                var pestanaExpediente = aeParams.ArbolExpediente
                    .Find(pestana => pestana.Clave == padecimiento.Clave)!;

                var seccionesId = pestanaExpediente.Hijos
                    .ConvertAll(h => h.IdSeccion);

                var nuevasSecciones = padecimiento.Hijos
                    .Where(hijo => !seccionesId.Contains(hijo.IdSeccion))
                    .Select(seccion => new EntidadEstructuraDto
                    {
                        IdEntidad = aeParams.IdEntidadExpediente,
                        Nombre = null,
                        Clave = null,
                        Tabulacion = null,
                        IdSeccion = seccion.IdSeccion,
                        IdEntidadEstructuraPadre = pestanaExpediente.IdEntidadEstructura,
                    })
                    .ToList();

                entidadEstructuraService.Agregar(nuevasSecciones);
            }
        }

        private void AgregarNuevosPadecimientos(ActualizacionExpedienteParams aeParams)
        {
            var pestanasNuevas = aeParams.ArbolPadecimientos
                .Where(pestana => !aeParams.ClavesExpediente.Contains(pestana.Clave!))
                .ToList();

            foreach (var pestana in pestanasNuevas)
            {
                pestana.IdEntidad = aeParams.IdEntidadExpediente;

                entidadEstructuraService.Agregar(pestana);

                foreach (var hijo in pestana.Hijos)
                {
                    hijo.IdEntidad = aeParams.IdEntidadExpediente;
                    hijo.IdEntidadEstructuraPadre = pestana.IdEntidadEstructura;
                }

                entidadEstructuraService.Agregar(pestana.Hijos);
            }
        }
    }
}
