namespace TrackrAPI.Dtos.Catalogo
{
    public class EstadoSelectorDto
    {
        public int IdEstado { get; set; }
        public string Nombre { get; set; }

        public EstadoSelectorDto(int idEstado, string nombre)
        {
            this.IdEstado = idEstado;
            this.Nombre = nombre;
        }
    }
}
