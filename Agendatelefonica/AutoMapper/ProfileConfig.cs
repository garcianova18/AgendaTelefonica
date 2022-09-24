using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.AutoMapper
{
    public class ProfileConfig : Profile
    {

        public ProfileConfig()
        {
            CreateMap<Mantenedore, MantenedoresView>().ReverseMap();
            CreateMap<Electromecanica, ElectromecanicaView>().ReverseMap();
            CreateMap<Estacione, EstacionesView>().ReverseMap();
            CreateMap<Usuario, UsuariosView>().ReverseMap();
        }
    }
}
