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
       
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;
        private readonly IRepositoryGenerico<Usuario> repositoryGenerico;
        private readonly IServicioUsuario servicioUsuario;

        public UsuariosController( IMapper mapper, 
            IHubContext<agendaHub> hubContext, IRepositoryGenerico<Usuario> repositoryGenerico, IServicioUsuario servicioUsuario)
        {
         
            this.mapper = mapper;
            this.hubContext = hubContext;
            this.repositoryGenerico = repositoryGenerico;
            this.servicioUsuario = servicioUsuario;
        }
        public async Task<IEnumerable<UsuariosView>> Usuarios(string usuario)
        {

            var usuarios = await servicioUsuario.Usuarios();

            var MapUsuarios = mapper.Map<IEnumerable<UsuariosView>>(usuarios);


            if (usuario != null)
            {
                MapUsuarios = MapUsuarios.Where(u => u.Nombre.Contains(usuario) || u.Apellido.Contains(usuario) || u.Codigo.Contains(usuario) || u.UserName.Contains(usuario));
            }


            return MapUsuarios.OrderBy(u => u.UserName);
        }

        public async Task<ActionResult<int>> CrearEditarUsuarios([FromBody] UsuarioCreateView usuario)
        {
            // verificar si el usuario tanto para crear coo par actualiza existe
                var verificaExisteUser = await servicioUsuario.VerifivarExiste(usuario);
            
           


            if (usuario.Id == 0)
            {

                

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
