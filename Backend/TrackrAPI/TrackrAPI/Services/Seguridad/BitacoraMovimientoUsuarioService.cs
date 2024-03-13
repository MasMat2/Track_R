using CanalDistAPI.Dtos.Seguridad;
using DinkToPdf;
using DinkToPdf.Contracts;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using TrackrAPI.Repositorys.Seguridad;

namespace TrackrAPI.Services.Seguridad
{
    public class BitacoraMovimientoUsuarioService
    {
        private IBitacoraMovimientoUsuarioRepository bitacoraMovimientoUsuarioRepository;
        private IConverter converter;
        private IUsuarioRepository usuarioRepository;
        private AutenticacionService autenticacionService;

        public BitacoraMovimientoUsuarioService(
            IBitacoraMovimientoUsuarioRepository bitacoraMovimientoUsuarioRepository,
            IConverter converter,
            IUsuarioRepository usuarioRepository,
            AutenticacionService autenticacionService
            )
        {
            this.bitacoraMovimientoUsuarioRepository = bitacoraMovimientoUsuarioRepository;
            this.converter = converter;
            this.usuarioRepository= usuarioRepository;
            this.autenticacionService = autenticacionService;
        }
        /// <summary>
        /// Agrega un registro a la tabla BitacoraMovimientoUsuario
        /// checa si se tiene el id del usuario en sesión, si no se tiene
        /// utiliza el método de autenticacionService para obtenerlo
        /// </summary>
        /// <param name="tipo">String que representa el tipo de registro en la bitácora</param>
        /// <param name="descripcion">String que describe con mayor precisión el registro de la bitácora</param>
        /// <param name="idUsuarioAlta">Id del usuario en sesión que crea el registro en la bitácora</param>
        public void Agregar(string tipo, string descripcion, int? idUsuarioAlta)
        {
            if(idUsuarioAlta == null)
            {
                idUsuarioAlta = autenticacionService.ObtenerIdUsuarioSesion();
            }
            var usuario = usuarioRepository.Consultar((int)idUsuarioAlta);
            BitacoraMovimientoUsuario bitacoraMovimientoUsuario = new BitacoraMovimientoUsuario();
            bitacoraMovimientoUsuario.Descripcion = descripcion;
            bitacoraMovimientoUsuario.Tipo = tipo;
            bitacoraMovimientoUsuario.FechaAlta = Utileria.ObtenerFechaActual();
            bitacoraMovimientoUsuario.IdUsuarioAlta = (int)idUsuarioAlta;
            bitacoraMovimientoUsuario.IdLocacion = (int)usuario.IdHospital;
            bitacoraMovimientoUsuarioRepository.Agregar(bitacoraMovimientoUsuario);
        }

        /// <summary>
        /// Genera un archivo PDF con la bitacora de movimientos de usuario
        /// Primero utiliza el metodo ObtenerEstilos() para obtener los estilos del archivo css
        /// Después utiliza el método GenerarPDF para la consulta de los datos y la generación del html
        /// </summary>
        /// <param name="filtro">
        /// Atributos del filtro: IdUsuario, IdLocacion, FechaInicio, FechaFin
        /// utilizados en la consulta a la BD
        /// </param>
        /// <returns>
        /// Byte Array en formato PDF
        /// </returns>
        public byte[] DescargarPdfPorFiltro(BitacoraMovimientoUsuarioFiltroDto filtro)
        {
            string nombreArchivo = "Bitacora de Movimientos de Usuario.pdf";

            string html = ObtenerEstilos();

            html += GenerarPDF(filtro);

            var globalSettings = new GlobalSettings
            {
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.Letter,
                DocumentTitle = nombreArchivo
            };

            var objectSettings = new ObjectSettings
            {
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8" }
            };

            var pdfFile = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            return converter.Convert(pdfFile);
        }

