namespace TrackrAPI.Dtos.Catalogo
{
    public class TipoCuentaContableDto
    {

        public int IdTipoCuentaContable { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }


        public TipoCuentaContableDto(int idTipoCuentaContable, string clave, string nombre)
        {

            this.Clave = clave;
            this.Nombre = nombre;
            this.IdTipoCuentaContable = idTipoCuentaContable;

        }
    }
}
