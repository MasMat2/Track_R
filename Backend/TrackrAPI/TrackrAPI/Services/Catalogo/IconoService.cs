using TrackrAPI.Models;
using TrackrAPI.Repositorys.Catalogo;
using System.Collections.Generic;

namespace TrackrAPI.Services.Catalogo
{
    public class IconoService
    {
        private IIconoRepository iconoRepository;

        public IconoService(IIconoRepository iconoRepository)
        {
            this.iconoRepository = iconoRepository;
        }

        public IEnumerable<Icono> ConsultarGeneral()
        {
            return iconoRepository.ConsultarGeneral();
        }

    }
}
