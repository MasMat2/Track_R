namespace TrackrAPI.Dtos.Catalogo
{
    public class SubtipoCuentaContableDto
    {
        public int IdSubtipoCuentaContable { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }

        public int IdTipoCuentaContable { get; set; }

        public SubtipoCuentaContableDto(int idSubtipoCuentaContable, string clave,string nombre, int idTipoCuentaContable)
        {
            this.IdSubtipoCuentaContable = idSubtipoCuentaContable;
            this.Clave = clave;
            this.Nombre = nombre;
            this.IdTipoCuentaContable = idTipoCuentaContable;

        }


    }
}