        /// <summary>
        /// Consulta la lista de movimientos de la tabla BitacoraMovimientoUsuario que correspondan
        /// al filtro. Después por cada fecha genera una tabla, para incorporar cada registro obtenido
        /// en la tabla
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public string GenerarPDF(BitacoraMovimientoUsuarioFiltroDto filtro)
        {
            string renglones = "";
            var bitacoraMovimientoUsuarioList = bitacoraMovimientoUsuarioRepository.consultarFiltroParaPdf(filtro).ToList();

            DateTime? fechaAnterior = null;

            for (int i = 0; i < bitacoraMovimientoUsuarioList.Count; i++)
            {
                bool nuevaTabla = false;

                if (fechaAnterior == null || ((DateTime)fechaAnterior).Date != bitacoraMovimientoUsuarioList[i].FechaAlta.Date)
                {
                    nuevaTabla = true;
                }

                if (nuevaTabla)
                {
                    if (fechaAnterior != null)
                    {
                        renglones += "</table>";
                    }

                    renglones +=
                    "<div class='text' style='padding: 5px 20px; text-align: left; font-size: 14px'>" +
                     "<strong style='text-align:center; font-size: 14px;'>Del día " + bitacoraMovimientoUsuarioList[i].FechaAlta.ToString("dd/MM/yyyy") + "</strong>" +
                     "</div>" +
                     "<table class='grid center table table-striped' style='width: 95%;'>" +
                       "<tr>" +
                         "<th style='width: 15%'><strong>Hora</strong></th>" +
                         "<th style='width: 30%'><strong>Usuario</strong></th>" +
                         "<th style='width: 15%'><strong>Tipo</strong></th>" +
                         "<th style='width: 40%'><strong>Descripción</strong></th>" +
                       "</tr>";
                }

                renglones +=
                   "<tr>" +
                       "<td>" + bitacoraMovimientoUsuarioList[i].FechaAlta.FormatoHora() + "</td>" +
                       "<td>" + bitacoraMovimientoUsuarioList[i].IdUsuarioAltaNavigation.ObtenerNombreCompleto() + "</td>" +
                       "<td>" + bitacoraMovimientoUsuarioList[i].Tipo + "</td>" +
                       "<td>" + bitacoraMovimientoUsuarioList[i].Descripcion + "</td>" +
                   "</tr>";

                if ((i + 1) == bitacoraMovimientoUsuarioList.Count)
                {
                    renglones += "</table>";
                }

                fechaAnterior = bitacoraMovimientoUsuarioList[i].FechaAlta;
            }

            return
                "<div class='body'>" +
                  "<div class='text' style='padding: 20px 15px; text-align: center; font-size: 18px'>" +
                    "<strong>Historial de Movimientos</strong>" +
                  "</div>" +

                        renglones +
                  "</table>" +
                "</div>";
        }
        /// <summary>
        /// Es el string de CSS, solo está aquí para que no se confunda
        /// </summary>
        /// <returns>Estilo de la tabla</returns>

        private string ObtenerEstilos()
        {
            return @"
            <style>
                html {
                    font-family: Helvetica;
                    font-size: 12px;
                }

                  .logo {
                      width: 150px;
                      padding: 15px 0 0 50px;

                }

                  .header {
                      width: 100%;

                }

                  .center {
                      margin: 0 auto;

                }

                  .text {
                      font-size: 13px
                }

                  .item {
                      display: inline-block;

                }

                  .table {
                      width: 100%;
                      margin-bottom: 1rem;
                      color: #212529;
                       border-collapse: collapse;

                }

                  .table th,
                 .table td {
                      padding: 0.75rem;
                      vertical-align: top;
                       font-weight: normal;
                       border-bottom: 1px solid rgb(222, 226, 230);
                      border-top: 1px solid \trgb(200, 200, 200);

                }

                  .table-border-top {
                      border-top: 1px solid \trgb(200, 200, 200) !important;

                }

                  .table thead th {
                      vertical-align: bottom;

                }

                    .table td {
                      border: 0;

                }

                   body {
                      width: 100%;
                      margin: 0;

                }

                .grid {
                    font-size: 12px;
                    border-collapse: collapse;
                    width: 100%;
                    margin-top: 10px;
                    margin-bottom: 25px;
                }

                .grid tr:first-child td {
                    border-top: 1px #c7c7c7 solid;
                    border-bottom: 1px #c7c7c7 solid;
                }

                .grid td,
                .grid th {
                    text-align: left;
                    padding: 4px 8px;
                }

                .grid tr:nth-child(even) {}

                  .grid-totales {
                    margin: 0 auto;
                }

                .grid-totales td,
                .grid-totales th {
                    padding: 4px 8px;
                }

                .grid-totales tr td:nth-child(3n+3) {
                    text-align: right;
                }
            </style>";
        }
    }
}
