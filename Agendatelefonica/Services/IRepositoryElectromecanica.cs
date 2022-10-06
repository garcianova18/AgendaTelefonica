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

        Task<IEnumerable<ElectromecanicaView>> GetAll();
        Task<int> Create(ElectromecanicaView electromecanica);
       
    }
}
