using Agendatelefonica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendatelefonica.ViewModels;

namespace Agendatelefonica.Services
{
   public interface IRepositoryElectromecanica
    {

        Task<IEnumerable<Electromecanica>> GetAll();
        Task<int> Create(Electromecanica electromecanica);
        Task<int> update(Electromecanica electromecanica);
        Task<Electromecanica> GetById(int? id);
        Task<int> Delete(Electromecanica electromecanica);
    }
}
