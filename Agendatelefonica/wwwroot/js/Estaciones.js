//------------------ buscar Estaciones a actualizar ---------------//

function buscarActualizar_estaciones(id) {

    const idestacion = document.getElementById("estacion_id");
    const linea = document.getElementById("estacion_lineaActualizar");
    const estacion = document.getElementById("estacion_estacionActualizar");
    const tic = document.getElementById("estacion_ticActualizar");
    const cci = document.getElementById("estacion_cciActualizar");
    const com = document.getElementById("estacion_comActualizar");
    const enclavamiento = document.getElementById("estacion_enclavamientoActualizar");
    const atbt = document.getElementById("estacion_atbtActualizar");
    const set = document.getElementById("estacion_setActualizar");
    const cabinaAnden = document.getElementById("estacion_cabinaandenActualizar");
    const interfono = document.getElementById("estacion_interfonoActualizar");
    const emergencia = document.getElementById("estacion_emergenciaActualizar");



    const urlActualizar_usuarios = "Estaciones/buscarEstacion/?id=" + id;

    fetch(urlActualizar_usuarios)
        .then(res => res.json())
        .then(data => {


            idestacion.value = data.id;
            linea.value = data.linea;
            estacion.value = data.estacion;
            tic.value = data.boleteria;
            cci.value = data.cuartoControl;
            com.value = data.cuartoCom;
            enclavamiento.value = data.enclavamiento;
            atbt.value = data.cuartoAtbt;
            set.value = data.subestacionTraccion;
            cabinaAnden.value = data.cabinaAnden;
            interfono.value = data.interfono;
            emergencia.value = data.pstnEmergencia;

        })
        .catch(e => e)



}


//------------------ Agregar Estacion ---------------//


const formagregarEstacion = document.getElementById("form_agregarEstacion");


