namespace TrackrAPI.Dtos.Contabilidad
{
    public class JerarquiaClaveDto
    {
        public int IdJerarquia { get; set; }
        public string Clave { get; set; }
        public decimal? Valor { get; set; }

        public JerarquiaClaveDto(int idJerarquia, string clave)
        {
            this.IdJerarquia = idJerarquia;
            this.Clave = clave;
        }

        public JerarquiaClaveDto(int hierarchyId, string code, decimal valor)
        {
            this.IdJerarquia = hierarchyId;
            this.Clave = code;
            this.Valor = valor;
        }

    }
}
