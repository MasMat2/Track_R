﻿using Microsoft.EntityFrameworkCore;
using TrackrAPI.Dtos.Catalogo;
using TrackrAPI.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using TrackrAPI.Helpers;

namespace TrackrAPI.Repositorys.Catalogo
{
    public class CodigoPostalRepository : Repository<CodigoPostal>, ICodigoPostalRepository
    {
        private readonly string connectionString;
        public CodigoPostalRepository(TrackrContext context, IConfiguration configuration): base(context)
        {
            this.connectionString = configuration.GetConnectionString("DefaultConnection");
            base.context = context;
        }

        public CodigoPostal Consultar(int idCodigoPostal)
        {
            return context.CodigoPostal.Where(e => e.IdCodigoPostal == idCodigoPostal).FirstOrDefault();
        }

        public CodigoPostalDto ConsultarDto(int idCodigoPostal)
        {
            return context.CodigoPostal
                      .Include(e => e.IdMunicipioNavigation)
                      .Include(e => e.IdMunicipioNavigation)
                      .ThenInclude(e => e.IdEstadoNavigation)
                      .Where(e => e.IdCodigoPostal == idCodigoPostal)
                      .Select(e => new CodigoPostalDto
                      {
                          IdCodigoPostal = e.IdCodigoPostal,
                          CodigoPostal1 = e.CodigoPostal1,
                          Colonia = e.Colonia,
                          Municipio = e.IdMunicipioNavigation.Nombre,
                          IdMunicipio = e.IdMunicipio,
                          Estado = e.IdMunicipioNavigation.IdEstadoNavigation.Nombre,
                          IdEstado = e.IdMunicipioNavigation.IdEstado,
                          IdPais = e.IdMunicipioNavigation.IdEstadoNavigation.IdPais,
                      })
                      .FirstOrDefault();
        }

        public IEnumerable<CodigoPostalGridDto> ConsultarTodosParaGrid()
        {
            return context.CodigoPostal
                      .OrderBy(e => e.CodigoPostal1)
                      .Select(e => new CodigoPostalGridDto(
                           e.IdCodigoPostal,
                          e.CodigoPostal1,
                          e.Colonia,
                          e.IdMunicipioNavigation.Nombre,
                          e.IdMunicipio,
                          e.IdMunicipioNavigation.IdEstadoNavigation.Nombre))
                      .ToList();
        }

        public IEnumerable<CodigoPostal> ConsultarTodos()
        {
            return context.CodigoPostal;
        }

        public void EliminarSinDependencias(){
            var codigoPostal = context.CodigoPostal
                      .Where(e => !context.Colonia.Any(d => d.IdCodigoPostal == e.IdCodigoPostal))
                      .ToList();
            context.CodigoPostal.RemoveRange(codigoPostal);
        }


        public IEnumerable<CodigoPostalDto> ConsultarPorCodigoPostal(string codigoPostal)
        {
            return context.CodigoPostal
                      .Include(e => e.IdMunicipioNavigation)
                      .Where(e => e.CodigoPostal1 == codigoPostal)
                      .Select(e => new CodigoPostalDto
                      {
                          IdCodigoPostal = e.IdCodigoPostal,
                          CodigoPostal1 = e.CodigoPostal1,
                          Colonia = e.Colonia,
                          Municipio = e.IdMunicipioNavigation.Nombre,
                          IdMunicipio = e.IdMunicipio,
                          Estado = e.IdMunicipioNavigation.IdEstadoNavigation.Nombre,
                          IdEstado = e.IdMunicipioNavigation.IdEstado,
                          IdPais = e.IdMunicipioNavigation.IdEstadoNavigation.IdPais,
                      })
                      .ToList();
        }

        public IEnumerable<CodigoPostalDto> ConsultarPorMunicipio(int idMunicipio)
        {
            return context.CodigoPostal
                      .Where(e => e.IdMunicipio == idMunicipio)
                      .Select(e => new CodigoPostalDto(
                          e.IdCodigoPostal,
                          e.CodigoPostal1,
                          e.Colonia,
                          e.IdMunicipioNavigation.Nombre,
                          e.IdMunicipio,
                          e.IdMunicipioNavigation.IdEstadoNavigation.Nombre))
                      .ToList();
        }

        public IEnumerable<CodigoPostalDto> ConsultarPorPaisBusqueda(string codigoPostal, int idPais)
        {
            return context.CodigoPostal
                      .Where(e => e.CodigoPostal1.StartsWith(codigoPostal)
                      && e.IdMunicipioNavigation.IdEstadoNavigation.IdPais == idPais)
                      .GroupBy(e => e.CodigoPostal1)
                      .Select(e => new CodigoPostalDto
                      {
                          CodigoPostal1 = e.Key
                      })
                      .ToList();
        }

        public CodigoPostal ConsultarPorColonia(string colonia)
        {
            return context.CodigoPostal
                      .Where(e => e.Colonia == colonia)
                      .FirstOrDefault();
        }

public void BulkInsert(List<CodigoPostal> codigoPostalList)
    {
        const int batchSize = 10000; // Tamaño del lote
        var totalBatches = (int)Math.Ceiling((double)codigoPostalList.Count / batchSize);

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int batch = 0; batch < totalBatches; batch++)
                {
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        // Especificar el esquema y el nombre de la tabla
                        bulkCopy.DestinationTableName = "Configuracion.CodigoPostal";

                        // Aumentar el tiempo de espera
                        bulkCopy.BulkCopyTimeout = 600; // Tiempo de espera en segundos (10 minutos)

                        // Mapear las columnas
                        bulkCopy.ColumnMappings.Add("CodigoPostal1", "CodigoPostal");
                        bulkCopy.ColumnMappings.Add("Colonia", "Colonia");
                        bulkCopy.ColumnMappings.Add("IdMunicipio", "IdMunicipio");

                        // Crear DataTable a partir del lote
                        var table = new DataTable();
                        table.Columns.Add("CodigoPostal1", typeof(string));
                        table.Columns.Add("Colonia", typeof(string));
                        table.Columns.Add("IdMunicipio", typeof(int));

                        var batchItems = codigoPostalList.Skip(batch * batchSize).Take(batchSize);
                        foreach (var codigoPostal in batchItems)
                        {
                            table.Rows.Add(codigoPostal.CodigoPostal1, codigoPostal.Colonia, codigoPostal.IdMunicipio);
                        }

                        // Realizar el bulk insert
                        bulkCopy.WriteToServer(table);
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Manejo de excepciones SQL
            throw new CdisException($"Error al realizar el bulk insert: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Manejo de excepciones generales
            throw new CdisException($"Error inesperado: {ex.Message}");
        }
    }
}

}
