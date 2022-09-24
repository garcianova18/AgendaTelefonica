using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendatelefonica.Controllers
{
    public class agendaHub:Hub
    {

        //esta parte a qui se confioigura si es enviardata entre el cliente

        //public async Task Enviar(string nombre, string apellido, string codigo, string username, string contrasena, int rol)
        //{

        //    await Clients.All.SendAsync("recibir", nombre, apellido, codigo, username);
        //}
    }
}
