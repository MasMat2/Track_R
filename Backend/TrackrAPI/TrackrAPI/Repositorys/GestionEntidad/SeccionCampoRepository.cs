﻿using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.GestionEntidad;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace TrackrAPI.Repositorys.GestionEntidad
{
    public class SeccionCampoRepository : Repository<SeccionCampo>, ISeccionCampoRepository
    {
        public SeccionCampoRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        public SeccionCampo? Consultar(int idSeccionCampo)
        {
            return context.SeccionCampo.Where(e => e.IdSeccionCampo == idSeccionCampo).ToList().FirstOrDefault();
        }

        public SeccionCampo? Consultar(string descripcion)
        {
            return context.SeccionCampo
                .Where(e => e.Descripcion == descripcion)
                .FirstOrDefault();
        }

        public SeccionCampo? ConsultarPorClave(string clave)
        {
            return context.SeccionCampo
                .Where(e => e.Clave == clave)
                .FirstOrDefault();
        }

        public SeccionCampo? ConsultarDuplicado(
            int orden,
            string? grupo,
            string clave,
            int idSeccion)
        {
            return context.SeccionCampo
                .Where(s => (s.Orden == orden &&
                    s.Grupo == grupo &&
                    s.IdSeccion == idSeccion) ||
                    s.Clave == clave)
                .FirstOrDefault();
        }

        public IEnumerable<SeccionCampoDto> ConsultarPorSeccion(int idSeccion)
        {
            var secciones =
                from es in context.SeccionCampo
                where es.IdSeccion == idSeccion
                orderby es.Orden ascending
                select new SeccionCampoDto
                {
                    Clave = es.Clave,
                    IdSeccionCampo = es.IdSeccionCampo,
                    IdSeccion = es.IdSeccion,
                    IdDominio = es.IdDominio,
                    Descripcion = es.Descripcion,
                    TamanoColumna = es.TamanoColumna,
                    Orden = es.Orden,
                    LongitudMaxima = es.IdDominioNavigation.LongitudMaxima,
                    TipoCampo = es.IdDominioNavigation.TipoCampo,
                    TipoDato = es.IdDominioNavigation.TipoDato,
                    ListaOpciones = es.IdDominioNavigation.DominioDetalle,
                    Requerido = es.Requerido,
                    FechaMinima = es.IdDominioNavigation.FechaMinima,
                    FechaMaxima = es.IdDominioNavigation.FechaMaxima,
                    ValorMinimo = es.IdDominioNavigation.ValorMinimo,
                    ValorMaximo = es.IdDominioNavigation.ValorMaximo,
                    Deshabilitado = es.Deshabilitado,
                    ClaveSeccion = es.IdSeccionNavigation.Clave ?? "",
                    NombreDominio = es.IdDominioNavigation.Nombre,
                    Grupo = es.Grupo,
                    Fila = es.Fila,
                };
            return secciones;
        }

        public IEnumerable<ExpedienteColumnaDTO> ConsultarSeccionesPadecimientos(int idPadecimiento)
        {
            return context.SeccionCampo
                .Where(sc => sc.IdSeccionNavigation.EntidadEstructura
                    .Any(e => e.IdEntidadEstructuraPadre == idPadecimiento))
                .Select(sc =>
                    new ExpedienteColumnaDTO {
                        Parametro = sc.Descripcion,
                        ClaveCampo = "ME-" + sc.Clave,
                        ClaveSeccion = sc.IdSeccionNavigation.Clave,
                        Variable = sc.IdSeccionNavigation.Nombre,
                        ValorMinimo = sc.IdDominioNavigation.ValorMinimo,
                        ValorMaximo = sc.IdDominioNavigation.ValorMaximo
                    });
        }

        public IEnumerable<ExpedienteColumnaDTO> ConsultarSeccionesPadecimientosGeneral()
        {
            return context.SeccionCampo
                .Select(sc =>
                    new ExpedienteColumnaDTO
                    {
                        Parametro = sc.Descripcion,
                        ClaveCampo = "ME-" + sc.Clave,
                        Variable = sc.IdSeccionNavigation.Nombre,
                        ValorMinimo = sc.IdDominioNavigation.ValorMinimo,
                        ValorMaximo = sc.IdDominioNavigation.ValorMaximo
                    });
        }
    }
}
