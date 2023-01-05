using Agendatelefonica.Models;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Agendatelefonica.Services
{

    public interface ISelectRol
    {
        List<SelectListItem> SelectRol();
    }
    public class SelectRoles: ISelectRol
    {
        private readonly AgendatelefonicaContext _context;
        public SelectRoles( AgendatelefonicaContext context)
        {
                _context = context;
        }
        public List<SelectListItem> SelectRol( )
        {

            var roles = _context.Rols.Select(r => new SelectListItem
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

    }
}
