//crear la conexion con signalR
// agendaHUd es la clase creada en el backend y configurada como un Empoint en Startup
let conexion = new signalR.HubConnectionBuilder().withUrl("/agendaHud").build();

//conexion.start().then(function () {
//    console.log("conexion exitosa")
//}).catch(error => console.log(error));

//inicar la conecion con signalR
conexion.start();

// Recibir los eventos y enviarselo a todos los usuarios conectados al Hub
conexion.on("recibir", () => listarTodos());
