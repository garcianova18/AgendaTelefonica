
//-------------- Listar Usuarios----------------------------//

function listarUsuarios(url) {


    fetch(url)
        .then(res => res.json())
        .then(data => {


            let tbodydataUsuarios = document.getElementById("tbodydataUsuarios");

            let filas = "";


            for (var i = 0; i < data.length; i++) {



                filas += `


                        <tr>

                        <td>${data[i].nombre}</td>
                        <td>${data[i].apellido}</td>
                        <td>${data[i].codigo}</td>
                        <td>${data[i].userName}</td>
                        <td>${data[i].idRolNavigationNombre}</td>



                                        <td>

                                     <!-- Button trigger modal -->
                                   <a href="" onclick="buscarActualizar_usuarios(${data[i].id})" class="link-primary text-decoration-none"  data-bs-toggle="modal" data-bs-target="#modalActualizar_usuarios">Actualizar</a>

                                </td>

                                <td>

                                    <a href="#" onclick="Eliminar_usuario(${data[i].id})" class="link-danger text-decoration-none" >Eliminar</a>

                                </td>



                        </tr>

                    `


            }

            tbodydataUsuarios.innerHTML = filas;


        })


}

//------------------ Agregar Usuarios ---------------//


const formagregarUsuario = document.getElementById("form_agregarUsuario");


function CrearUsuarios(e) {

    e.preventDefault();


    const nombre = document.getElementById("user_nombre");
    const apellido = document.getElementById("user_apellido");
    const codigo = document.getElementById("user_codigo");
    const usuario = document.getElementById("user_usuario");
    const contrasena = document.getElementById("user_contrasena");
    const rol = document.getElementById("user_rol");


    const errorNombre = document.getElementById("user_error_nombre");
    const errorApellido = document.getElementById("user_error_apellido");
    const erroCodigo = document.getElementById("user_error_codigo");
    const errorUsuario = document.getElementById("user_error_usuario");
    const erroContrasena = document.getElementById("user_error_contrasena");
    const errorRol = document.getElementById("user_error_rol");



    nombre.value === "" ? errorNombre.classList.add("campo_incorrecto") : errorNombre.classList.remove("campo_incorrecto");
    apellido.value === "" ? errorApellido.classList.add("campo_incorrecto") : errorApellido.classList.remove("campo_incorrecto");
    codigo.value === "" ? erroCodigo.classList.add("campo_incorrecto") : erroCodigo.classList.remove("campo_incorrecto");
    usuario.value === "" ? errorUsuario.classList.add("campo_incorrecto") : errorUsuario.classList.remove("campo_incorrecto");
    contrasena.value === "" ? erroContrasena.classList.add("campo_incorrecto") : erroContrasena.classList.remove("campo_incorrecto")
    rol.value ==="" ? errorRol.classList.add("campo_incorrecto") : errorRol.classList.remove("campo_incorrecto")




    const divpadre_User = document.getElementById("user_divpadre");

    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".user_campos_obligatorios"));

    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_camposObligatirios = Array.prototype.slice.apply(document.querySelectorAll(".user_iconoError_camposObligatorios"));



    //aqui agregamos o quitamos cla clase error si el campo esta lleno o vacio
    function agregarQuitarUsuario(e) {




        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("user_campos_obligatorios")) {




            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_camposObligatirios[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatirios[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre_User.addEventListener("keyup", agregarQuitarUsuario)




    const alert_camposObligatorios = document.getElementById("user_alert_camposObligatorios");


    if (nombre.value === "" || apellido.value === "" || codigo.value === "" || usuario.value === "" || contrasena.value === "" || rol.value === "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios");

    }
    else {

        alert_camposObligatorios.classList.add("alert-campos-obligatorios");

        const url = "Usuarios/CrearEditarUsuarios/usuariosView";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({


                nombre: nombre.value,
                apellido: apellido.value,
                codigo: codigo.value,
                userName: usuario.value,
                password: contrasena.value,
                idRol: Number(rol.value)


            })



        })
            .then(res => res.json())
            .then(data => {



                if (data === 1) {
                    // para que el modal se cierre automaticamente al agregar
                    const usuario_btncerrar = document.getElementById("usuario_btncerrar");
                    usuario_btncerrar.click();



                    //alerta cuando se agregar un nuevo mantenedor
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Se ha agregado correctamente',
                        showConfirmButton: false,
                        timer: 1500
                    })

                    //para Limpiar los campos al agregarun nuevo registro


                    document.getElementById("user_nombre").value = "";
                    document.getElementById("user_apellido").value = "";
                    document.getElementById("user_codigo").value = "";
                    document.getElementById("user_usuario").value = "";
                    document.getElementById("user_contrasena").value = "";
                    document.getElementById("user_rol").value = "";


                    //para que refresque la tabla y nuestre lo nuevo que se agrego
                    //const urlusuarios = "Electromecanica/Usuarios";
                    //listarUsuarios(urlusuarios);



                }
                else if (data === 3) {

                    Swal.fire({
                        position: 'center',
                        icon: 'warning',
                        title: 'Este usuario ya existe intente con uno diferente',
                        showConfirmButton: false,
                        timer: 3000
                    })


                }
                else {

                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Ocurrio algun error intentelo de nuevo',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })
            .catch(e => e)


    }





}

