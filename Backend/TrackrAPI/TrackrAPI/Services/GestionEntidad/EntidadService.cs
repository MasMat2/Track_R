﻿using TrackrAPI.Dtos.GestionEntidad;
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

            AgregarNuevasMuestras(aeParams);
            ActualizarPadecimientos(aeParams);

            ts.Complete();
        }

        private List<EntidadEstructuraDto> ConsultarArbol(string clave, string nombreEntidad)
        {
            var entidad = entidadRepository.ConsultarPorClave(clave);
            if (entidad == null)
            {
                throw new CdisException($"No se encontró la entidad {nombreEntidad} con clave {clave}");
            }

            var arbolPadecimientos = entidadEstructuraService.ConsultarArbol(entidad.IdEntidad);
            return arbolPadecimientos.ToList();
        }

        private void AgregarNuevasMuestras(ActualizacionExpedienteParams aeParams)
        {
            var seccionesPadecimientos = aeParams.ArbolPadecimientos
                .SelectMany(padecimiento => padecimiento.Hijos.Select(h => h.IdSeccion))
                .Distinct();

            var pestanaMuestras = BuscarPestanaMuestrasEnExpediente(aeParams.ArbolExpediente);

            var nuevasMuestras = seccionesPadecimientos
                .Except(pestanaMuestras.Hijos.Select(e => e.IdSeccion))
                .Select(m => new EntidadEstructuraDto
                {
                    IdEntidad = pestanaMuestras.IdEntidad,
                    Nombre = null,
                    Clave = null,
                    Tabulacion = null,
                    IdSeccion = m,
                    IdEntidadEstructuraPadre = pestanaMuestras.IdEntidadEstructura,
                })
                .ToList();

            entidadEstructuraService.Agregar(nuevasMuestras);
        }

        private static EntidadEstructuraDto BuscarPestanaMuestrasEnExpediente(IEnumerable<EntidadEstructuraDto> arbolExpediente)
        {
            const string clavePestanaMuestras = "005";

            var pestanaMuestras = arbolExpediente
                .FirstOrDefault(e => e.Clave == clavePestanaMuestras);

            if (pestanaMuestras == null)
            {
                throw new CdisException("No se encontró la pestaña de muestras con clave " + clavePestanaMuestras + " en la entidad Expediente Trackr");
            }

            return pestanaMuestras;
        }

        private void ActualizarPadecimientos(ActualizacionExpedienteParams aeParams)
        {
            ActualizarPadecimientosExistentes(aeParams);
            AgregarNuevosPadecimientos(aeParams);
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
