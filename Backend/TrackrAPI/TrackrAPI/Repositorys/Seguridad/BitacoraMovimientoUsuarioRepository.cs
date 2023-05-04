using CanalDistAPI.Dtos.Seguridad;
using Microsoft.EntityFrameworkCore;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace TrackrAPI.Repositorys.Seguridad
{
    public class BitacoraMovimientoUsuarioRepository : Repository<BitacoraMovimientoUsuario>, IBitacoraMovimientoUsuarioRepository
    {
        public BitacoraMovimientoUsuarioRepository(TrackrContext context) : base(context)
        {
            base.context = context;
        }

        /// <summary>
        /// Consulta la tabla BitacoraMovimientoUsuario con los filtros indicados
        /// Que esté dentro de las fechaInicio y fechaFin. Que sea en la locación seleccionada.
        /// Que sea por el usuario seleccionado
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Lista de BitacoraMovimientoUsuario</returns>
        public IEnumerable<BitacoraMovimientoUsuario> consultarFiltroParaPdf(BitacoraMovimientoUsuarioFiltroDto filtro)
        {
            return context.BitacoraMovimientoUsuario
                .Include(bmu => bmu.IdUsuarioAltaNavigation)
                .Where(bmu =>
                (filtro.fechaInicio == null || bmu.FechaAlta.Date >= ((DateTime)filtro.fechaInicio).Date)
                && (filtro.fechaFin == null || bmu.FechaAlta.Date <= ((DateTime)filtro.fechaFin).Date)
                && (filtro.IdLocacion == null || bmu.IdLocacion == filtro.IdLocacion)
                && (filtro.IdUsuario == null || bmu.IdUsuarioAlta == filtro.IdUsuario))
                .Select(bmu=> new BitacoraMovimientoUsuario
                {
                    FechaAlta = bmu.FechaAlta,
                    IdUsuarioAltaNavigation = bmu.IdUsuarioAltaNavigation,
                    Tipo= bmu.Tipo,
                    Descripcion= bmu.Descripcion,
                })
                .OrderBy(bmu => bmu.FechaAlta);
        }
    }
}
