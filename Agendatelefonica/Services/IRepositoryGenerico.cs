using Agendatelefonica.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendatelefonica.Services
{
    public interface IRepositoryGenerico<T> where T : class
    {

        Task<IEnumerable<T>> GetAll();
        Task<int> Create(T Entidad);
        Task<int> update(T Entidad);
        Task<T> GetById(int? id);
        Task<int> Delete(T Entidad);
    }
}
