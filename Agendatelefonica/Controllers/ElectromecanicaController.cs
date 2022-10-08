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

namespace Agendatelefonica.Controllers
{

    [Authorize]
    public class ElectromecanicaController : Controller
    {
        private readonly IRepositoryElectromecanica repositoryElectromecanica;
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;
        

        public ElectromecanicaController(IRepositoryElectromecanica repositoryElectromecanica, AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext)
        {
            this.repositoryElectromecanica = repositoryElectromecanica;
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
           
        }
        public IActionResult Index()
        {
            ViewBag.roles = SelectRol();
            return View();
        }

        //==================Area de Listar todos ============================

        public async Task<IEnumerable< ElectromecanicaView>>Electromecanica(string electromecanico)
        {


            //para evitarnos estar mapeando las propiedades de Electromecanica a ElectromecanicaView usamos ProjectTo de automapper que lo hace automatico
            //var electromecanica = context.Electromecanicas.ProjectTo<ElectromecanicaView>(mapper.ConfigurationProvider);

            var electromecanica = await repositoryElectromecanica.GetAll();

            var mapElectromecanica = mapper.Map<IEnumerable<ElectromecanicaView>>(electromecanica);

            if (electromecanico != null)
            {
                mapElectromecanica = mapElectromecanica.Where(n => n.Nombre.Contains(electromecanico.Trim()) || n.Telefono.Contains(electromecanico.Trim()) || n.Correo.Contains(electromecanico.Trim()) || n.Extension.Contains(electromecanico.Trim()) || n.Subsistema.Contains(electromecanico.Trim()));
            }

            return mapElectromecanica;


        }

       
       

        public IEnumerable Mantenedores(string mantenedor)
        {
            var mantenedores = context.Mantenedores.ProjectTo<MantenedoresView>(mapper.ConfigurationProvider);


            if (mantenedor != null)
            {
                mantenedores = mantenedores.Where(n => n.Mantenedor.Contains(mantenedor.Trim()) || n.Nombre.Contains(mantenedor.Trim()) || n.Funcion.Contains(mantenedor.Trim()) || n.Telefono.Contains(mantenedor.Trim()) || n.Subsistema.Contains(mantenedor.Trim()));
            }



            return mantenedores.OrderBy(m => m.Mantenedor);
        }

        public JsonResult Estaciones(string estacion)
        {


            var estaciones = context.Estaciones.ProjectTo<EstacionesView>(mapper.ConfigurationProvider);


            if (estacion != null)
            {
                estaciones = estaciones.Where(n => n.Linea.Contains(estacion.Trim()) || n.Estacion.Contains(estacion.Trim()) || n.Boleteria.Contains(estacion.Trim()) || n.CuartoControl.Contains(estacion.Trim()) || n.CuartoCom.Contains(estacion.Trim()) || n.CuartoCom.Contains(estacion.Trim()) || n.CuartoCom.Contains(estacion.Trim()) || n.Enclavamiento.Contains(estacion.Trim()) || n.CuartoAtbt.Contains(estacion.Trim()) || n.SubestacionTraccion.Contains(estacion.Trim()) || n.CabinaAnden.Contains(estacion.Trim()) || n.PstnEmergencia.Contains(estacion.Trim()));
            }

            return Json(estaciones.OrderBy(L =>L.Linea));
        }


        public IActionResult Usuarios(string usuario)
        {
            var usuarios = context.Usuarios.ProjectTo<UsuariosView>(mapper.ConfigurationProvider);

            if (usuario != null)
            {
                usuarios = usuarios.Where(u => u.Nombre.Contains(usuario) || u.Apellido.Contains(usuario) || u.Codigo.Contains(usuario) || u.UserName.Contains(usuario));
            }


            return Json(usuarios.OrderByDescending(u=> u.Id));
        }


        public List<SelectListItem> SelectRol()
        {

            var roles = context.Rols.Select(r => new SelectListItem
            {

                Text = r.Nombre,
                Value = r.Id.ToString()

            }).ToList();

            roles.Insert(0, new SelectListItem
            {
                Text = "Seleccione un rol",
                Value = ""
            });


            return roles;



        }



        //==================CRUD de electromecanica============================


        public async Task< IActionResult >CrearActualizar([FromBody] ElectromecanicaView electromecanica)
        {
            //crear
            if (electromecanica.Id== 0)
            {
                if (ModelState.IsValid)
                {
                    var mapElectromecanica = mapper.Map<Electromecanica>(electromecanica);

                    var electromecanicaCreate = await repositoryElectromecanica.Create(mapElectromecanica);

                    await hubContext.Clients.All.SendAsync("recibir");

                    return Ok(electromecanicaCreate);

                }


            }
            else
            {
                //Actualizar

                if (ModelState.IsValid)
                {

                    var mapElectromecanica = mapper.Map<Electromecanica>(electromecanica);

                    var electromecanicaUpdate = await repositoryElectromecanica.update(mapElectromecanica);

                    await hubContext.Clients.All.SendAsync("recibir");

                    return Ok(electromecanicaUpdate);

                }

            }





            return Ok(0);
        }

        
        public async Task<ActionResult> BuscarElectromecanico(int? id)
        {


            if (id == null || id == 0)
            {
                return Ok(0);
            }


            var electromecanico = await repositoryElectromecanica.GetById(id);

            var mapElectromecanica = mapper.Map<ElectromecanicaView>(electromecanico);

            if (mapElectromecanica == null)
            {
                return Ok(0);
            }

            return Ok(mapElectromecanica);



        }



        public async Task<ActionResult> EliminarElectromecanica(int? id)
        {


            if (id == null || id == 0)
            {
                return Ok(0);
            }

            var electromecanico = await repositoryElectromecanica.GetById(id);

            if (electromecanico == null)
            {
                return Ok(0);
            }
          

            var electromecanicaDelete = await repositoryElectromecanica.Delete(electromecanico);
            
           
            await hubContext.Clients.All.SendAsync("recibir");

            return Ok(electromecanicaDelete);



        }

       
    }
}
