namespace TrackrAPI.Dtos.Catalogo
{
    public class ColoniaDto
    {
          public int IdMunicipio { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Clave { get; set; } = string.Empty;   
            public string CodigoPostal { get; set; } = string.Empty;
            public string ClaveMunicipio { get; set; } = string.Empty;
            public string NombreMunicipio { get; set; } = string.Empty;
            public string ClaveEstado { get; set; } = string.Empty;
    }
}