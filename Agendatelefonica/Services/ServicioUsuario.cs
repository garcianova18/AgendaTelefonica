using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.Services
{
    public interface IServicioUsuario
    {
        Task<IEnumerable<Usuario>> Usuarios();
    }
    public class ServicioUsuario: IServicioUsuario
    {
        private readonly AgendatelefonicaContext _context;
     
        

        public ServicioUsuario(AgendatelefonicaContext context)
        {
            _context= context;
        
           
        }

        public async Task<IEnumerable<Usuario>> Usuarios()
        {
            var usuarios = await _context.Usuarios.Include(r=>r.IdRolNavigation).ToListAsync();




            return usuarios;
        }
    }
}
