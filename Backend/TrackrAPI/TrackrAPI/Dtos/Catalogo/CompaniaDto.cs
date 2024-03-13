using TrackrAPI.Models;

namespace TrackrAPI.Dtos.Catalogo
{
    public class CompaniaDto
    {
        public int IdCompania { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string PortalWeb { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public string Colonia { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string Ciudad { get; set; }
        public int? IdEstado { get; set; }
        public int? IdPais { get; set; }
        public string Rfc { get; set; }
        public int? IdLada { get; set; }
        public int? IdRegimenFiscal { get; set; }
        public int? IdAgrupadorCuentaContable { get; set; }
        public int? IdTipoCompania { get; set; }
        public int? IdMoneda { get; set; }
        public bool Existe { get; set; }
        public string Logotipo { get; set; }
        public string contrasenaUsuario { get; set; }
        public bool? AfectacionContable { get; set; }
        public string ClaveTipoCompania { get; set; }
        public bool? Timbrado { get; set; }
        public int? IdGiroComercial { get; set; }
        public CompaniaContacto CompaniaContacto { get; set; }
        public int? IdLocacionDefault { get; set; }
        public int? IdMunicipio { get; set; }
        public bool UsoAlmacen { get; set; }

        public CompaniaDto() { }
        public CompaniaDto(Compania compania)
        {

            if (compania == null)
            {
                return;
            }

            this.Existe = true;
        }
    }
}
