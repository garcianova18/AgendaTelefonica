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

namespace Agendatelefonica.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly AgendatelefonicaContext context;
        private readonly IMapper mapper;
        private readonly IHubContext<agendaHub> hubContext;

        public UsuariosController(AgendatelefonicaContext context, IMapper mapper, IHubContext<agendaHub> hubContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
        }

        public async Task<IActionResult> CrearEditarUsuarios([FromBody] UsuariosView usuariosView)
        {
            if (usuariosView.Id == 0)
            {

                var verificaExisteUser = context.Usuarios.Where(u => u.UserName.Trim() == usuariosView.UserName.Trim()).Count();

                if (verificaExisteUser == 1)
                {
                    return Ok(3);
                }

             

                //Crear

                if (ModelState.IsValid)
                {


                    Usuario usuario = new Usuario();


                    usuario.Nombre = usuariosView.Nombre;
                    usuario.Apellido = usuariosView.Apellido;
                    usuario.Codigo = usuariosView.Codigo;
                    usuario.UserName = usuariosView.UserName;
                    usuario.Password = usuariosView.Password;
                    usuario.IdRol = usuariosView.IdRol;
                    usuario.Fecha = DateTime.UtcNow.ToUniversalTime();

                    context.Add(usuario);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");



                    return Ok(1);
                }

            }
            else
            {
                //Editar

                var verificaExisteUser = context.Usuarios.Where(u => u.UserName.Trim() == usuariosView.UserName.Trim() && u.Id != usuariosView.Id).Count();


                if (verificaExisteUser == 1)
                {
                    return Ok(3);
                }

                if (ModelState.IsValid)
                {
                    Usuario usuario = new Usuario();

                    usuario.Id = usuariosView.Id;
                    usuario.Nombre = usuariosView.Nombre;
                    usuario.Apellido = usuariosView.Apellido;
                    usuario.Codigo = usuariosView.Codigo;
                    usuario.UserName = usuariosView.UserName;
                    usuario.Password = usuariosView.Password;
                    usuario.IdRol = usuariosView.IdRol;


                    context.Update(usuario);
                    await context.SaveChangesAsync();
                    await hubContext.Clients.All.SendAsync("recibir");
                    return Ok(2);
                }
            }


            return Ok(0);
        }

        public async Task<IActionResult> buscarUsuarios(int? id)
        {

            if (id == 0 || id == null)
            {
                return Ok(0);
            }
            var buscarusuario = await context.Usuarios.FindAsync(id);

            if (buscarusuario == null)
            {
                return Ok(0);
            }

            return Json(buscarusuario);

        }


        public async Task<int> EliminarUsuarios(int? id)
        {
            if (id == 0 || id == null)
            {
                return 0;
            }

            var usuario = await context.Usuarios.FindAsync(id);


            if (usuario == null)
            {
                return 0;
            }

            context.Remove(usuario);
            await context.SaveChangesAsync();
            await hubContext.Clients.All.SendAsync("recibir");

            return 1;
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


        

        public async Task<FileResult> ReportesExcel()
        {
            DataTable tabla_Electromecanica = new DataTable();
            DataTable tabla_Mantenedor = new DataTable();

            List<DataTable> tables = new List<DataTable>();

      
           //creamos las columnas que contendra nuestro tabla y las nombramos

            tabla_Mantenedor.Columns.AddRange(new DataColumn[]
               {

                    new DataColumn("Mantenedor"),
                    new DataColumn("Nombre"),
                    new DataColumn("Funcion"),
                    new DataColumn("Telefono"),
                    new DataColumn("Extension"),
                    new DataColumn("Subsistema"),
                   




               });

            tabla_Electromecanica.Columns.AddRange(new DataColumn[5]
               {
                    new DataColumn("Nombre"),
                    new DataColumn("Telefono"),
                    new DataColumn("Extension"),
                    new DataColumn("Correo"),
                    new DataColumn("Subsistema"),



               });

            var mantenedores = await context.Mantenedores.ToListAsync();
            var Electromecanica = await context.Electromecanicas.ToListAsync();

            //aqui recorremos nuestro listado de usuarios para asignarselo a las fila de nuestra tabla

            foreach (var mantenedor in mantenedores)
            {
                tabla_Mantenedor.Rows.Add(mantenedor.Mantenedor, mantenedor.Nombre, mantenedor.Funcion, mantenedor.Telefono, mantenedor.Extension, mantenedor.Subsistema);
               

            }
            foreach (var electro in Electromecanica)
            {
                tabla_Electromecanica.Rows.Add(electro.Nombre, electro.Telefono, electro.Extension, electro.Correo, electro.Subsistema);

            }

            //crear libro de excel

            using (var libro = new XLWorkbook())
            {
                //le damos un bombre a la tabla la cual sera el nombre de la hoja excel
                tabla_Electromecanica.TableName = "Electromecanica";
                tabla_Mantenedor.TableName = "Mantenedores";

                // crear hoja de excel
                var hoja_electromecanica = libro.AddWorksheet(tabla_Electromecanica);
                hoja_electromecanica.ColumnsUsed().AdjustToContents();

                var hoja_mantenedores = libro.AddWorksheet(tabla_Mantenedor).ColumnsUsed().AdjustToContents();


                
             

                //guardamos el libro en memoria

                using(var memoria = new MemoryStream())
                {
                    libro.SaveAs(memoria);

                    //le damos un nombre al archivo de excel y la extension en que se guardara
                    var NombreArchivo_Excel = string.Concat("Listado_Telefonico", ".xlsx");

                    return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", NombreArchivo_Excel);

                }


            }




        }



    }           
}
