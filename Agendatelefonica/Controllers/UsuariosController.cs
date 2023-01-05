using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;
using Agendatelefonica.Services;

namespace Agendatelefonica.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;
        private readonly IRepositoryGenerico<Usuario> repositoryGenerico;
   

        public UsuariosController(AgendatelefonicaContext context, IMapper mapper, 
            IHubContext<agendaHub> hubContext, IRepositoryGenerico<Usuario> repositoryGenerico)
        {
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.repositoryGenerico = repositoryGenerico;
            
        }

        public async Task<ActionResult<int>> CrearEditarUsuarios([FromBody] UsuarioCreateView usuario)
        {
            if (usuario.Id == 0)
            {

                var verificaExisteUser = context.Usuarios.Where(u => u.UserName.Trim() == usuario.UserName.Trim()).Count();

                var mapusuario = mapper.Map<Usuario>(usuario);

               

                if (verificaExisteUser == 1)
                {
                    return 3;
                }

             

                //Crear

                if (ModelState.IsValid)
                {
                    

                    var mapUsuario = mapper.Map<Usuario>(usuario);

                    mapUsuario.Fecha = DateTime.Now;

                    var usuarioCreate = await repositoryGenerico.Create(mapUsuario);

                    await hubContext.Clients.All.SendAsync("recibir");



                    return usuarioCreate;
                }

            }
            else
            {
                //Editar

                var verificaExisteUser = context.Usuarios.Where(u => u.UserName.Trim() == usuario.UserName.Trim() && u.Id != usuario.Id).Count();


                if (verificaExisteUser == 1)
                {
                    return 3;
                }

                if (ModelState.IsValid)
                {

                    var mapUsuario = mapper.Map<Usuario>(usuario);

                    var usuarioUpdate= await repositoryGenerico.update(mapUsuario);

                    
                    await hubContext.Clients.All.SendAsync("recibir");

                    return usuarioUpdate;


                }
            }


            return 0;
        }

        public async Task<ActionResult<UsuariosView>> buscarUsuarios(int? id)
        {

            if (id == 0 || id == null)
            {
                return Ok(0);
            }
            var Usuario = await repositoryGenerico.GetById(id);

            if (Usuario == null)
            {
                return Ok(0);
            }

            var mapUsuario =mapper.Map<UsuariosView>(Usuario);

            return mapUsuario;

        }


        public async Task<int> EliminarUsuarios(int? id)
        {
            if (id == 0 || id == null)
            {
                return 0;
            }

            var usuario = await repositoryGenerico.GetById(id);


            if (usuario == null)
            {
                return 0;
            }

            var usuarioDelete = await repositoryGenerico.Delete(usuario);

            await hubContext.Clients.All.SendAsync("recibir");

            return usuarioDelete;
        }


      


    }           
}
