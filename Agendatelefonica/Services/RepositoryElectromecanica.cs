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
     

        public RepositoryElectromecanica( AgendatelefonicaContext context)
        {
            this.context = context;
           
        }

        
        public async Task<IEnumerable<Electromecanica>> GetAll()
        {
            var electromecanico = await context.Electromecanicas.ToListAsync();

            return electromecanico;
        }

        public async Task<Electromecanica> GetById(int? id)
        {
            var obtenerelectromecanico = await context.Electromecanicas.FindAsync(id);

            return obtenerelectromecanico;

        }
        public async Task<int> Create(Electromecanica electromecanica)
        {


            context.Add(electromecanica);
            await context.SaveChangesAsync();

            return 1;
        }
        public async Task<int> update(Electromecanica electromecanica)
        {
           

            context.Update(electromecanica);
            await context.SaveChangesAsync();

            return 2;
        }

        public async Task<int> Delete(Electromecanica electromecanica)
        {


            context.Remove(electromecanica);
            await context.SaveChangesAsync();

            return 1;


        }
    }
}
