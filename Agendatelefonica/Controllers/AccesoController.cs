 using Agendatelefonica.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Agendatelefonica.Services;

namespace gesin_app.Controllers  
{

   
    public class AccesoController : Controller
    {

        
        private readonly IServicioAcceso servicioAcceso;

        public AccesoController(IServicioAcceso servicioAcceso)
        {
            
            this.servicioAcceso = servicioAcceso;
        }

        public IActionResult Index()
        {



            return View();
        }


        [HttpPost]
        public async Task< IActionResult> Login( Usuario usuario)
        {

            var Usuario = await servicioAcceso.BuscarUsuario(usuario);

            if (usuario !=null)
            {



                var Claims = new List<Claim>
                {
                   new Claim("Username", Usuario.UserName),
                   new Claim (ClaimTypes.Name, Usuario.Nombre + " " + Usuario.Apellido),
                   new Claim("id", Usuario.Id.ToString()),
                   new Claim(ClaimTypes.Role, Usuario.IdRolNavigation.Nombre)

                };

                var claimidentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimidentity));

                return RedirectToAction("Index", "Electromecanica");
            }
            else
            {

               
                    ViewBag.errorusuario = "El usuario o contraseña que ha ingresado es incorrecto vuelva a intentarlo";
                

               
                return View(nameof(Index));
            }
        }


        public async Task< IActionResult > Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index","Acceso");
        }


        public ActionResult Restringido()
        {


            return View();
        }

        
    }
}
