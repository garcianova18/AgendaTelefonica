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
using Agendatelefonica.Services;
using System.Collections;

namespace Agendatelefonica.Controllers
{
    [Authorize]
    public class MantenedoresController : Controller
    {
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;
        private readonly IRepositoryGenerico<Mantenedore> repositoryGenerico;

        public MantenedoresController(AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext, IRepositoryGenerico<Mantenedore> repositoryGenerico)
        {
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.repositoryGenerico = repositoryGenerico;
        }


        public async Task<IEnumerable> Mantenedores(string mantenedor)
        {

            var mantenedores = await repositoryGenerico.GetAll();

            var MapMantenedores = mapper.Map<IEnumerable<MantenedoresView>>(mantenedores);


            if (mantenedor != null)
            {
                MapMantenedores = MapMantenedores.Where(n => n.Mantenedor.Contains(mantenedor.Trim()) || n.Nombre.Contains(mantenedor.Trim()) || n.Funcion.Contains(mantenedor.Trim()) || n.Telefono.Contains(mantenedor.Trim()) || n.Subsistema.Contains(mantenedor.Trim()));
            }



            return mantenedores.OrderBy(m => m.Mantenedor);
        }




        public async Task<ActionResult<MantenedoresView>>BuscarMantenedor(int? id)
        {


            if (id == null || id ==0)
            {
                return Json(0);
            }

            var mantenedor = await repositoryGenerico.GetById(id);

            if (mantenedor == null)
            {
                return Json(0);
            }

            var mapMantenedor = mapper.Map<MantenedoresView>(mantenedor);

            return mapMantenedor;



        }

        public async Task<ActionResult<int>> EliminarMantenedor(int? id)
        {


            if (id == null ||id ==0)
            {
                return Json(0);
            }

            var mantenedor = await repositoryGenerico.GetById(id);

            if (mantenedor == null)
            {
                return Json(0);
            }
            

            var mantendorDelete =  await repositoryGenerico.Delete(mantenedor);
            await hubContext.Clients.All.SendAsync("recibir"); ;

            return mantendorDelete;



        }

        public async Task<ActionResult<int>> CrearActualizar([FromBody] MantenedoresView mantenedores)
        {
            //Crear
            if (mantenedores.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    // para obtener los mensajes de error que colocamos en el modelview para si queremos mostrarlo en el fronted esto es bueno cuando trabajamos con peticiones asincrona
                    //var query = (from Estados in ModelState.Values
                    //             from errores in Estados.Errors
                    //             select errores.ErrorMessage).ToList();

                    var mapMantenedores = mapper.Map<Mantenedore>(mantenedores);


                    var mantenedorCreate = await repositoryGenerico.Create(mapMantenedores);
                    
                   
                    await hubContext.Clients.All.SendAsync("recibir");

                    return mantenedorCreate;

                }


            }

            //Actualizar
            else
            {

                if (ModelState.IsValid)
                {

                    var mapMantenedores = mapper.Map<Mantenedore>(mantenedores);

                    var mantenedorUpdate = await repositoryGenerico.update(mapMantenedores);

                    
                    await hubContext.Clients.All.SendAsync("recibir");

                    return mantenedorUpdate;

                }

            }


            return 0;
        }



    }
}
