    using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.SignalR;

namespace Agendatelefonica.Controllers
{
    [Authorize]
    public class MantenedoresController : Controller
    {
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;

        public MantenedoresController(AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
        }
        public IActionResult Index()
        {

            return View();
        }


        public async Task<JsonResult> BuscarMantenedor(int? id)
        {


            if (id == null || id ==0)
            {
                return Json(0);
            }

            var mantenedor = await context.Mantenedores.FindAsync(id);

            if (mantenedor == null)
            {
                return Json(0);
            }

            return Json(mantenedor);



        }

        public async Task<JsonResult> EliminarMantenedor(int? id)
        {


            if (id == null ||id ==0)
            {
                return Json(0);
            }

            var mantenedor = await context.Mantenedores.FindAsync(id);

            if (mantenedor == null)
            {
                return Json(0);
            }

            context.Remove(mantenedor);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("recibir"); ;

            return Json(1);



        }

        public async Task<IActionResult> CrearActualizar([FromBody] MantenedoresView mantenedores)
        {
            //crear
            if (mantenedores.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    // para obtener los mensajes de error que colocamos en el modelview para si queremos mostrarlo en el fronted esto es bueno cuando trabajamos con peticiones asincrona
                    var query = (from Estados in ModelState.Values
                                 from errores in Estados.Errors
                                 select errores.ErrorMessage).ToList();

                    var mapperMantenedores = mapper.Map<Mantenedore>(mantenedores);
                    
                    context.Add(mapperMantenedores);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");

                    return Ok(1);

                }


            }
            else
            {

                if (ModelState.IsValid)
                {

                    var mapperMantenedores = mapper.Map<Mantenedore>(mantenedores);

                    context.Update(mapperMantenedores);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");

                    return Ok(2);

                }

            }





            return Ok(0);
        }






    }
}