formagregarUsuario.addEventListener("submit", CrearUsuarios)


//------------------ buscar usuario a actualizar ---------------//

function buscarActualizar_usuarios(id) {

    const urlActualizar_usuarios = "Usuarios/buscarUsuarios/?id=" + id;

    fetch(urlActualizar_usuarios)
        .then(res => res.json())
        .then(data => {

            const id_Usuario = document.getElementById("user_idusuario");
            const nombre = document.getElementById("user_nombreActualizar");
            const apellido = document.getElementById("user_apellidoActualizar");
            const codigo = document.getElementById("user_codigoActualizar");
            const usuario = document.getElementById("user_usuarioActualizar");
            const contrasena = document.getElementById("user_contrasenaActualizar");
            const rol = document.getElementById("user_rolActualizar");

            id_Usuario.value = data.id;
            nombre.value = data.nombre;
            apellido.value = data.apellido;
            codigo.value = data.codigo;
            usuario.value = data.userName;
            contrasena.value = data.password;
            rol.value = data.idRol;

        })
        .catch(e => e)



}



//------------------ Actualizar Usuario ---------------//

const actualizarUsuario = document.getElementById("form_actualizarUsuario");

function EditarUsuario(e) {

    e.preventDefault();

    const id_Usuario = Number(document.getElementById("user_idusuario").value);
    const nombre = document.getElementById("user_nombreActualizar");
    const apellido = document.getElementById("user_apellidoActualizar");
    const codigo = document.getElementById("user_codigoActualizar");
    const usuario = document.getElementById("user_usuarioActualizar");
    const contrasena = document.getElementById("user_contrasenaActualizar");
    const rol = document.getElementById("user_rolActualizar");






    const errorNombre = document.getElementById("user_error_nombreActualizar");
    const errorApellido = document.getElementById("user_error_apellidoActualizar");
    const erroCodigo = document.getElementById("user_error_codigoActualizar");
    const errorUsuario = document.getElementById("user_error_usuarioActualizar");
    const erroContrasena = document.getElementById("user_error_contrasenaActualizar");
    const errorRol = document.getElementById("user_error_rolActualizar");



    nombre.value === "" ? errorNombre.classList.add("campo_incorrecto") : errorNombre.classList.remove("campo_incorrecto");
    apellido.value === "" ? errorApellido.classList.add("campo_incorrecto") : errorApellido.classList.remove("campo_incorrecto");
    codigo.value === "" ? erroCodigo.classList.add("campo_incorrecto") : erroCodigo.classList.remove("campo_incorrecto");
    usuario.value === "" ? errorUsuario.classList.add("campo_incorrecto") : errorUsuario.classList.remove("campo_incorrecto");
    contrasena.value === "" ? erroContrasena.classList.add("campo_incorrecto") : erroContrasena.classList.remove("campo_incorrecto")
    rol.value === "" ? errorRol.classList.add("campo_incorrecto") : errorRol.classList.remove("campo_incorrecto")




    const divpadre_User = document.getElementById("user_divpadreActualizar");

    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".user_campos_obligatoriosActualizar"));

    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_camposObligatirios = Array.prototype.slice.apply(document.querySelectorAll(".user_iconoError_camposObligatoriosActualizar"));



    //aqui agregamos o quitamos cla clase error si el campo esta lleno o vacio
    function agregarQuitarUsuarioActualizar(e) {




        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("user_campos_obligatoriosActualizar")) {




            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() !== "" ? iconoError_camposObligatirios[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatirios[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre_User.addEventListener("keyup", agregarQuitarUsuarioActualizar)




    const alert_camposObligatorios = document.getElementById("user_alert_camposObligatoriosActualizar");



    if (nombre.value === "" || apellido.value === "" || codigo.value === "" || usuario.value === "" || contrasena.value === "" || rol.value === "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios");

    }
    else {

        alert_camposObligatorios.classList.add("alert-campos-obligatorios");


        const url = "Usuarios/CrearEditarUsuarios/usuariosView";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({

                id: id_Usuario,
                nombre: nombre.value,
                apellido: apellido.value,
                codigo: codigo.value,
                userName: usuario.value,
                password: contrasena.value,
                idRol: Number(rol.value)

            })



        })
            .then(res => res.json())
            .then(data => {

                if (data === 2) {
                    // para que el modal se cierre automaticamente al actualizar
                    const usuario_btncerrar = document.getElementById("usuario_btncerrar_actualizar");
                    usuario_btncerrar.click();

                    //alerta de actualizacion
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Su registro ha sido actualizado',
                        showConfirmButton: false,
                        timer: 1500
                    })


                    //para que refresque la tabla y nuestre lo nuevo que se agrego
                    const urlusuarios = "Electromecanica/Usuarios";
                    listarUsuarios(urlusuarios);





                }
                else if (data === 3) {

                    Swal.fire({
                        position: 'center',
                        icon: 'warning',
                        title: 'Este usuario ya existe intente con uno diferente',
                        showConfirmButton: false,
                        timer: 3000
                    })


                }

                else {

                    Swal.fire({
                        position: 'center',
                        icon: 'error',
                        title: 'Ocurrio algun error intentelo de nuevo',
                        showConfirmButton: false,
                        timer: 1500
                    })
                }
            })


    }




}

