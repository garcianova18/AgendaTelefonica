using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DocumentFormat.OpenXml.InkML;
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
        Task<int> VerifivarExiste(UsuarioCreateView usuario);
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
            // se creo este metodo porque a qui se va a incluir la data relaciona
            // y en el metodo de repositoriogenerico no podemos incluirla
            var usuarios = await _context.Usuarios.Include(r=>r.IdRolNavigation).ToListAsync();


            return usuarios;
        }

        public async Task<int> VerifivarExiste(UsuarioCreateView usuario)
        {

            if (usuario.Id == 0)
            {
                 // si entra aqui se va averificar para crear un usuario
                return await _context.Usuarios.Where(u => u.UserName.Trim() == usuario.UserName.Trim()).CountAsync();
            }

            // si entra aqui se va a verificar para actualizar un usuario
            return await _context.Usuarios.Where(u => u.UserName.Trim() == usuario.UserName.Trim() && u.Id != usuario.Id).CountAsync();

        }
    }
}
