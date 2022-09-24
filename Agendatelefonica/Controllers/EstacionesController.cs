using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.Controllers
{
    public class EstacionesController : Controller
    {

        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;

        public EstacionesController(AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CrearEditarEstaciones([FromBody] EstacionesView estacionesView)
        {
            if (estacionesView.Id == 0)
            {
                //Crear

                if (ModelState.IsValid)
                {

                    var estacion = mapper.Map<Estacione>(estacionesView);


                    
                    context.Add(estacion);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");



                    return Ok(1);
                }

            }
            else
            {
                //Editar


                if (ModelState.IsValid)
                {
                    var estacion = mapper.Map<Estacione>(estacionesView);


                    context.Update(estacion);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");
                    return Ok(2);
                }
            }


            return Ok(0);
        }

        public async Task<IActionResult> buscarEstacion(int? id)
        {

            if (id == 0 || id == null)
            {
                return Ok(0);
            }
            var buscarestacion = await context.Estaciones.FindAsync(id);

            if (buscarestacion == null)
            {
                return Ok(0);
            }

            return Json(buscarestacion);

        }


        public async Task<int> EliminarEstaciones(int? id)
        {
            if (id == 0 || id == null)
            {
                return 0;
            }

            var estacion = await context.Estaciones.FindAsync(id);


            if (estacion == null)
            {
                return 0;
            }

            context.Remove(estacion);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("recibir");

            return 1;
        }
    }
}
