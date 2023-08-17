﻿using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Models;

namespace TrackrAPI.Repositorys.GestionExpediente
{
    public interface IExpedientePadecimientoRepository : IRepository<ExpedientePadecimiento>
    {
        public IEnumerable<ExpedientePadecimientoDTO> Consultar();
        public IEnumerable<ExpedientePadecimientoDTO> ConsultarPorUsuario(int idUsuario);
        public IEnumerable<ExpedientePadecimientoGridDTO> ConsultarParaGridPorUsuario(int idUsuario);
        public IEnumerable<ExpedientePadecimientoSelectorDTO> ConsultarParaSelector();
        public void EliminarPorExpediente(int idExpediente);
        public IEnumerable<PadecimientoFueraRangoDTO> ConsultarValoresFueraRango(int idPadecimiento, int idUsuario);

    }
}
