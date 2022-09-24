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

namespace Agendatelefonica.Controllers
{

    [Authorize]
    public class ElectromecanicaController : Controller
    {
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;

        public ElectromecanicaController(AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext)
        {
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

        public JsonResult Electromecanica(string electromecanico)
        {


            //para evitarnos estar mapeando las propiedades de Electromecanica a ElectromecanicaView usamos ProjectTo de automapper que lo hace automatico
            var electromecanica = context.Electromecanicas.ProjectTo<ElectromecanicaView>(mapper.ConfigurationProvider);

            if (electromecanico != null)
            {
                electromecanica = electromecanica.Where(n => n.Nombre.Contains(electromecanico.Trim()) || n.Telefono.Contains(electromecanico.Trim()) || n.Correo.Contains(electromecanico.Trim()) || n.Extension.Contains(electromecanico.Trim()) || n.Subsistema.Contains(electromecanico.Trim()));
            }

            return Json(electromecanica.OrderBy(n => n.Nombre));


        }

        public JsonResult Mantenedores(string mantenedor)
        {
            var mantenedores = context.Mantenedores.ProjectTo<MantenedoresView>(mapper.ConfigurationProvider);


            if (mantenedor != null)
            {
                mantenedores = mantenedores.Where(n => n.Mantenedor.Contains(mantenedor.Trim()) || n.Nombre.Contains(mantenedor.Trim()) || n.Funcion.Contains(mantenedor.Trim()) || n.Telefono.Contains(mantenedor.Trim()) || n.Subsistema.Contains(mantenedor.Trim()));
            }



            return Json(mantenedores.OrderBy(m => m.Mantenedor));
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
                    var mapperElectromecanico = mapper.Map<Electromecanica>(electromecanica);
                    

                    context.Add(mapperElectromecanico);
                   await context.SaveChangesAsync();
                   await hubContext.Clients.All.SendAsync("recibir");

                    return Ok(1);

                }


            }
            else
            {

                if (ModelState.IsValid)
                {

                    var mapperElectromecanico = mapper.Map<Electromecanica>(electromecanica);

                    context.Update(mapperElectromecanico);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");

                    return Ok(2);

                }

            }





            return Ok(0);
        }

        
        public async Task<JsonResult> BuscarElectromecanico(int? id)
        {


            if (id == null || id == 0)
            {
                return Json(0);
            }

            var electromecanico = await context.Electromecanicas.FindAsync(id);

            if (electromecanico == null)
            {
                return Json(0);
            }

            return Json(electromecanico);



        }



        public async Task<JsonResult> EliminarElectromecanica(int? id)
        {


            if (id == null || id == 0)
            {
                return Json(0);
            }

            var electromecanico = await context.Electromecanicas.FindAsync(id);

            if (electromecanico == null)
            {
                return Json(0);
            }

            context.Remove(electromecanico);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("recibir");

            return Json(1);



        }


       

    }
}
