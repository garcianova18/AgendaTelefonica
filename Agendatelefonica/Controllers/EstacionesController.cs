using Agendatelefonica.Models;
using Agendatelefonica.Services;
using Agendatelefonica.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Agendatelefonica.Controllers
{
    public class EstacionesController : Controller
    {

        
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;
        private readonly IRepositoryGenerico<Estacione> repositoryGenerico; 
        public EstacionesController( IMapper mapper, IHubContext<agendaHub> hubContext,IRepositoryGenerico<Estacione> repositoryGenerico)
        {
           
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.repositoryGenerico = repositoryGenerico;
        }
      


        public async Task<ActionResult<int>> CrearEditarEstaciones([FromBody] EstacionesView estacionesView)
        {
            if (estacionesView.Id == 0)
            {
                //Crear

                if (ModelState.IsValid)
                {

                    var mapEstacion = mapper.Map<Estacione>(estacionesView);


                   var estaconCreate = await repositoryGenerico.Create(mapEstacion);
                    
                    await hubContext.Clients.All.SendAsync("recibir");



                    return estaconCreate;
                }

            }
            else
            {
                //Editar


                if (ModelState.IsValid)
                {
                    var mapEstacion = mapper.Map<Estacione>(estacionesView);


                    var estacionUpdate =await repositoryGenerico.update(mapEstacion);
                   
                    await hubContext.Clients.All.SendAsync("recibir");

                    return estacionUpdate;
                }
            }


            return 0;
        }

        public async Task<ActionResult<EstacionesView>> buscarEstacion(int? id)
        {

            if (id == 0 || id == null)
            {
                return Ok(0);
            }
            var estacion = await repositoryGenerico.GetById(id);

            if (estacion == null)
            {
                return Ok(0);
            }

            var mapEstacion = mapper.Map<EstacionesView>(estacion);

            return mapEstacion;

        }


        public async Task<int> EliminarEstaciones(int? id)
        {
            if (id == 0 || id == null)
            {
                return 0;
            }

            var estacion = await repositoryGenerico.GetById(id);


            if (estacion == null)
            {
                return 0;
            }

            var estacionDelete = await repositoryGenerico.Delete(estacion);

            await hubContext.Clients.All.SendAsync("recibir");

            return estacionDelete;
        }
    }
}
