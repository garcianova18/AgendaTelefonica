using Agendatelefonica.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.Services
{
    public class RepositoryGenerico<T> : IRepositoryGenerico<T> where T : class
    {
        private readonly AgendatelefonicaContext _context;

        private DbSet<T> Entidades;
        public RepositoryGenerico(AgendatelefonicaContext context)
        {
            _context = context;
            Entidades= _context.Set<T>();
        }
        public async Task<int> Create(T Entidad)
        {
            
            Entidades.Add(Entidad);
            await _context.SaveChangesAsync();
           

            return 1;
        }

        public async Task<int> Delete(T Entidad)
        {
                
            Entidades.Remove(Entidad);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<IEnumerable<T>> GetAll()
        {

            return await Entidades.ToListAsync();

           
        }

        public async Task<T> GetById(int? id)
        {
            return await Entidades.FindAsync(id);

        }

        public async Task<int> update(T Entidad)
        {
             Entidades.Update(Entidad);
            await _context.SaveChangesAsync();

            return 2;
        }

       
    }
}
