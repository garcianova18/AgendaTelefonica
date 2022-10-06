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
            var electromecanico = mapper.Map<Electromecanica>(electromecanica);

            context.Add(electromecanico);

            await context.SaveChangesAsync();

            return 1;
        }


        public async Task<IEnumerable<ElectromecanicaView>> GetAll()
        {
            var electromecanico = await context.Electromecanicas.ToListAsync();

            var mapElectromecanico = mapper.Map<List<ElectromecanicaView>>(electromecanico);

               
            return mapElectromecanico;
        }

       
    }
}
