using Agendatelefonica.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Agendatelefonica.Services
{
    public class Reportes:ControllerBase
    {

        private readonly AgendatelefonicaContext context;
        public Reportes(AgendatelefonicaContext context)
        {
            this.context = context;
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

                using (var memoria = new MemoryStream())
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