actualizarUsuario.addEventListener("submit", EditarUsuario);


//------------------ Eliminar Usuario ---------------//

function Eliminar_usuario(id) {


    Swal.fire({
        title: 'Estas seguro?',
        text: "No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si, eliminar!'

    }).then((result) => {

        if (result.isConfirmed) {

            const urlEliminar = "Usuarios/EliminarUsuarios/?id=" + id;

            fetch(urlEliminar)
                .then(res => res.json())
                .then(data => {

                    if (data === 1) {

                        const urlusuarios = "Electromecanica/Usuarios";
                        listarUsuarios(urlusuarios);


                        Swal.fire({
                            position: 'center',
                            icon: 'success',
                            title: 'Su registro ha sido eliminado',
                            showConfirmButton: false,
                            timer: 1500
                        })




                    }
                    else {

                        Swal.fire({
                            position: 'center',
                            icon: 'error',
                            title: 'Ocurrio algun error y el registro no pudo ser eliminado',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }



                })
                .catch(e => e)


        }
    })



}




//----------filtrar Usuarios----------------------------//

const usuarios = document.getElementById("electromecanica");

function filtrarUsuarios() {

    const urlusuarios = "/Usuarios/Usuarios/?usuario=" + usuarios.value;


    listarUsuarios(urlusuarios);

}

usuarios.addEventListener("keyup", filtrarUsuarios);



// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de crear Usuario

const usuariosBtncerrar = document.getElementById("usuario_btncerrar");

usuariosBtncerrar.addEventListener("click", () => {


    document.getElementById("user_nombre").value = "";
    document.getElementById("user_apellido").value = "";
    document.getElementById("user_codigo").value = "";
    document.getElementById("user_usuario").value = "";
    document.getElementById("user_contrasena").value = "";
    document.getElementById("user_rol").value = "";

    document.getElementById("user_alert_camposObligatorios").classList.add("alert-campos-obligatorios");;


    document.getElementById("user_error_nombre").classList.remove("campo_incorrecto");
    document.getElementById("user_error_apellido").classList.remove("campo_incorrecto");
    document.getElementById("user_error_codigo").classList.remove("campo_incorrecto");
    document.getElementById("user_error_usuario").classList.remove("campo_incorrecto");
    document.getElementById("user_error_contrasena").classList.remove("campo_incorrecto");
    document.getElementById("user_error_rol").classList.remove("campo_incorrecto");


})


// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de Actualizar Usuario

const usuariosBtncerrarActualizar = document.getElementById("usuario_btncerrar_actualizar");

usuariosBtncerrarActualizar.addEventListener("click", () => {


    document.getElementById("user_nombreActualizar").value = "";
    document.getElementById("user_apellidoActualizar").value = "";
    document.getElementById("user_codigoActualizar").value = "";
    document.getElementById("user_usuarioActualizar").value = "";
    document.getElementById("user_contrasenaActualizar").value = "";
    document.getElementById("user_rolActualizar").value = "";

    document.getElementById("user_alert_camposObligatoriosActualizar").classList.add("alert-campos-obligatorios");;


    document.getElementById("user_error_nombreActualizar").classList.remove("campo_incorrecto");
    document.getElementById("user_error_apellidoActualizar").classList.remove("campo_incorrecto");
    document.getElementById("user_error_codigoActualizar").classList.remove("campo_incorrecto");
    document.getElementById("user_error_usuarioActualizar").classList.remove("campo_incorrecto");
    document.getElementById("user_error_contrasenaActualizar").classList.remove("campo_incorrecto");
    document.getElementById("user_error_rolActualizar").classList.remove("campo_incorrecto");


})