function CrearEstacion(e) {

    e.preventDefault();


    const linea = document.getElementById("estacion_linea");
    const estacion = document.getElementById("estacion_estacion");
    const tic = document.getElementById("estacion_tic");
    const cci = document.getElementById("estacion_cci");
    const com = document.getElementById("estacion_com");
    const enclavamiento = document.getElementById("estacion_enclavamiento");
    const atbt = document.getElementById("estacion_atbt");
    const set = document.getElementById("estacion_set");
    const cabinaAnden = document.getElementById("estacion_cabinaanden");
    const interfono = document.getElementById("estacion_interfono");
    const emergencia = document.getElementById("estacion_emergencia");



    const errorLinea = document.getElementById("estacion_error-linea");
    const errorEstacion = document.getElementById("estacion_error-estacion");
    const errorTic = document.getElementById("estacion_error-tic");
    const errorCci = document.getElementById("estacion_error-cci");
    const errorInterfono = document.getElementById("estacion_error-interfono");





    linea.value == "" ? errorLinea.classList.add("campo_incorrecto") : errorLinea.classList.remove("campo_incorrecto");
    estacion.value == "" ? errorEstacion.classList.add("campo_incorrecto") : errorEstacion.classList.remove("campo_incorrecto");
    tic.value == "" ? errorTic.classList.add("campo_incorrecto") : errorTic.classList.remove("campo_incorrecto");
    cci.value == "" ? errorCci.classList.add("campo_incorrecto") : errorCci.classList.remove("campo_incorrecto");
    interfono.value == "" ? errorInterfono.classList.add("campo_incorrecto") : errorInterfono.classList.remove("campo_incorrecto")








    const divpadre = document.getElementById("estacion_divpadre_camposObligatorios");



    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".estacion_campos_obligatorios"));


    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".estacion_iconoError_camposObligatorios"));




    //aqui agregamos o quitamos la clase error si el campo esta lleno o vacio
    function agregarQuitarEstacion(e) {


        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("estacion_campos_obligatorios")) {


            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_camposObligatorios[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatorios[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre.addEventListener("keyup", agregarQuitarEstacion)




    const alert_camposObligatorios = document.getElementById("estacion_alert_camposObligatorios");



    if (linea.value == "" || estacion.value == "" || tic.value == "" || cci.value == "" || interfono.value == "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios");

    }
    else {



        const url = "Estaciones/CrearEditarEstaciones/estacionesView";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({

                linea: linea.value,
                estacion: estacion.value,
                boleteria: tic.value,
                cuartoControl: cci.value,
                cuartoCom: com.value,
                enclavamiento: enclavamiento.value,
                cuartoAtbt: atbt.value,
                subestacionTraccion: set.value,
                cabinaAnden: cabinaAnden.value,
                interfono: interfono.value,
                pstnEmergencia: emergencia.value

            })



        })
            .then(res => res.json())
            .then(data => {



                if (data == 1) {
                    // para que el modal se cierre automaticamente al agregar
                    const usuario_btncerrar = document.getElementById("estacion_btncerrar");
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

                    document.getElementById("estacion_linea").value = "";
                    document.getElementById("estacion_estacion").value = "";
                    document.getElementById("estacion_tic").value = "";
                    document.getElementById("estacion_cci").value = "";
                    document.getElementById("estacion_com").value = "";
                    document.getElementById("estacion_enclavamiento").value = "";
                    document.getElementById("estacion_atbt").value = "";
                    document.getElementById("estacion_set").value = "";
                    document.getElementById("estacion_cabinaanden").value = "";
                    document.getElementById("estacion_interfono").value = "";
                    document.getElementById("estacion_emergencia").value = "";


                    ////para que refresque la tabla y nuestre lo nuevo que se agrego
                    //const urlestaciones = "Electromecanica/Estaciones";
                    //listarEstaciones(urlestaciones);



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

formagregarEstacion.addEventListener("submit", CrearEstacion)

//------------------ Actualizar Estaciones  ---------------//


const actualizarEstacion = document.getElementById("form_actualizarEstacion");

function EditarEstacion(e) {

    e.preventDefault();


    const id_Estacion = Number(document.getElementById("estacion_id").value);
    const linea = document.getElementById("estacion_lineaActualizar");
    const estacion = document.getElementById("estacion_estacionActualizar");
    const tic = document.getElementById("estacion_ticActualizar");
    const cci = document.getElementById("estacion_cciActualizar");
    const com = document.getElementById("estacion_comActualizar");
    const enclavamiento = document.getElementById("estacion_enclavamientoActualizar");
    const atbt = document.getElementById("estacion_atbtActualizar");
    const set = document.getElementById("estacion_setActualizar");
    const cabinaAnden = document.getElementById("estacion_cabinaandenActualizar");
    const interfono = document.getElementById("estacion_interfonoActualizar");
    const emergencia = document.getElementById("estacion_emergenciaActualizar");



    const errorLinea = document.getElementById("estacion_error_lineaActualizar");
    const errorEstacion = document.getElementById("estacion_error_estacionActualizar");
    const errorTic = document.getElementById("estacion_error_ticActualizar");
    const errorCci = document.getElementById("estacion_error_cciActualizar");
    const errorInterfono = document.getElementById("estacion_error_interfonoActualizar");





    linea.value == "" ? errorLinea.classList.add("campo_incorrecto") : errorLinea.classList.remove("campo_incorrecto");
    estacion.value == "" ? errorEstacion.classList.add("campo_incorrecto") : errorEstacion.classList.remove("campo_incorrecto");
    tic.value == "" ? errorTic.classList.add("campo_incorrecto") : errorTic.classList.remove("campo_incorrecto");
    cci.value == "" ? errorCci.classList.add("campo_incorrecto") : errorCci.classList.remove("campo_incorrecto");
    interfono.value == "" ? errorInterfono.classList.add("campo_incorrecto") : errorInterfono.classList.remove("campo_incorrecto")



    const divpadre = document.getElementById("estacion_divpadre_camposObligatoriosActualizar");



    //convertimos en array la collecion que nos trae la clase campos-obligatorios
    const camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".estacion_campos_obligatoriosActualizar"));


    //convertimos en array la collecion que nos trae la clase hijos
    const iconoError_camposObligatorios = Array.prototype.slice.apply(document.querySelectorAll(".estacion_iconoError_camposObligatoriosActualizar"));


    //aqui agregamos o quitamos la clase error si el campo esta lleno o vacio
    function agregarQuitarEstacionActualizar(e) {


        // aqui decimos que cuando un campo obligatorio dispara el evento ejecute algo
        if (e.target.classList.contains("estacion_campos_obligatoriosActualizar")) {


            //obtenemos el index del campo que disparo el evento para asi saber a cual se le mostrara el error y acual se le quitara
            const Index = camposObligatorios.indexOf(e.target);


            //aqui decimos si el campo que disparo el evento esta vacio agregale la clase error si esta lleno quitase

            e.target.value.trim() != "" ? iconoError_camposObligatorios[Index].classList.remove("campo_incorrecto") : iconoError_camposObligatorios[Index].classList.add("campo_incorrecto")


        }

    }


    divpadre.addEventListener("keyup", agregarQuitarEstacionActualizar)




    const alert_camposObligatorios = document.getElementById("estacion_alert_camposObligatoriosActualizar");



    if (linea.value == "" || estacion.value == "" || tic.value == "" || cci.value == "" || interfono.value == "") {



        alert_camposObligatorios.classList.remove("alert-campos-obligatorios");

    }
    else {

        alert_camposObligatorios.classList.add("alert-campos-obligatorios");

        const url = "Estaciones/CrearEditarEstaciones/estacionesView";

        fetch(url, {

            method: "POST",

            headers: {

                'Content-Type': 'application/json'

            },

            body: JSON.stringify({


                id: id_Estacion,
                linea: linea.value,
                estacion: estacion.value,
                boleteria: tic.value,
                cuartoControl: cci.value,
                cuartoCom: com.value,
                enclavamiento: enclavamiento.value,
                cuartoAtbt: atbt.value,
                subestacionTraccion: set.value,
                cabinaAnden: cabinaAnden.value,
                interfono: interfono.value,
                pstnEmergencia: emergencia.value

            })



        })
            .then(res => res.json())
            .then(data => {

                if (data == 2) {
                    // para que el modal se cierre automaticamente al actualizar
                    const usuario_btncerrar = document.getElementById("estacion_btncerrarActualizar");
                    usuario_btncerrar.click();

                    //alerta de actualizacion
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Su registro ha sido actualizado',
                        showConfirmButton: false,
                        timer: 1500
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

actualizarEstacion.addEventListener("submit", EditarEstacion);



//----------filtrar Estaciones----------------------------//

const estacion = document.getElementById("electromecanica");

function filtrarEstaciones() {

    const urlestacion = "/Electromecanica/Estaciones/?estacion=" + estacion.value;

    listarEstaciones(urlestacion);

}

estacion.addEventListener("keyup", filtrarEstaciones);


//------------------ Eliminar Estaciones ---------------//

function Eliminar_Estaciones(id) {


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

            const urlEliminar = "Estaciones/EliminarEstaciones/?id=" + id;

            fetch(urlEliminar)
                .then(res => res.json())
                .then(data => {

                    if (data == 1) {

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




// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de crear estaciones

const estacionBtncerrar = document.getElementById("estacion_btncerrar");

estacionBtncerrar.addEventListener("click", () => {



    document.getElementById("estacion_linea").value = "";
    document.getElementById("estacion_estacion").value = "";
    document.getElementById("estacion_tic").value = "";
    document.getElementById("estacion_cci").value = "";
    document.getElementById("estacion_com").value = "";
    document.getElementById("estacion_enclavamiento").value = "";
    document.getElementById("estacion_atbt").value = "";
    document.getElementById("estacion_set").value = "";
    document.getElementById("estacion_cabinaanden").value = "";
    document.getElementById("estacion_interfono").value = "";

    document.getElementById("estacion_alert_camposObligatorios").classList.add("alert-campos-obligatorios");;



    document.getElementById("estacion_error-linea").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error-estacion").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error-tic").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error-cci").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error-interfono").classList.remove("campo_incorrecto");


})



// funcion para que al hacer click en el boton cerrar del modal se limpien los campos y las alertas
//esta funcion pertenece al modal de crear estaciones

const estacionBtncerrarActualizar = document.getElementById("estacion_btncerrarActualizar");

estacionBtncerrarActualizar.addEventListener("click", () => {



    document.getElementById("estacion_lineaActualizar").value = "";
    document.getElementById("estacion_estacionActualizar").value = "";
    document.getElementById("estacion_ticActualizar").value = "";
    document.getElementById("estacion_cciActualizar").value = "";
    document.getElementById("estacion_comActualizar").value = "";
    document.getElementById("estacion_enclavamientoActualizar").value = "";
    document.getElementById("estacion_atbtActualizar").value = "";
    document.getElementById("estacion_setActualizar").value = "";
    document.getElementById("estacion_cabinaandenActualizar").value = "";
    document.getElementById("estacion_interfonoActualizar").value = "";

    document.getElementById("estacion_alert_camposObligatoriosActualizar").classList.add("alert-campos-obligatorios");;



    document.getElementById("estacion_error_lineaActualizar").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error_estacionActualizar").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error_ticActualizar").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error_cciActualizar").classList.remove("campo_incorrecto");
    document.getElementById("estacion_error_interfonoActualizar").classList.remove("campo_incorrecto");


})

