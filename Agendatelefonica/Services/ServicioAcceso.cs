using Agendatelefonica.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.Services
{

    public interface IServicioAcceso
    {
        Task<Usuario> BuscarUsuario(Usuario usuario);
    }
    public class ServicioAcceso: IServicioAcceso
    {
        private readonly AgendatelefonicaContext _context;

        public ServicioAcceso(AgendatelefonicaContext context)
        {
            _context = context;
        }
        public async Task<Usuario> BuscarUsuario(Usuario usuario)
        {
           return await _context.Usuarios.Include(R=> R.IdRolNavigation).Where(u => u.UserName == usuario.UserName 
                                                                    && u.Password == usuario.Password).FirstOrDefaultAsync();

        }
    }
}
