 using Agendatelefonica.Models;
using Agendatelefonica.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Agendatelefonica.Controllers;

namespace gesin_app.Controllers  
{

   
    public class AccesoController : Controller
    {

        private readonly AgendatelefonicaContext context;
        

        public AccesoController(AgendatelefonicaContext context)
        {
            this.context = context;
            
        }

        public IActionResult Index()
        {



            return View();
        }


        [HttpPost]
        public async Task< IActionResult> Login( UsuariosView usuarioview)
        {

            var usuario = await context.Usuarios.Include(R=> R.IdRolNavigation).Where(u => u.UserName == usuarioview.UserName && u.Password == usuarioview.Password).FirstOrDefaultAsync();


            if (usuario !=null)
            {



                var Claims = new List<Claim>
                {
                   new Claim("Username", usuarioview.UserName = usuario.UserName),
                   new Claim (ClaimTypes.Name, usuarioview.FullName = usuario.Nombre + " " + usuario.Apellido),
                   new Claim("id", (usuarioview.Id = usuario.Id).ToString()),
                    new Claim(ClaimTypes.Role, usuarioview.IdRolNavigationNombre = usuario.IdRolNavigation.Nombre)

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
