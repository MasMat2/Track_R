using TrackrAPI.Helpers;
using TrackrAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TrackrAPI.Dtos.GestionExpediente;
using TrackrAPI.Dtos.Seguridad;
using TrackrAPI.DTOs.Dashboard;
using TrackrAPI.Helpers;
using TrackrAPI.Models;
using System.Linq;

namespace TrackrAPI.Repositorys.Seguridad;

public class ConfirmacionCorreoRepository: Repository<ConfirmacionCorreo>, IConfirmacionCorreoRepository
{

    public ConfirmacionCorreoRepository(TrackrContext context) : base(context)
    {
        base.context = context;
    }

    public ConfirmacionCorreo ConsultarPorUsuario(int idUsuario)
    {
        var fechaActual = Utileria.ObtenerFechaActual();

        return context.ConfirmacionCorreo
            .Where(cc => cc.IdUsuario == idUsuario && cc.FechaAlta.Date == fechaActual.Date)
            .OrderByDescending(cc => cc.FechaAlta)
            .OrderByDescending(cc => cc.IdConfirmacionCorreo)
            .Select(cc => cc)
            .FirstOrDefault();
    }

}

