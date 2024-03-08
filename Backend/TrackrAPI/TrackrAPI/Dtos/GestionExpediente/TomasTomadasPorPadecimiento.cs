namespace TrackrAPI.Dtos.GestionExpediente;

public class TomasTomadasPorPadecimientoDto
{
    public int IdPadecimiento { get; set; } = 0;
    public List<TomasTomadasPorDiaDto> TomasTomadas { get; set; } = new List<TomasTomadasPorDiaDto>();
}

public class TomasTomadasPorDiaDto
{
    public byte Dia { get; set; } = 0;
    public int TomasTotales { get; set; } = 0;
    public int TomasTomadas { get; set; } = 0;
}