using Agendatelefonica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendatelefonica.AutoMapper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Agendatelefonica.ViewModels;

namespace Agendatelefonica.Services
{
    public class RepositoryElectromecanica : IRepositoryElectromecanica
    {
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;

        public RepositoryElectromecanica( AgendatelefonicaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<int> Create(ElectromecanicaView electromecanica)
        {
            var mapElectromecanica = mapper.Map<Electromecanica>(electromecanica);

            context.Add(mapElectromecanica);
            await context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Delete(Electromecanica electromecanica )
        {
         
       
            context.Remove(electromecanica);
            await context.SaveChangesAsync();

            return 1;

           
        }

 

        public async Task<IEnumerable<ElectromecanicaView>> GetAll()
        {
            var electromecanico = await context.Electromecanicas.ToListAsync();

            var mapElectromecanico = mapper.Map<List<ElectromecanicaView>>(electromecanico);


            return mapElectromecanico;
        }

        public async Task<ElectromecanicaView> GetById(int? id)
        {
            var obtenerelectromecanico = await context.Electromecanicas.FindAsync(id);

            var mapElectromecanica = mapper.Map<ElectromecanicaView>(obtenerelectromecanico);

            return mapElectromecanica;

        }

        public async Task<int> update(ElectromecanicaView electromecanica)
        {
           var mapElectromecanica = mapper.Map<Electromecanica>(electromecanica);

            context.Update(mapElectromecanica);
            await context.SaveChangesAsync();

            return 2;
        }
    }
}
