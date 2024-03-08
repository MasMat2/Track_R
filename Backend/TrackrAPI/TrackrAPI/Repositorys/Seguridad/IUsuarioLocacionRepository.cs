using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.Models;
using System;
using System.Collections.Generic;

namespace TrackrAPI.Repositorys.Seguridad
{
    public interface IUsuarioLocacionRepository : IRepository<UsuarioLocacion>
    {
        UsuarioLocacion Consultar(int idUsuarioLocacion);
        UsuarioLocacion Consultar(int idUsuario, int idLocacion);
        IEnumerable<UsuarioLocacionDto> ConsultarPorUsuario(int idUsuario);
    }
}
