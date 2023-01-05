using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Agendatelefonica.Services;
using System.Collections;
using Microsoft.Extensions.Logging;

namespace Agendatelefonica.Controllers
{

    [Authorize]
    public class ElectromecanicaController : Controller
    {
       
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;
        private readonly IRepositoryGenerico<Electromecanica> repositoryGenerico;
        private readonly Ireportes _reportes;
        private readonly ISelectRol selectRoles;

        public ElectromecanicaController(AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext,IRepositoryGenerico<Electromecanica> repositoryGenerico,Ireportes reportes, ISelectRol selectRoles)
        {
           
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.repositoryGenerico = repositoryGenerico;
            _reportes = reportes;
            this.selectRoles = selectRoles;
         
           
        }
        public IActionResult Index()
        {
            ViewBag.roles = selectRoles.SelectRol();
            return View();
        }

        //==================Area de Listar todos ============================

        public async Task<IEnumerable< ElectromecanicaView>>Electromecanica(string electromecanico)
        {



            var electromecanica = await repositoryGenerico.GetAll();


            var mapElectromecanica = mapper.Map<IEnumerable<ElectromecanicaView>>(electromecanica);
         


            if (electromecanico != null)
            {
                mapElectromecanica = mapElectromecanica.Where(n => n.Nombre.Contains(electromecanico.Trim()) || n.Telefono.Contains(electromecanico.Trim()) || n.Correo.Contains(electromecanico.Trim()) || n.Extension.Contains(electromecanico.Trim()) || n.Subsistema.Contains(electromecanico.Trim()));
            }

            return mapElectromecanica.OrderBy(e=>e.Subsistema);


        }

       

        //==================CRUD de electromecanica============================


        public async Task<ActionResult<int>>CrearActualizar([FromBody] ElectromecanicaView electromecanica)
        {
            //crear
            if (electromecanica.Id== 0)
            {
                if (ModelState.IsValid)
                {
                    var mapElectromecanica = mapper.Map<Electromecanica>(electromecanica);


                    var electromecanicaCreate = await repositoryGenerico.Create(mapElectromecanica);

                    await hubContext.Clients.All.SendAsync("recibir");

                    return electromecanicaCreate;

                }


            }
            else
            {
                //Actualizar

                if (ModelState.IsValid)
                {

                    var mapElectromecanica = mapper.Map<Electromecanica>(electromecanica);

                    var electromecanicaUpdate = await repositoryGenerico.update(mapElectromecanica);

                    await hubContext.Clients.All.SendAsync("recibir");

                    return electromecanicaUpdate;

                }

            }


            return 0;
        }

        
        public async Task<ActionResult<ElectromecanicaView>> BuscarElectromecanico(int? id)
        {


            if (id == null || id == 0)
            {
                return Ok(0);
            }

            var electromecanico = await repositoryGenerico.GetById(id);

            var mapElectromecanica = mapper.Map<ElectromecanicaView>(electromecanico);

            if (mapElectromecanica == null)
            {
                return Ok(0);
            }

            return mapElectromecanica;


        }



        public async Task<ActionResult<int>> EliminarElectromecanica(int? id)
        {


            if (id == null || id == 0)
            {
                return Ok(0);
            }

            var electromecanico = await repositoryGenerico.GetById(id);

            if (electromecanico == null)
            {
                return Ok(0);
            }

            var electromecanicaDelete = await repositoryGenerico.Delete(electromecanico);

            await hubContext.Clients.All.SendAsync("recibir");

            return electromecanicaDelete;



        }

        //Metodo para descargar reporte en excel
        public async Task<FileResult> Reportes()
        {

            

            return await _reportes.ReportesExcel();

        }

    }
}
