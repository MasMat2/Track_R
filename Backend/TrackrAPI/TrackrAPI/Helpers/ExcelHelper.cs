using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace TrackrAPI.Helpers
{
    public class ExcelHelper
    {
        public static XLWorkbook ObtenerWorkbook(string archivoBase64)
        {
            if (archivoBase64 == null)
                return null;

            int index = archivoBase64.LastIndexOf(',') + 1;

            if (index != -1)
                archivoBase64 = archivoBase64[(index)..];

            byte[] bytes = Convert.FromBase64String(archivoBase64);
            Stream file = new StreamContent(new MemoryStream(bytes)).ReadAsStream();

            return new XLWorkbook(file);
        }
        public static IXLWorksheet ObtenerWorksheet(XLWorkbook workbook, string nombreWorksheet)
        {
            try
            {
                return workbook.Worksheet(nombreWorksheet);
            }
            catch (ArgumentException)
            {
                throw new CdisException($"No se encontro la hoja de cálculo \"{nombreWorksheet}\".");
            }
        }

        public static void ValidarHeaders(IXLRangeRow headerRow, List<string> columnas)
        {
            List<string> missingColumns = new();

            foreach (string headerName in columnas)
            {
                IXLCell cell = headerRow.Cells()
                    .Where(c => FormatearHeader(c.Value.ToString()) == headerName.ToLower())
                    .FirstOrDefault();

                if (cell == null)
                    missingColumns.Add(headerName);
            }

            if (missingColumns.Count > 0)
            {
                string error = missingColumns.Count == 1
                    ? "No se encontró la columna " + missingColumns[0]
                    : "No se encontraron las columnas: " + string.Join(", ", missingColumns);
                throw new CdisException(error);
            }
        }

        public static int ObtenerIndiceColumna(string columnName, IXLRangeRow headerRow)
        {
            foreach (IXLCell cell in headerRow.Cells())
            {
                string headerName = FormatearHeader(cell.Value.ToString());

                if (columnName.ToLower() == headerName)
                {
                    return cell.Address.ColumnNumber;
                }
            }

            return -1;
        }

        public static string ObtenerValorCelda(IXLRangeRow row, int columna, string header, bool esRequerido)
        {
            string valor = row.Cell(columna).GetValue<string>();

            if (esRequerido && string.IsNullOrWhiteSpace(valor))
                throw new CdisException($"El campo \"{header}\" es requerido.");

            return valor;
        }

        public static bool CastBoolean(string header, string str)
        {
            string unformatedStr = str.RemoveDiacritics().ToLower();

            if (unformatedStr.Equals("si"))
                return true;
            else if (unformatedStr.Equals("no"))
                return false;
            else
                throw new CdisException($"Sólo se permiten los valores 'Si' / 'No' en la columna {header}");
        }

        private static string FormatearHeader(string header)
        {
            return header.Replace("*", "").Trim().ToLower();
        }
    }
}